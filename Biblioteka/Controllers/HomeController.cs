using Biblioteka.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Biblioteka.Controllers
{
    public class HomeController : Controller
    {
        private readonly BibliotekaContext _context;

        public HomeController(BibliotekaContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }







        [HttpGet]
        public IActionResult Wypozyczanie()
        {
            return View(new FormModel());
        }

        [HttpPost]
        public IActionResult Wypozyczanie(FormModel dane)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Książka wypożyczona pomyślnie";
                return View("Wynik", dane);
            }
            else return View(dane);
        }






        [HttpGet]
        public IActionResult Zwracanie()
        {
            return View(new FormModel());
        }

        [HttpPost]
        public IActionResult Zwracanie(FormModel dane)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Książka zwrócona pomyślnie";
                return View("Wynik", dane);
            }
            else return View(dane);
        }





        [HttpGet]
        public IActionResult Logowanie()
        {
            return View(new Uzytkownicy());
        }


        [HttpPost]
        public IActionResult Logowanie(Uzytkownicy dane)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Zalogowano pomyślnie";
                return View(dane);
            }
            else return View(dane);
        }











        [HttpPost]
        public IActionResult Wynik(FormModel model)
        {
            if (ModelState.IsValid)
            {
                var numerBiblioteczny = model.Ksiazka.Nr_biblioteczny;
                var nazwa = model.Ksiazka.Tytul;
                var autor = model.Ksiazka.Autor;
                var isbn = model.Ksiazka.ISBN;

                var imie = model.Klient.Imie;
                var nazwisko = model.Klient.Nazwisko;
                var email = model.Klient.Email;

                return View();
            }

            return View(model);
        }











    }
}
