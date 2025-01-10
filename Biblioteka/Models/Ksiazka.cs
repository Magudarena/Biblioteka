using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Models
{
    public class Ksiazka
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Proszę podać nr biblioteczny")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Nr biblioteczny musi mieć dokładnie 10 cyfr")]
        public string Nr_biblioteczny { get; set; }

        [Required(ErrorMessage = "Proszę podać tytuł książki")]
        public string Tytul { get; set; }

        [Required(ErrorMessage = "Proszę podać autora książki")]
        public string Autor { get; set; }

        [Required(ErrorMessage = "Proszę podać numer ISBN")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "ISBN musi mieć dokładnie 13 cyfr")]
        public string ISBN { get; set; }

        public bool Dostepna { get; set; } = true;

        [Required(ErrorMessage = "Proszę wybrać kategorię")]
        public int Kategoria { get; set; }
    }
}
