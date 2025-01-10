using Biblioteka.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Controllers
{
    public class ZwracanieController : Controller
    {
        private readonly BibliotekaContext _context;

        public ZwracanieController(BibliotekaContext context)
        {
            _context = context;
        }

        public IActionResult Zwracanie(int id)
        {

            return View();
        }


        [HttpGet]
        public IActionResult Zwroc(int id)
        {
            // Pobierz wypożyczenie
            var wypozyczenie = _context.Wypozyczenia
                .FirstOrDefault(w => w.Id == id && w.Data_Zwrotu == null);

            if (wypozyczenie == null)
            {
                return NotFound("Nie znaleziono wypożyczenia lub książka została już zwrócona.");
            }

            // Pobierz książkę
            var ksiazka = _context.NowaKsiazka
                .FirstOrDefault(k => k.Id == wypozyczenie.Id_Ksiazka);

            if (ksiazka == null)
            {
                return NotFound("Nie znaleziono książki w bazie danych.");
            }

            // Przekazanie danych do widoku
            ViewBag.Wypozyczenie = wypozyczenie;
            ViewBag.Ksiazka = ksiazka;

            return View();
        }

        [HttpPost]
        public IActionResult PrzekierujDoZwrotu([Bind("NrBiblioteczny")] string nrBiblioteczny)
        {
            if (string.IsNullOrEmpty(nrBiblioteczny))
            {
                ViewData["Message"] = "Numer biblioteczny nie został wprowadzony.";
                return View("Zwracanie");
            }

            Console.WriteLine($"Przekazany numer biblioteczny: {nrBiblioteczny}");

            var ksiazka = _context.KsiazkaPerKlient
                .FirstOrDefault(k => k.Nr_Biblioteczny == nrBiblioteczny && k.Data_Zwrotu == null);

            if (ksiazka == null)
            {
                Console.WriteLine("Nie znaleziono książki w bazie danych.");
                ViewData["Message"] = "Nie znaleziono książki do zwrotu lub książka już została zwrócona.";
                return View("Zwracanie");
            }

            Console.WriteLine($"Znaleziono książkę: {ksiazka.Tytul}, ID Wypożyczenia: {ksiazka.Id_Wypozyczenia}");

            // Przekierowanie do widoku Zwroc w kontrolerze Ksiazka
            return RedirectToAction("Zwroc", "Zwracanie", new { id = ksiazka.Id_Wypozyczenia });
        }
    }
}