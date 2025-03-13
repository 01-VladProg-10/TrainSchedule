using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Npgsql;
using TrainSchedule.Models;
using System.Windows;

namespace TrainSchedule.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private User _user;
        private string _errorMessage = string.Empty;
        private bool _isLoggingIn = false;

        public LoginViewModel()
        {
            _user = LoadUserFromConfig();  // Załaduj dane z pliku konfiguracyjnego, jeśli istnieją
        }

        public User User
        {
            get => _user;
            set
            {
                _user = value;
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

        public void LoginUser()
        {
            ErrorMessage = string.Empty;

            if (User == null || string.IsNullOrEmpty(User.Email) || string.IsNullOrEmpty(User.Password))
            {
                ErrorMessage = "Please enter both email and password.";
                return;
            }

            IsLoggingIn = AuthenticateUser(User);

            if (IsLoggingIn)
            {
                SaveUserToConfig(User);
            }
            else
            {
                ErrorMessage = "Login failed. Please check your email or password.";
            }
        }

        private bool AuthenticateUser(User user)
        {
            const string connectionString = "Host=localhost;Username=postgres;Password=polishchuk;Database=Trackly";

            try
            {
                using var conn = new NpgsqlConnection(connectionString);
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    "SELECT COUNT(*) FROM users WHERE email = @Email AND password = @Password", conn);

                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password); 

                var result = (long)cmd.ExecuteScalar();

                if (result > 0)
                {
                    return true;  
                }
                else
                {
                    return false;  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        // Zapisz dane użytkownika w pliku config.json
        public static void SaveUserToConfig(User user)
        {
            var configFilePath = "config.json";
            var json = JsonConvert.SerializeObject(user);
            File.WriteAllText(configFilePath, json);
        }

        // Załaduj dane użytkownika z pliku config.json
        public static User LoadUserFromConfig()
        {
            var configFilePath = "config.json";

            if (File.Exists(configFilePath))
            {
                var json = File.ReadAllText(configFilePath);
                return JsonConvert.DeserializeObject<User>(json);
            }

            return null;  // Jeśli plik nie istnieje, zwróć null
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
