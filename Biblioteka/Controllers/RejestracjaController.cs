using Biblioteka.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Controllers
{
    public class RejestracjaController : Controller
    {
        private readonly BibliotekaContext _context;
        public RejestracjaController(BibliotekaContext context)
        {
            _context = context;
        }

        public IActionResult Rejestracja()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Rejestracja(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Tworzenie nowego użytkownika na podstawie modelu
                var nowyUzytkownik = new Uzytkownik
                {
                    Imie = model.Imie,
                    Nazwisko = model.Nazwisko,
                    Email = model.Email,
                    Haslo = HashPassword(model.Haslo), // Hashowanie hasła
                    Id_Uprawnienia = 4 // Opcjonalnie można przypisać domyślny poziom uprawnień
                };

                // Dodanie użytkownika do bazy danych
                _context.Uzytkownicy.Add(nowyUzytkownik);
                _context.SaveChanges();

                // Przekierowanie do strony logowania lub innej
                return RedirectToAction("Logowanie", "Logowanie");
            }

            return View(model); // Powrót do widoku z błędami walidacji
        }

        private string HashPassword(string password)
        {
            var hasher = new PasswordHasher<Uzytkownik>();
            return hasher.HashPassword(null, password);
        }

    }
}
