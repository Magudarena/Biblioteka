using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Models
{
    public class LogowanieModel
    {
        [Required(ErrorMessage = "Proszę podaj email")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Proszę podaj hasło")]
        [MinLength(6, ErrorMessage = "Hasło musi mieć co najmniej 6 znaków")]
        public string Haslo { get; set; }
    }
}
