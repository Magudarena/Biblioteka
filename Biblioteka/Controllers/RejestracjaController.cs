using Biblioteka.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Controllers
{
    [AllowAnonymous]
    public class RejestracjaController : Controller
    {
        private readonly BibliotekaContext _context;
        public RejestracjaController(BibliotekaContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Rejestracja()
        {
            return View();
        }

        [AllowAnonymous]
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

                try
                {
                    // Dodanie użytkownika do bazy danych
                    _context.Uzytkownicy.Add(nowyUzytkownik);
                    _context.SaveChanges();
                    TempData["Success"] = "Konto zostało utworzone pomyślnie. Możesz się teraz zalogować.";
                    return RedirectToAction("Logowanie", "Logowanie");
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("UNIQUE KEY"))
                    {
                        ModelState.AddModelError(string.Empty, "Użytkownik o podanym adresie e-mail już isnieje. Jeżeli nie pamiętasz hasła, skontaktuj się z administratorem.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Wystąpił błąd podczas zapisywania zmian w bazie danych.");
                    }
                }
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
