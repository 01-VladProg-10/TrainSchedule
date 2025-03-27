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
        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLogin);
            var user = DatabaseService.LoadUserFromConfig();
            if (user != null) Email = user.Email;
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        private void ExecuteLogin(object parameter)
        {
            ErrorMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Please enter both email and password.";
                return;
            }

            var user = DatabaseService.AuthenticateUser(Email, Password);
            if (user != null)
            {
                DatabaseService.SaveEncryptedUserToConfig(user);
                MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                new TrainSearchWindow().Show();
                Application.Current.MainWindow.Close();
            }
            else
            {
                ErrorMessage = "Login failed. Please check your email or password.";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
