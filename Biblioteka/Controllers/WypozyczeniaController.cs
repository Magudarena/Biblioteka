using Microsoft.AspNetCore.Mvc;
using Biblioteka.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Controllers
{
    public class WypozyczeniaController : Controller
    {
        private readonly BibliotekaContext _context;

        public WypozyczeniaController(BibliotekaContext context)
        {
            _context = context;
        }

        // Wyświetla widok formularza wyszukiwania książki
        public IActionResult Index()
        {
            return View("Wypozyczenia");
        }

        // Wyszukuje książkę po numerze bibliotecznym
        [HttpPost]
        public IActionResult SzukajKsiazke(string nrBiblioteczny)
        {

            var ksiazka = _context.NowaKsiazka.FirstOrDefault(k => k.Nr_biblioteczny == nrBiblioteczny && k.Dostepna);

            if (ksiazka == null)
            {
                ViewBag.Message = "Nie znaleziono książki lub jest niedostępna.";
                return View("Wypozyczenia");
            }

            Console.WriteLine($"Znaleziono książkę: Id = {ksiazka.Id}, Tytuł = {ksiazka.Tytul}");
            return View("PodsumowanieKsiazki", ksiazka);
        }

        [HttpPost]
        public IActionResult SzukajKlienta(string telefon, int ksiazkaId)
        {

            var klient = _context.Klient.FirstOrDefault(k => k.Telefon == telefon);

            if (klient == null)
            {
                ViewBag.Message = "Nie znaleziono klienta o podanym numerze telefonu.";
                var ksiazka = _context.NowaKsiazka.FirstOrDefault(k => k.Id == ksiazkaId);
                return View("PodsumowanieKsiazki", ksiazka);
            }

            ViewBag.KsiazkaId = ksiazkaId;
            return View("WybierzKlienta", new List<Klient> { klient });
        }

        [HttpPost]
        public IActionResult Wypozycz(int ksiazkaId, int klientId)
        {
                // Znajdź książkę po ID w odpowiednim DbSet
                var ksiazka = _context.NowaKsiazka.FirstOrDefault(k => k.Id == ksiazkaId);


                // Aktualizacja statusu książki
                ksiazka.Dostepna = false;
                _context.NowaKsiazka.Update(ksiazka);

                // Dodanie rekordu do tabeli wypożyczenia
                var wypozyczenie = new Wypozyczenie
                {
                    Id_Ksiazka = ksiazkaId,
                    Id_Klient = klientId,
                    Data_Wypozyczenia = DateTime.Now,
                    Data_Zwrotu = null
                };
                _context.Wypozyczenia.Add(wypozyczenie);

                // Zapisanie zmian w bazie
                _context.SaveChanges();

                // Przekierowanie do widoku Listy książek Klienta
                return RedirectToAction("KsiazkiKlienta", "Klienci", new { id = klientId });
        }

    }
}
