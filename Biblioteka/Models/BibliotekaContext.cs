﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<LsitaKlientow> ListaKlientow { get; set; }
        public DbSet<ListaKsiazek> Ksiazka { get; set; }
        public DbSet<Klient> Klient { get; set; }
        public DbSet<Ksiazka> NowaKsiazka { get; set; }
        public DbSet<Kategoria> Kategoria { get; set; }
        public DbSet<KsiazkaPerKlient> KsiazkaPerKlient { get; set; }
        public DbSet<Wypozyczenie> Wypozyczenia { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
