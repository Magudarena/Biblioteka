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
            // Wyszukiwanie książki na podstawie numeru bibliotecznego i dostępności
            var ksiazka = _context.NowaKsiazka.FirstOrDefault(k => k.Nr_biblioteczny == nrBiblioteczny && k.Dostepna);

            // Obsługa przypadku, gdy książka nie została znaleziona
            if (ksiazka == null)
            {
                ViewBag.Message = "Książka niedostępna lub nie istnieje.";
                return View("Wypozyczenia");
            }

            // Przekazanie znalezionej książki do widoku PodsumowanieKsiazki
            return View("PodsumowanieKsiazki", ksiazka);
        }

        [HttpPost]
        public IActionResult SzukajKlienta(string telefon, int ksiazkaId)
        {
            if (string.IsNullOrWhiteSpace(telefon))
            {
                ViewBag.Message = "Podaj numer telefonu.";

                // Pobieramy wcześniej wyszukaną książkę, aby ponownie ją przekazać do widoku
                var ksiazka = _context.NowaKsiazka.FirstOrDefault(k => k.Id == ksiazkaId);
                return View("PodsumowanieKsiazki", ksiazka);
            }

            // Wyszukiwanie klienta w bazie danych
            var klient = _context.Klient.FirstOrDefault(k => k.Telefon == telefon);

            if (klient == null)
            {
                // Obsługa przypadku, gdy klient nie został znaleziony
                ViewBag.Message = "Nie znaleziono klienta o tym numerze telefonu.";

                // Pobieramy wcześniej wyszukaną książkę, aby ponownie ją przekazać do widoku
                var ksiazka = _context.NowaKsiazka.FirstOrDefault(k => k.Id == ksiazkaId);
                return View("PodsumowanieKsiazki", ksiazka);
            }

            // Przekazanie klienta do kolejnego widoku (jeśli znaleziony)
            return View("WybierzKlienta", new List<Klient> { klient });
        }

        // Wypożycza książkę dla klienta
        [HttpPost]
        public IActionResult Wypozycz(string nrBiblioteczny, int klientId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // Wyszukiwanie książki po numerze bibliotecznym
                var ksiazka = _context.NowaKsiazka.FirstOrDefault(k => k.Nr_biblioteczny == nrBiblioteczny && k.Dostepna);

                if (ksiazka == null)
                {
                    ViewBag.Message = "Książka jest niedostępna.";
                    return View("Wypozyczenia");
                }

                // Aktualizacja dostępności książki
                ksiazka.Dostepna = false;
                _context.NowaKsiazka.Update(ksiazka);

                // Dodanie wpisu wypożyczenia do tabeli KsiazkaPerKlient
                var wypozyczenie = new KsiazkaPerKlient
                {
                    Id_Ksiazka = ksiazka.Id, // Klucz główny to 'Id'
                    Id_Klient = klientId,
                    Data_Wypozyczenia = DateTime.Now
                };
                _context.KsiazkaPerKlient.Add(wypozyczenie);

                // Zapisanie zmian i zatwierdzenie transakcji
                _context.SaveChanges();
                transaction.Commit();

                return RedirectToAction("KsiazkiKlienta", "Klienci", new { id = klientId });
            }
            catch
            {
                transaction.Rollback();
                ViewBag.Message = "Wystąpił błąd podczas wypożyczania książki.";
                return View("Wypozyczenia");
            }
        }
    }
}
