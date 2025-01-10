using Microsoft.AspNetCore.Mvc;
using Biblioteka.Models;
using System.Linq;

namespace Biblioteka.Controllers
{
    public class WypozyczeniaController : Controller
    {
        private readonly BibliotekaContext _context;

        public WypozyczeniaController(BibliotekaContext context)
        {
            _context = context;
        }

        // Wyświetlanie formularza do wyszukiwania książki
        public IActionResult Index()
        {
            return View("Wypozyczenia");
        }

        // Wyszukiwanie książki po nr bibliotecznym
        [HttpPost]
        public IActionResult SzukajKsiazke(string nrBiblioteczny)
        {
            var ksiazka = _context.NowaKsiazka.FirstOrDefault(k => k.Nr_biblioteczny == nrBiblioteczny && k.Dostepna);
            if (ksiazka == null)
            {
                ViewBag.Message = "Książka niedostępna lub nie istnieje.";
                return View("Wypozyczenia");
            }
            return View("PodsumowanieKsiazki", ksiazka);
        }

        [HttpPost]
        public IActionResult SzukajKlienta(string telefon)
        {

            // Sprawdzenie, czy pole telefon nie jest puste
            if (string.IsNullOrWhiteSpace(telefon))
            {
                ViewBag.Message = "Podaj numer telefonu.";
                return View("PodsumowanieKsiazki");
            }

            // Wyszukiwanie klientów po telefonie
            var klient = _context.Klient.FirstOrDefault(k => k.Telefon == telefon);

            // Sprawdzamy, czy klient został znaleziony
            if (klient == null)
            {
                ViewBag.Message = "Nie znaleziono klienta o tym numerze telefonu.";
                return View("PodsumowanieKsiazki");
            }

            // Przekazanie wyniku do widoku
            return View("WybierzKlienta", klient);
        }





        // Wypożyczenie książki
        [HttpPost]
        public IActionResult Wypozycz(int ksiazkaId, int klientId)
        {
            var wypozyczenie = new Wypozyczenie
            {
                Id_Ksiazka = ksiazkaId,
                Id_Klient = klientId,
                Data_Wypozyczenia = DateTime.Now
            };
            _context.Wypozyczenia.Add(wypozyczenie);

            var ksiazka = _context.NowaKsiazka.First(k => k.Id == ksiazkaId);
            ksiazka.Dostepna = false;

            _context.SaveChanges();

            return RedirectToAction("KsiazkiKlienta", "Klienci", new { id = klientId });
        }
    }
}
