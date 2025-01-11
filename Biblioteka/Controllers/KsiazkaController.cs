using Biblioteka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
                try
                {
                    _context.NowaKsiazka.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("UNIQUE KEY"))
                    {
                        ModelState.AddModelError(string.Empty, "Numer biblioteczny został już zarezerwowany. Wybierz inny numer.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Wystąpił błąd podczas zapisywania zmian w bazie danych.");
                    }
                }
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


        [HttpGet]
        public IActionResult Edytuj(int id)
        {
            // Pobierz książkę z bazy danych
            var ksiazka = _context.NowaKsiazka.FirstOrDefault(k => k.Id == id);
            if (ksiazka == null)
            {
                return NotFound();
            }

            // Pobierz listę kategorii z bazy danych
            var kategorie = _context.Kategoria.Select(k => new SelectListItem
            {
                Value = k.Id.ToString(),
                Text = k.Nazwa
            }).ToList();

            // Przekaż listę kategorii do widoku
            ViewBag.Kategorie = kategorie;

            return View(ksiazka); // Przekaż książkę do widoku
        }

        [HttpPost]
        public IActionResult Edytuj(Ksiazka model)
        {
            if (ModelState.IsValid)
            {
                // Pobierz książkę z bazy danych
                var ksiazka = _context.NowaKsiazka.FirstOrDefault(k => k.Id == model.Id);
                if (ksiazka == null)
                {
                    return NotFound();
                }

                // Aktualizacja danych książki
                ksiazka.Nr_biblioteczny = model.Nr_biblioteczny;
                ksiazka.Tytul = model.Tytul;
                ksiazka.Autor = model.Autor;
                ksiazka.ISBN = model.ISBN;
                ksiazka.Kategoria = model.Kategoria;
                ksiazka.Dostepna = model.Dostepna;

                _context.SaveChanges(); // Zapisz zmiany w bazie danych
                return RedirectToAction("Index");
            }

            return View(model); // Wyświetl formularz z błędami walidacji
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

            // Pobierz książkę z mapowania NowaKsiazka
            var ksiazka = _context.NowaKsiazka
                .FirstOrDefault(k => k.Id == wypozyczenie.Id_Ksiazka);

            if (ksiazka == null)
            {
                return NotFound("Nie znaleziono książki w bazie danych.");
            }

            // Przekaż dane do widoku
            ViewBag.Wypozyczenie = wypozyczenie;
            ViewBag.Ksiazka = ksiazka;

            return View();
        }



        [HttpPost]
        public IActionResult ZwrocPotwierdzenie(int id_wypozyczenia)
        {
            // Znajdź wypożyczenie na podstawie ID
            var wypozyczenie = _context.Wypozyczenia
                .FirstOrDefault(w => w.Id == id_wypozyczenia && w.Data_Zwrotu == null);

            if (wypozyczenie == null)
            {
                return NotFound("Nie znaleziono wypożyczenia lub książka została już zwrócona.");
            }

            // Ustaw datę zwrotu
            wypozyczenie.Data_Zwrotu = DateTime.Now;

            // Znajdź książkę z mapowania NowaKsiazka i ustaw jako dostępną
            var ksiazka = _context.NowaKsiazka.FirstOrDefault(k => k.Id == wypozyczenie.Id_Ksiazka);
            if (ksiazka != null)
            {
                ksiazka.Dostepna = true;
            }

            // Zapisz zmiany
            _context.SaveChanges();

            TempData["Message"] = "Zwrot książki został pomyślnie zarejestrowany.";
            return RedirectToAction("KsiazkiKlienta", "Klienci", new { id = wypozyczenie.Id_Klient });
        }
    }
}