using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static TrainSchedule.Classes.Validation;
using TrainSchedule.Models;
using static TrainSchedule.ViewModels.LoginViewModel;
using System.Net.Sockets;
using TrainSchedule.ViewModels;

namespace TrainSchedule
{
    /// <summary>
    /// Logika interakcji dla klasy LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        private void GoToRegistrationPage(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegisterPage());
        }
        public LogInPage()
        {
            InitializeComponent();
        }

        private void OnLoginClick(object sender, RoutedEventArgs e)
        {
            string? email = EmailTextBox.Text;
            string? password = PasswordBox.Password;

            bool isValid = true;
            string errorMessage = "";

            // Walidacja emaila
            if (!ValidationEmail(ref email))
            {
                isValid = false;
                errorMessage += "Invalid email.\n";
            }

            if (isValid)
            {
                // Utworzenie obiektu User z danymi z formularza
                User user = new User
                {
                    Email = email,
                    Password = password
                };

                // Inicjalizacja ViewModel logowania
                LoginViewModel viewModel = new LoginViewModel();
                viewModel.User = user;

                // Próba zalogowania użytkownika
                viewModel.LoginUser();

                if (viewModel.IsLoggingIn)  // Jeśli logowanie jest pomyślne
                {
                    MessageBox.Show("Login successful!");

                    // Zresetowanie ewentualnych komunikatów o błędach
                    ErrorTextBlock.Visibility = Visibility.Hidden;
                }
                else
                {
                    // Jeśli logowanie się nie powiodło
                    ErrorTextBlock.Visibility = Visibility.Visible;
                    ErrorTextBlock.Text = "Invalid email or password.";
                }
            }
            else
            {
                // Jeśli dane nie są poprawne
                ErrorTextBlock.Visibility = Visibility.Visible;
                ErrorTextBlock.Text = errorMessage;
            }
        }


        private void EmailBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "example@email.com")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void EmailBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "example@email.com";
                textBox.Foreground = Brushes.Gray;
            }
        }
    }
}
