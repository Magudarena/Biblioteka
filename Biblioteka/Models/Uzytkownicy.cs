using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Models
{
    public class Uzytkownicy
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "Proszę podaj imię")]
        [MinLength(2, ErrorMessage = "Imię musi mieć co najmniej 2 znaki")]
        public string Imie { get; set; }

        [Required(ErrorMessage = "Proszę podaj nazwisko")]
        [MinLength(2, ErrorMessage = "Nazwisko musi mieć co najmniej 2 znaki")]
        public string Nazwisko { get; set; }

        [Required(ErrorMessage = "Proszę podaj adres e-mail")]
        [EmailAddress(ErrorMessage = "Proszę podaj poprawny adres e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Proszę podaj numer telefonu")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Telefon musi zawierać dokładnie 9 cyfr.")]
        public int Id_Uprawnienia { get; set; }


        [Required(ErrorMessage = "Proszę podaj hasło")]
        [MinLength(8, ErrorMessage = "Hasło musi mieć co najmniej 8 znaków")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).+$", ErrorMessage = "Hasło musi zawierać co najmniej jedną dużą literę, jedną małą literę i jedną cyfrę")]
        public string Haslo { get; set; }

        public bool RememberMe { get; set; }


    }
}
