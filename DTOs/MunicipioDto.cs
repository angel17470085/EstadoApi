using System.ComponentModel.DataAnnotations;

namespace AppEstados.DTOs
{
    public class MunicipioDto
    {
        public int Id { get; set; }


        [Required]
        public string? Nombre { get; set; }

    }
}
