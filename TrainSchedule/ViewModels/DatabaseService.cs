using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TrainSchedule.Models;

namespace TrainSchedule.ViewModels
{
    public class DatabaseService
    {
        private const string ConnectionString = "Host=localhost;Username=postgres;Password=polishchuk;Database=Trackly";
        private const string EncryptionKey = "MySecureKey12345";

        public static bool SaveUserToDatabase(User user)
        {
            try
            {
                using var conn = new NpgsqlConnection(ConnectionString);
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    "INSERT INTO users (email, password, first_name, last_name, phone_number, ticket_type, is_admin) " +
                    "VALUES (@Email, @Password, @FirstName, @LastName, @PhoneNumber, @TicketType, @IsAdmin)", conn);

                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", HashPassword(user.Password));
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                cmd.Parameters.AddWithValue("@TicketType", user.TicketType);
                cmd.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        public static bool SaveTrainToDatabase(TrainModel train)
        {
            try
            {
                using var conn = new NpgsqlConnection(ConnectionString);
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    "INSERT INTO trains (trainname, start_station, finish_station, departure, arrival, price) " +
                    "VALUES (@TrainName, @StartStation, @FinishStation, @Departure, @Arrival, @Price)", conn);

                cmd.Parameters.AddWithValue("@TrainName", train.TrainName);
                cmd.Parameters.AddWithValue("@StartStation", train.StartStation);
                cmd.Parameters.AddWithValue("@FinishStation", train.FinishStation);
                cmd.Parameters.AddWithValue("@Departure", train.Departure);
                cmd.Parameters.AddWithValue("@Arrival", train.Arrival);
                cmd.Parameters.AddWithValue("@Price", train.Price);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd bazy danych: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        public static bool UpdateTrain(TrainModel train)
        {
            try
            {
                using var conn = new NpgsqlConnection(ConnectionString);
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    "UPDATE trains SET trainname = @TrainName, start_station = @StartStation, " +
                    "finish_station = @FinishStation, departure = @Departure, arrival = @Arrival, price = @Price " +
                    "WHERE trainname = @TrainName", conn);

                cmd.Parameters.AddWithValue("@TrainName", train.TrainName);
                cmd.Parameters.AddWithValue("@StartStation", train.StartStation);
                cmd.Parameters.AddWithValue("@FinishStation", train.FinishStation);
                cmd.Parameters.AddWithValue("@Departure", train.Departure);
                cmd.Parameters.AddWithValue("@Arrival", train.Arrival);
                cmd.Parameters.AddWithValue("@Price", train.Price);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0; // Return true if rows were affected
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd bazy danych: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public static List<TrainModel> GetTrains(string from, string to, string selectedTime)
        {
            List<TrainModel> trains = new List<TrainModel>();

            try
            {
                using var conn = new NpgsqlConnection(ConnectionString);
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    "SELECT trainname, start_station, finish_station, departure, arrival, price " +
                    "FROM trains " +
                    "WHERE start_station = @From " +
                    "AND finish_station = @To " +
                    "AND departure::TIME >= @SelectedTime::TIME " +  // Konwersja na TIME w SQL
                    "ORDER BY departure::TIME", conn);

                cmd.Parameters.AddWithValue("@From", from);
                cmd.Parameters.AddWithValue("@To", to);
                cmd.Parameters.AddWithValue("@SelectedTime", selectedTime); // np. "13:00"

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    trains.Add(new TrainModel
                    {
                        TrainName = reader.GetString(0),
                        StartStation = reader.GetString(1),
                        FinishStation = reader.GetString(2),
                        Departure = reader.GetString(3),
                        Arrival = reader.GetString(4),
                        Price = reader.GetString(5)
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd bazy danych: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return trains;
        }
        public static bool DeleteTrain(string trainName)
        {
            try
            {
                using var conn = new NpgsqlConnection(ConnectionString);
                conn.Open();

                // Przygotowanie zapytania SQL do usunięcia pociągu
                using var cmd = new NpgsqlCommand("DELETE FROM trains WHERE trainname = @TrainName", conn);
                cmd.Parameters.AddWithValue("@TrainName", trainName);

                int rowsAffected = cmd.ExecuteNonQuery();

                // Jeśli usunięto co najmniej jeden wiersz, zwróć true
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd bazy danych: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }


        public static User AuthenticateUser(string email, string password)
        {
            try
            {
                using var conn = new NpgsqlConnection(ConnectionString);
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    "SELECT password, first_name, last_name, phone_number, ticket_type, is_admin FROM users WHERE email = @Email", conn);
                cmd.Parameters.AddWithValue("@Email", email);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string hashedPasswordInDb = reader.GetString(0);
                    if (hashedPasswordInDb == HashPassword(password))
                    {
                        return new User
                        {
                            Email = email,
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            PhoneNumber = reader.GetString(3),
                            TicketType = reader.GetString(4),
                            IsAdmin = reader.GetBoolean(5)
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return null;
        }


        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder hashString = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                hashString.Append(b.ToString("x2"));
            }
            return hashString.ToString();
        }

        public static void SaveEncryptedUserToConfig(User user)
        {
            var json = JsonConvert.SerializeObject(user);
            File.WriteAllText("config.json", EncryptData(json));
        }

        public static User LoadUserFromConfig()
        {
            if (!File.Exists("config.json")) return null;
            var decryptedJson = DecryptData(File.ReadAllText("config.json"));
            return JsonConvert.DeserializeObject<User>(decryptedJson);
        }

        private static string EncryptData(string data)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            aes.IV = new byte[16];
            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            using var cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using var writer = new StreamWriter(cryptoStream);
            writer.Write(data);
            writer.Close();
            return Convert.ToBase64String(ms.ToArray());
        }

        private static string DecryptData(string encryptedData)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            aes.IV = new byte[16];
            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(Convert.FromBase64String(encryptedData));
            using var cryptoStream = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }
    }
}
