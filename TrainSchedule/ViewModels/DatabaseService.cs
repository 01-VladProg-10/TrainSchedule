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
