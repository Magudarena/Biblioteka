using Microsoft.AspNetCore.Mvc;
using Biblioteka.Models;
using System.Linq;

namespace Biblioteka.Controllers
{
    public class KategorieController : Controller
    {
        private readonly BibliotekaContext _context;

        public KategorieController(BibliotekaContext context)
        {
            _context = context;
        }

        // GET: Kategorie
        public IActionResult Index()
        {
            var kategorie = _context.Kategoria.ToList();
            return View("Kategorie", kategorie); // Wskazanie widoku Kategorie.cshtml
        }

        [HttpGet]
        public IActionResult Dodaj()
        {
            return View(); // Widok formularza dodawania kategorii
        }

        [HttpPost]
        public IActionResult Dodaj(Kategoria model)
        {
            if (ModelState.IsValid)
            {
                _context.Kategoria.Add(model); // Dodanie nowej kategorii do bazy danych
                _context.SaveChanges(); // Zapisanie zmian w bazie danych
                return RedirectToAction("Index"); // Powrót do listy kategorii
            }

            return View(model); // Jeśli model jest niepoprawny, ponownie wyświetl formularz z błędami walidacji
        }

        [HttpGet]
        public IActionResult Usun(int id)
        {
            var kategoria = _context.Kategoria.FirstOrDefault(k => k.Id == id);
            if (kategoria == null)
            {
                return NotFound(); // Jeśli kategoria nie istnieje
            }

            return View(kategoria); // Przekaż kategorię do widoku
        }

        [HttpPost]
        public IActionResult UsunPotwierdzenie(int id)
        {
            var kategoria = _context.Kategoria.FirstOrDefault(k => k.Id == id);
            if (kategoria == null)
            {
                return NotFound(); // Jeśli kategoria nie istnieje
            }

            _context.Kategoria.Remove(kategoria); // Usuń kategorię z bazy danych
            _context.SaveChanges(); // Zapisz zmiany
            return RedirectToAction("Index"); // Powrót na listę kategorii
        }
    }
}