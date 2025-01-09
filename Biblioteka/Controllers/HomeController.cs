using Biblioteka.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Biblioteka.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
        public IActionResult Klient()
        {
            return View(new Dane());
        }

        [HttpPost]
        public IActionResult Klient(Dane dane)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Rejestracja zakoñczona pomyœlnie";
                return View("Wynik", dane);
            }
            else return View(dane);
        }
        public IActionResult Wynik(Dane dane)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Rejestracja zakoñczona pomyœlnie";
                return View("Wynik", dane);
            }
            return View(dane);

        }

        [HttpGet]
        public IActionResult Ksiazka()
        {
            return View(new Book());
        }

        [HttpPost]
        public IActionResult Ksiazka(Book dane)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Ksi¹¿ka dodana pomyœlnie";
                return View("Wynik", dane);
            }
            else return View(dane);
        }
    }
}
