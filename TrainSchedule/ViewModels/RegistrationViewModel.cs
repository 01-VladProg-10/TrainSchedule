using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Npgsql;
using TrainSchedule.Models;
using System.Windows;
using System.Windows.Input;
using System.Text;
using System.Security.Cryptography;

namespace TrainSchedule.ViewModels
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private User _user;
        private string _errorMessage = string.Empty;
        private bool _showErrorMessage = false;
        private string _confirmPassword = string.Empty;

        public RegistrationViewModel() : this(new User()) { }

        public RegistrationViewModel(User user)
        {
            _user = user;
            RegisterCommand = new RelayCommand(RegisterUser, CanRegister);
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

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                ShowErrorMessage = !string.IsNullOrEmpty(value);
                OnPropertyChanged();
            }
        }

        public bool ShowErrorMessage
        {
            get => _showErrorMessage;
            set
            {
                _showErrorMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand RegisterCommand { get; }

        private bool CanRegister(object parameter)
        {
            return !string.IsNullOrWhiteSpace(User.Email) &&
                   !string.IsNullOrWhiteSpace(User.Password) &&
                   User.Password == ConfirmPassword &&
                   !string.IsNullOrWhiteSpace(User.FirstName) &&
                   !string.IsNullOrWhiteSpace(User.LastName) &&
                   !string.IsNullOrWhiteSpace(User.PhoneNumber) &&
                   !string.IsNullOrWhiteSpace(User.TicketType);
        }

        private void RegisterUser(object parameter)
        {
            ErrorMessage = string.Empty;

            if (!User.Validate(out string validationError))
            {
                ErrorMessage = validationError;
                return;
            }

            if (SaveUserToDatabase(User))
            {
                MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearFields();
            }
            else
            {
                ErrorMessage = "Registration failed. Please try again.";
            }
        }

        private void ClearFields()
        {
            User.Email = string.Empty;
            User.Password = string.Empty;
            ConfirmPassword = string.Empty;
            User.FirstName = string.Empty;
            User.LastName = string.Empty;
            User.PhoneNumber = string.Empty;
            User.TicketType = string.Empty;
        }

        private static bool SaveUserToDatabase(User user)
        {
            const string connectionString = "Host=localhost;Username=postgres;Password=polishchuk;Database=Trackly";

            try
            {
                using var conn = new NpgsqlConnection(connectionString);
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    "INSERT INTO users (email, password, first_name, last_name, phone_number, ticket_type) " +
                    "VALUES (@Email, @Password, @FirstName, @LastName, @PhoneNumber, @TicketType)", conn);

                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", HashPassword(user.Password)); // Haszowanie hasła
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                cmd.Parameters.AddWithValue("@TicketType", user.TicketType);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        // 🔒 Metoda do haszowania hasła przy użyciu SHA256
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}