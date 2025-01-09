using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Biblioteka.Models
{
    public class BibliotekaContext : DbContext
    {
        public BibliotekaContext(DbContextOptions<BibliotekaContext> options)
            : base(options)
        {
        }

        // Zdefiniuj DbSet dla każdej tabeli w bazie danych
        public DbSet<Klient> Klient { get; set; }
        public DbSet<Ksiazka> Ksiazka { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            // Wymuszenie nazwy tabeli "klient" dla klasy Klient
            modelBuilder.Entity<Klient>().HasNoKey().ToTable("ListaKlientow");

            // Wymuszenie nazwy tabeli "ksiazka" dla klasy Ksiazka
            modelBuilder.Entity<Ksiazka>().HasNoKey().ToTable("ksiazka");
        }
    }
}
