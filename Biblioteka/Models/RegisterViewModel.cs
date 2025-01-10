﻿using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Proszę podaj imię")]
        public string Imie { get; set; }

        [Required(ErrorMessage = "Proszę podaj nazwisko")]
        public string Nazwisko { get; set; }

        [Required(ErrorMessage = "Proszę podaj email")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Proszę podaj hasło")]
        [MinLength(6, ErrorMessage = "Hasło musi mieć co najmniej 6 znaków")]
        public string Haslo { get; set; }

        [Required(ErrorMessage = "Proszę potwierdź hasło")]
        [Compare("Haslo", ErrorMessage = "Hasła nie są zgodne")]
        public string ConfirmHaslo { get; set; }
    }
}