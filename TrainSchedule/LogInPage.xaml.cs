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
        public LogInPage()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.LoginViewModel viewModel)
            {
                viewModel.Password = ((PasswordBox)sender).Password;
            }
        }
        private void OnRegisterLinkClick(object sender, MouseButtonEventArgs e)
        {
            if (NavigationService != null)
            {
                NavigationService.Navigate(new RegisterPage());
            }
        }
    }
}
