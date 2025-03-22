using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Npgsql;
using TrainSchedule.Models;
using System.Windows;
using System.Windows.Input;
using System.Security.Cryptography;
using System.Text;

namespace TrainSchedule.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _email;
        private string _password;
        private string _errorMessage;
        private bool _isLoggingIn;
        private const string EncryptionKey = "MySecureKey12345"; // Klucz AES - musi mieć 16 znaków dla AES-128

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLogin);
            LoadUserFromConfig();
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoggingIn
        {
            get => _isLoggingIn;
            set
            {
                _isLoggingIn = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }

        private void ExecuteLogin(object parameter)
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Please enter both email and password.";
                return;
            }

            if (AuthenticateUser(Email, Password))
            {
                // Używamy zaszyfrowanego zapisu danych użytkownika z parametrem `user`
                MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Tworzymy i pokazujemy okno TrainSearchWindow po zalogowaniu
                TrainSearchWindow trainSearchWindow = new TrainSearchWindow();
                trainSearchWindow.Show();

                // Opcjonalnie: zamknij okno logowania
                Application.Current.MainWindow.Close();
            }
            else
            {
                ErrorMessage = "Login failed. Please check your email or password.";
            }
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
        private bool AuthenticateUser(string email, string password)
        {
            const string connectionString = "Host=localhost;Username=postgres;Password=polishchuk;Database=Trackly";

            try
            {
                using var conn = new NpgsqlConnection(connectionString);
                conn.Open();

                // Pobieramy zahaszowane hasło z bazy danych dla podanego emaila
                using var cmd = new NpgsqlCommand(
                    "SELECT password, first_name, last_name, phone_number, ticket_type FROM users WHERE email = @Email", conn);

                cmd.Parameters.AddWithValue("@Email", email);

                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string hashedPasswordInDb = reader.GetString(0); // Zahaszowane hasło z bazy
                    string hashedInputPassword = ComputeSha256Hash(password); // Hashujemy wprowadzone hasło

                    if (hashedPasswordInDb == hashedInputPassword)
                    {
                        var user = new User
                        {
                            Email = email,
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            PhoneNumber = reader.GetString(3),
                            TicketType = reader.GetString(4)
                        };

                        SaveEncryptedUserToConfig(user);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return false;
        }

        private void SaveEncryptedUserToConfig(User user)
        {
            var configFilePath = "config.json";
            var json = JsonConvert.SerializeObject(user);
            var encryptedJson = EncryptData(json);
            File.WriteAllText(configFilePath, encryptedJson);
        }

        private void LoadUserFromConfig()
        {
            var configFilePath = "config.json";

            if (File.Exists(configFilePath))
            {
                var encryptedJson = File.ReadAllText(configFilePath);
                var decryptedJson = DecryptData(encryptedJson);
                var user = JsonConvert.DeserializeObject<User>(decryptedJson);

                if (user != null)
                {
                    Email = user.Email;
                }
            }
        }

        // 🔒 Szyfrowanie AES
        private string EncryptData(string data)
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

        // 🔓 Deszyfrowanie AES
        private string DecryptData(string encryptedData)
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
