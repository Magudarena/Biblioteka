using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Models
{
    public class Ksiazka
    {

        public int NumerBiblioteczny { get; set; }

        [Required(ErrorMessage = "Proszę podaj nazwę")]
        [MinLength(2, ErrorMessage = "Nazwa musi mieć co najmniej 2 znaki")]
        public string Tytul { get; set; }

        [Required(ErrorMessage = "Proszę podaj autora")]
        [MinLength(2, ErrorMessage = "Autor musi mieć co najmniej 2 znaki")]
        public string Autor { get; set; }

        [Required(ErrorMessage = "Proszę podaj ISBN")]
        [MinLength(10, ErrorMessage = "ISBN musi mieć co najmniej 10 znaków")]
        public string ISBN { get; set; }

        public string Kategoria { get; set; }

        public string Wypozyczona { get; set; }
    }
}
