using System;

namespace Biblioteka.Models
{
    public class Wypozyczenie
    {
        public int Id { get; set; }
        public int? Id_Ksiazka { get; set; }
        public int? Id_Klient { get; set; }
        public DateTime? Data_Wypozyczenia { get; set; }
        public DateTime? Data_Zwrotu { get; set; }
    }
}