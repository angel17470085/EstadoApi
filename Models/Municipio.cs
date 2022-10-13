using System.ComponentModel.DataAnnotations;

namespace AppEstados.Models
{
    public class Municipio
    {

        public int Id { get; set; }

        [Required]
        public  string? Nombre { get; set; }

        //public int EstadoId { get; set; }
        //public Estado? Estado { get; set; }
    }
}
