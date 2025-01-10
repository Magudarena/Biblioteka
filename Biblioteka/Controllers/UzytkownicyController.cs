using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Models;
using Microsoft.AspNetCore.Authorization;

namespace Biblioteka.Controllers
{
    public class UzytkownicyController : Controller
    {
        private readonly BibliotekaContext _context;

        public UzytkownicyController(BibliotekaContext context)
        {
            _context = context;
        }

        // Wyświetlenie listy użytkowników - tylko dla administratorów
        [Authorize]
        public IActionResult Uzytkownicy()
        {
            var uprawnienia = User.FindFirst("Uprawnienia")?.Value;

            if (uprawnienia != "1")
            {
                return Forbid(); // Odmowa dostępu dla osób bez odpowiednich uprawnień
            }

            var uzytkownicy = _context.Uzytkownicy
                .Include(u => u.Uprawnienia)
                .ToList();

            return View("Uzytkownicy", uzytkownicy); // Wywołanie widoku Uzytkownicy.cshtml
        }

    }
}
