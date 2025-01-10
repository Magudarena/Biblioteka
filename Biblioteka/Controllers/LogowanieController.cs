using Biblioteka.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Biblioteka.Controllers
{
    public class LogowanieController : Controller
    {
        private readonly BibliotekaContext _context;

        public LogowanieController(BibliotekaContext context)
        {
            _context = context;
        }

        public IActionResult Logowanie()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logowanie(LogowanieViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Wyszukanie użytkownika na podstawie e-maila
                var uzytkownik = _context.Uzytkownicy.FirstOrDefault(u => u.Email == model.Email);
                if (uzytkownik != null)
                {
                    // Weryfikacja hasła
                    var hasher = new PasswordHasher<Uzytkownik>();
                    var wynik = hasher.VerifyHashedPassword(null, uzytkownik.Haslo, model.Haslo);

                    if (wynik == PasswordVerificationResult.Success)
                    {
                        // Logowanie zakończone sukcesem
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, uzytkownik.Email),
                            new Claim("Imie", uzytkownik.Imie),
                            new Claim("Nazwisko", uzytkownik.Nazwisko)
                        };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        TempData["Success"] = "Zalogowano pomyślnie!";
                        return RedirectToAction("Index", "Home");
                    }
                }

                // Błąd logowania
                ModelState.AddModelError(string.Empty, "Nieprawidłowy email lub hasło");
            }

            return View(model);
        }

        public async Task<IActionResult> Wyloguj()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["Success"] = "Wylogowano pomyślnie!";
            return RedirectToAction("Index", "Home");
        }
    }
}
