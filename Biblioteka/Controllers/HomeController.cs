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
    }
}
