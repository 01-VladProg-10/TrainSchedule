using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Npgsql;
using TrainSchedule.Models;
using System.Windows;

namespace TrainSchedule.ViewModels
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private User _user;
        private string _errorMessage = string.Empty;
        private bool _isRegistering = false;

        public RegistrationViewModel(User user)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
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

        public bool IsRegistering
        {
            get => _isRegistering;
            set
            {
                _isRegistering = value;
                OnPropertyChanged();
            }
        }

        public void RegisterUser()
        {
            ErrorMessage = string.Empty;

            if (User == null)
            {
                ErrorMessage = "User data is missing.";
                return;
            }

            IsRegistering = SaveUserToDatabase(User);
            if (!IsRegistering)
            {
                ErrorMessage = "Registration failed. Please try again.";
            }
        }

        public static bool SaveUserToDatabase(User user)
        {
            if (user == null) return false;

            const string connectionString = "Host=localhost;Username=postgres;Password=polishchuk;Database=Trackly";

            try
            {
                using var conn = new NpgsqlConnection(connectionString);
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    "INSERT INTO users (email, password, first_name, last_name, phone_number, ticket_type) " +
                    "VALUES (@Email, @Password, @FirstName, @LastName, @PhoneNumber, @TicketType)", conn);

                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
