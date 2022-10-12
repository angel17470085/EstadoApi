using System.ComponentModel.DataAnnotations;

namespace AppEstados.Models
{
    public class Municipio
    {

        public int Id { get; set; }

        [Required]
        public  string? Name { get; set; }
    }
}
