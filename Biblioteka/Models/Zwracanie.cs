using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Models
{
    public class Zwracanie
    {
        [Required(ErrorMessage = "Numer biblioteczny jest wymagany.")]
        public string Nr_Biblioteczny { get; set; }
    }
}
