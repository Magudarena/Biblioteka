using System;

namespace Biblioteka.Models
{
    public class KsiazkaPerKlient
    {
        public int? Id_Wypozyczenia { get; set; }
        public int? Id_Klient { get; set; }
        public int? Id_Ksiazka { get; set; }
        public string Nr_Biblioteczny { get; set; }
        public string Tytul { get; set; }
        public string Autor { get; set; }
        public string ISBN { get; set; }
        public DateTime? Data_Wypozyczenia { get; set; }
        public DateTime? Data_Zwrotu { get; set; }
    }

}