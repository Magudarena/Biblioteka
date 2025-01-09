using Biblioteka.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Controllers
{
    public class KsiazkaControler : Controller
    {
        private readonly BibliotekaContext _context;

        public KsiazkaControler(BibliotekaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var klienci = _context.Ksiazka.ToList(); // Pobierz listę Ksiazek z bazy danych
            return View("ListaKsiazek", klienci); // Wskaż widok ListaKsiazek
        }
    }
}
