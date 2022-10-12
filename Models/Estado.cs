using System.ComponentModel.DataAnnotations;

namespace AppEstados.Models
{
    public class Estado
    {
        public int Id { get; set; }

        [Required]
        public string? Nombre { get; set; }
    }
}
