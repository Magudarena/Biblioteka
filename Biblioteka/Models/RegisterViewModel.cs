using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Models
{


    // Model dla tabeli uzytkownicy
    public class Uzytkownik
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Haslo { get; set; }

        public string Imie { get; set; }

        public string Nazwisko { get; set; }

        public int? IdUprawnienia { get; set; }

    }



    // Model dla tabeli uprawnienia
    public class Uprawnienia
    {
        public int Id { get; set; }

        public string Nazwa { get; set; }
    }

    // Model dla tabeli wypozyczenia

}



public class RegisterViewModel
{
    public string Username { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}

public class LoginViewModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}
