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
        private string _errorMessage;
        private string _confirmPassword;

        public RegistrationViewModel()
        {
            _user = new User();
            RegisterCommand = new RelayCommand(RegisterUser, CanRegister);
        }

        public User User
        {
            get => _user;
            set { _user = value; OnPropertyChanged(); }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set { _confirmPassword = value; OnPropertyChanged(); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public ICommand RegisterCommand { get; }

        private bool CanRegister(object parameter) =>
            !string.IsNullOrWhiteSpace(User.Email) &&
            !string.IsNullOrWhiteSpace(User.Password) &&
            User.Password == ConfirmPassword;

        private void RegisterUser(object parameter)
        {
            ErrorMessage = string.Empty;
            if (!User.Validate(out string validationError))
            {
                ErrorMessage = validationError;
                return;
            }

            if (DatabaseService.SaveUserToDatabase(User))
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
            User = new User();
            ConfirmPassword = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}