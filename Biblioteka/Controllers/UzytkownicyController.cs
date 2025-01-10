﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Biblioteka.Controllers
{
    public class UzytkownicyController : Controller
    {
        private readonly BibliotekaContext _context;

        public UzytkownicyController(BibliotekaContext context)
        {
            _context = context;
        }

        // Wyświetlenie listy użytkowników - tylko dla administratorów
        [Authorize]
        public IActionResult Uzytkownicy()
        {
            var uprawnienia = User.FindFirst("Uprawnienia")?.Value;

            if (uprawnienia != "1")
            {
                return Forbid();
            }

            var uzytkownicy = _context.Uzytkownicy
                .Include(u => u.Uprawnienia)
                .ToList();

            var listaUprawnien = _context.Uprawnienia.ToList();
            ViewBag.Uprawnienia = new SelectList(listaUprawnien, "Id", "Nazwa");

            return View(uzytkownicy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ZmienUprawnienia(int id, int noweUprawnienia)
        {
            var uzytkownik = _context.Uzytkownicy.Find(id);

            if (uzytkownik != null)
            {
                uzytkownik.Id_Uprawnienia = noweUprawnienia;
                _context.SaveChanges();
            }

            return RedirectToAction("Uzytkownicy");
        }
    }
}
