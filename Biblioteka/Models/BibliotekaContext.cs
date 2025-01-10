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


        public DbSet<LsitaKlientow> ListaKlientow { get; set; }
        public DbSet<ListaKsiazek> Ksiazka { get; set; }
        public DbSet<Klient> Klient { get; set; }
        public DbSet<Ksiazka> NowaKsiazka { get; set; }
        public DbSet<Kategoria> Kategoria { get; set; }
        public DbSet<KsiazkaPerKlient> KsiazkaPerKlient { get; set; }
        public DbSet<Wypozyczenie> Wypozyczenia { get; set; }
        public DbSet<Uzytkownik> Uzytkownicy { get; set; }
        public DbSet<Uprawnienia> Uprawnienia { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Uprawnienia>(entity =>
            {
                entity.ToTable("uprawnienia");
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<Uzytkownik>(entity =>
            {
                entity.ToTable("uzytkownicy");
                entity.HasKey(u => u.Id);
                entity.HasOne(u => u.Uprawnienia)
                      .WithMany(p => p.Uzytkownicy)
                      .HasForeignKey(u => u.Id_Uprawnienia);
            });

            modelBuilder.Entity<Wypozyczenie>(entity =>
            {
                entity.ToTable("wypozyczenia"); // Nazwa tabeli w bazie danych
                entity.HasKey(w => w.Id); // Definicja klucza głównego
            });

            modelBuilder.Entity<KsiazkaPerKlient>(entity =>
            {
                entity.HasNoKey(); // Widok jest bezkluczowy
                entity.ToView("KsiazkaPerKlient"); // Mapowanie do widoku
            });

            // Mapowanie dla klasy NowaKsiazka
            modelBuilder.Entity<Ksiazka>().ToTable("ksiazka"); // Tabela używana do zapisu książek

            // Wymuszenie nazwy tabeli "ListaKlientow" dla klasy Klient
            modelBuilder.Entity<LsitaKlientow>().HasNoKey().ToTable("ListaKlientow");

            // Wymuszenie nazwy tabeli "ListaKsiazek" dla klasy Ksiazka
            modelBuilder.Entity<ListaKsiazek>().HasNoKey().ToTable("ListaKsiazek");

            // Tabela używana do zapisu klientów
            modelBuilder.Entity<Klient>().ToTable("klient").HasKey(k => k.Id);

            modelBuilder.Entity<Kategoria>().ToTable("kategoria");
        }
    }
}
