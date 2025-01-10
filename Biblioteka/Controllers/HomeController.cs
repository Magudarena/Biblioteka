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








        [HttpGet]
        public IActionResult Rejestracja()
        {
            return View(new Uzytkownicy());
        }


        [HttpPost]
        public IActionResult Rejestracja(Uzytkownicy dane)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Zalogowano pomyślnie";
                return View(dane);
            }
            else return View(dane);
        }











    }
}
