using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Models
{
    public class Kategoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę kategorii.")]
        [StringLength(40, ErrorMessage = "Nazwa kategorii może mieć maksymalnie 40 znaków.")]
        public string Nazwa { get; set; }
    }
}
