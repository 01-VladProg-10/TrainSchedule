using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace TrainSchedule.Classes
{
    internal class Validation
    {
        public static bool ValidationPhoneNumber(ref string phone)
        {
            // Usuń wszystkie niepotrzebne znaki (np. spacje, myślniki)
            phone = phone.Replace(" ", "").Replace("-", "");

            // Sprawdź, czy numer ma tylko cyfry
            if (!phone.All(char.IsDigit))
            {
                MessageBox.Show("Phone number can only contain digits.");
                return false; // Niepoprawny numer
            }

            if (phone == "111111111")
            {
                MessageBox.Show("Phone number can not be empty!");
                return false; // Niepoprawny numer
            }

            // Sprawdź, czy numer ma odpowiednią długość (w Polsce numer telefonu ma 9 cyfr)
            if (phone.Length != 9)
            {
                MessageBox.Show("Phone number must be 9 digits.");
                return false; // Niepoprawny numer
            }

            return true; // Numer telefonu poprawny
        }

        public static bool ValidationEmail(ref string email)
        {
            // Wyrażenie regularne do walidacji e-mail
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Sprawdź, czy email pasuje do wzorca
            if (!Regex.IsMatch(email, emailPattern))
            {
                MessageBox.Show("Invalid email address format.");
                return false; // Niepoprawny format emaila
            }

            // Dodatkowa kontrola, czy e-mail nie jest pusty
            if (string.IsNullOrWhiteSpace(email) || email == "example@email.com")
            {
                MessageBox.Show("Email address cannot be empty.");
                return false; // E-mail nie może być pusty
            }

            return true; // E-mail jest poprawny
        }

        public static bool ValidationName(ref string name)
        {
            // Wyrażenie regularne do walidacji imienia (tylko litery, ewentualnie apostrofy i myślniki)
            string namePattern = @"^[a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ'-]+$";

            // Sprawdź, czy imię pasuje do wzorca
            if (string.IsNullOrWhiteSpace(name) || name == "Example")
            {
                MessageBox.Show("Name cannot be empty.");
                return false; // Imię nie może być puste
            }
            else if (!Regex.IsMatch(name, namePattern))
            {
                // Jeśli imię zawiera niedozwolone znaki
                MessageBox.Show("Name can only contain letters, apostrophes, and hyphens.");
                return false; // Niedozwolone znaki w imieniu
            }
            else
            {
                // Jeśli imię jest poprawne, sprawdź długość
                if (name.Length < 2 || name.Length > 50)
                {
                    MessageBox.Show("Name must be between 2 and 50 characters long.");
                    return false; // Imię musi mieć odpowiednią długość
                }
            }

            return true; // Imię jest poprawne
        }
    }
}
