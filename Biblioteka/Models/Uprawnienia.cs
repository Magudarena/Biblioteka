using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Models
{
    public class Uprawnienia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nazwa { get; set; }

        public virtual ICollection<Uzytkownik>? Uzytkownicy { get; set; }
    }
}
