using Microsoft.AspNetCore.Mvc;
using Biblioteka.Models;
using System.Linq;

namespace Biblioteka.Controllers
{
    public class KlienciController : Controller
    {
        private readonly BibliotekaContext _context;

        public KlienciController(BibliotekaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var klienci = _context.ListaKlientow.ToList(); // Pobierz listę klientów z bazy danych
            return View("ListaKlientow", klienci); // Wskaż widok ListaKlientow
        }

        [HttpGet]
        public IActionResult Dodaj()
        {
            return View("~/Views/Klienci/NowyKlient.cshtml");
        }


        [HttpPost]
        public IActionResult Dodaj(Klient model)
        {
            if (ModelState.IsValid)
            {
                // Zapisz nowego klienta w tabeli klient
                _context.NowyKlient.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("NowyKlient", model);
        }

        // GET: Potwierdzenie usunięcia klienta
        [HttpGet]
        public IActionResult Usun(int id)
        {
            // Pobierz książkę z tabeli NowaKsiazka
            var ksiazka = _context.NowaKsiazka.FirstOrDefault(k => k.Id == id);
            if (ksiazka == null)
            {
                return NotFound();
            }

            return View(ksiazka); // Przekazanie obiektu NowaKsiazka do widoku
        }

        // POST: Usunięcie klienta
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