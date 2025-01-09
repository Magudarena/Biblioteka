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
            var klienci = _context.Klient.ToList(); // Pobierz listę klientów z bazy danych
            return View("ListaKlientow", klienci); // Wskaż widok ListaKlientow
        }
    }
}