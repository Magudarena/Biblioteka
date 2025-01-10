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
                _context.Klient.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("NowyKlient", model);
        }

        // GET: Potwierdzenie usunięcia klienta
        [HttpGet]
        public IActionResult Usun(int id)
        {
            var klient = _context.Klient.FirstOrDefault(k => k.Id == id);
            if (klient == null)
            {
                return NotFound();
            }

            return View(klient); // Przekazanie obiektu NowyKlient do widoku
        }

        // POST: Usunięcie klienta
        [HttpPost]
        public IActionResult UsunPotwierdzenie(int id)
        {
            var klient = _context.Klient.FirstOrDefault(k => k.Id == id);
            if (klient == null)
            {
                return NotFound();
            }

            _context.Klient.Remove(klient); // Usuń klienta z tabeli
            _context.SaveChanges(); // Zapisz zmiany w bazie danych

            return RedirectToAction("Index"); // Przekierowanie na listę klientów
        }


        [HttpGet]
        public IActionResult Edytuj(int id)
        {
            // Pobranie klienta z bazy danych
            var klient = _context.Klient.FirstOrDefault(k => k.Id == id);
            if (klient == null)
            {
                return NotFound(); // Jeśli klient nie istnieje
            }

            return View(klient); // Przekazanie klienta do widoku
        }

        [HttpPost]
        public IActionResult Edytuj(Klient model)
        {
            if (ModelState.IsValid)
            {
                // Pobierz klienta z bazy danych
                var klient = _context.Klient.FirstOrDefault(k => k.Id == model.Id);
                if (klient == null)
                {
                    return NotFound();
                }

                // Aktualizacja danych klienta
                klient.Imie = model.Imie;
                klient.Nazwisko = model.Nazwisko;
                klient.Telefon = model.Telefon;
                klient.Email = model.Email;

                _context.SaveChanges(); // Zapisz zmiany w bazie danych
                return RedirectToAction("Index"); // Przekierowanie na listę klientów
            }

            return View(model); // Jeśli model jest nieprawidłowy, wyświetl formularz z błędami
        }

        public IActionResult KsiazkiKlienta(int id)
        {
            try
            {
                var ksiazki = _context.KsiazkaPerKlient
                    .Where(k => k.Id_Klient == id)
                    .Select(k => new KsiazkaPerKlient
                    {
                        Id_Wypozyczenia = k.Id_Wypozyczenia,
                        Id_Ksiazka = k.Id_Ksiazka,
                        Id_Klient = k.Id_Klient,
                        Nr_Biblioteczny = k.Nr_Biblioteczny,
                        Tytul = k.Tytul,
                        Autor = k.Autor,
                        ISBN = k.ISBN,
                        Data_Wypozyczenia = k.Data_Wypozyczenia,
                        Data_Zwrotu = k.Data_Zwrotu
                    })
                    .ToList();

                return View(ksiazki);
            }
            catch (Exception)
            {
                ViewBag.Message = "Brak książek przypisanych do tego klienta.";
                return View(new List<KsiazkaPerKlient>());
            }
        }

    }
}