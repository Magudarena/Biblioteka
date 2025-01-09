using Biblioteka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Biblioteka.Controllers
{
    public class KsiazkiController : Controller
    {
        private readonly BibliotekaContext _context;

        public KsiazkiController(BibliotekaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Pobierz listę książek z bazy danych
            var ksiazki = _context.Ksiazka.ToList();
            return View("ListaKsiazek", ksiazki);
        }

        [HttpGet]
        public IActionResult Dodaj()
        {
            var kategorie = _context.Kategoria.ToList();
            ViewBag.Kategorie = new SelectList(kategorie, "Id", "Nazwa");
            return View("NowaKsiazka");
        }

        // POST: Obsługuje zapis danych
        [HttpPost]
        public IActionResult Dodaj(Ksiazka model)
        {
            if (ModelState.IsValid)
            {
                model.Dostepna = true; // Domyślnie ustaw dostępność na true
                _context.NowaKsiazka.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var kategorie = _context.Kategoria.ToList();
            ViewBag.Kategorie = new SelectList(kategorie, "Id", "Nazwa");
            return View("NowaKsiazka", model);
        }

        // GET: Potwierdzenie usunięcia książki
        [HttpGet]
        public IActionResult Usun(int id)
        {
            // Pobierz książkę z tabeli NowaKsiazka
            var ksiazka = _context.NowaKsiazka.FirstOrDefault(k => k.Id == id);
            if (ksiazka == null)
            {
                return NotFound();
            }

            return View(ksiazka); // Przekazanie modelu do widoku
        }

        [HttpPost]
        public IActionResult UsunPotwierdzenie(int id)
        {
            // Pobierz książkę z tabeli NowaKsiazka
            var ksiazka = _context.NowaKsiazka.FirstOrDefault(k => k.Id == id);
            if (ksiazka == null)
            {
                return NotFound();
            }

            _context.NowaKsiazka.Remove(ksiazka); // Usuń książkę z tabeli
            _context.SaveChanges(); // Zapisz zmiany w bazie danych

            return RedirectToAction("Index"); // Przekierowanie na listę książek
        }
    }
}