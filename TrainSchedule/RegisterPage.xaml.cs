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
using TrainSchedule.ViewModels;
using static TrainSchedule.Classes.Validation;
using static TrainSchedule.ViewModels.RegistrationViewModel;
using TrainSchedule.Models;


namespace TrainSchedule
{
    /// <summary>
    /// Logika interakcji dla klasy RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void GoToLoginPage(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LogInPage());
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string? email = EmailTextBox.Text;
            string? password = PasswordBox.Password;
            string? confirmPassword = ConfirmPasswordBox.Password;
            string? firstName = FirstNameTextBox.Text;
            string? lastName = LastNameTextBox.Text;
            string? phoneNumber = PhoneNumberTextBox.Text;
            string? ticketType = (TicketTypeComboBox.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content?.ToString();

            bool isValid = true;
            string errorMessage = "";

            if (!ValidationEmail(ref email))
            {
                isValid = false;
                errorMessage += "Invalid email.\n";
            }

            if (!ValidationPhoneNumber(ref phoneNumber))
            {
                isValid = false;
                errorMessage += "Invalid phone number.\n";
            }

            if (password != confirmPassword || string.IsNullOrEmpty(password))
            {
                isValid = false;
                errorMessage += "Invalid passwords.\n";
            }

            if (!ValidationName(ref firstName) || !ValidationName(ref lastName))
            {
                isValid = false;
                errorMessage += "Invalid name or last name.\n";
            }

            if (ticketType == null)
            {
                isValid = false;
                errorMessage += "You need to select a type of ticket.\n";
            }

            if (isValid)
            {
                User user = new User { Email = email, Password = password, 
                    FirstName = firstName, LastName = lastName, PhoneNumber = phoneNumber, TicketType = ticketType };

                RegistrationViewModel viewModel = new RegistrationViewModel(user);
                viewModel.RegisterUser();
                if (viewModel.IsRegistering == true)
                {
                    MessageBox.Show("Registration successful!");
                    ErrorTextBlock.Text = "";
                    this.NavigationService.Navigate(new LogInPage());
                }
            }
            else
            {
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

        private void FirstNameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Example")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void FirstNameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Example";
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void LastNameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Example")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void LastNameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Example";
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void PhoneNumberBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "111111111")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void PhoneNumberBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "111111111";
                textBox.Foreground = Brushes.Gray;
            }
        }

    }
}
