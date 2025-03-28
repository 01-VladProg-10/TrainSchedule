using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TrainSchedule.Models
{
    public class TrainModel
    {
        private static readonly HashSet<string> AllowedCities = new()
        {
            "Warszawa", "Krakow", "Lodz", "Wroclaw", "Poznan", "Gdansk", "Szczecin", "Bydgoszcz", "Lublin", "Katowice",
            "Bialystok", "Gdynia", "Czestochowa", "Radom", "Sosnowiec", "Torun", "Kielce", "Gliwice", "Zabrze", "Bytom",
            "Bielsko-Biala", "Olsztyn", "Rybnik", "Prudnik", "Przemysl", "Opole"
        };

        // Identifikator pociągu w bazie danych
        public int TrainId { get; set; } = 0; // Domyślnie 0 dla nowego pociągu

        [Required(ErrorMessage = "Nazwa pociągu jest wymagana.")]
        public string TrainName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Stacja początkowa jest wymagana.")]
        public string StartStation { get; set; } = string.Empty;

        [Required(ErrorMessage = "Stacja końcowa jest wymagana.")]
        public string FinishStation { get; set; } = string.Empty;

        [Required(ErrorMessage = "Godzina odjazdu jest wymagana.")]
        [RegularExpression(@"^([01]\d|2[0-3]):([0-5]\d)$", ErrorMessage = "Niepoprawny format godziny (HH:mm).")]
        public string Departure { get; set; } = string.Empty;

        [Required(ErrorMessage = "Godzina przyjazdu jest wymagana.")]
        [RegularExpression(@"^([01]\d|2[0-3]):([0-5]\d)$", ErrorMessage = "Niepoprawny format godziny (HH:mm).")]
        public string Arrival { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cena jest wymagana.")]
        public string Price { get; set; } = string.Empty;

        // Walidacja obiektu
        public bool Validate(out string errorMessage)
        {
            var context = new ValidationContext(this);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(this, context, results, true);

            if (!isValid)
            {
                errorMessage = string.Join("\n", results.Select(r => r.ErrorMessage));
                return false;
            }

            if (StartStation == FinishStation)
            {
                errorMessage = "Stacja początkowa i końcowa nie mogą być takie same.";
                return false;
            }

            if (!AllowedCities.Contains(StartStation) || !AllowedCities.Contains(FinishStation))
            {
                errorMessage = "Niepoprawna stacja. Wybierz miasto z listy.";
                return false;
            }

            if (!decimal.TryParse(Price, out decimal priceValue) || priceValue <= 0)
            {
                errorMessage = "Cena musi być liczbą większą od 0.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }
    }
}
