using System.ComponentModel.DataAnnotations;

namespace EntregableLibreria.Models
{
    public class Libro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Titulo { get; set; }

        public string Autor { get; set; }

        [Range(0, 1000, ErrorMessage = "Stock no válido")]
        public int Stock { get; set; }

        // Relación: Un libro puede estar en muchos registros de préstamo
        public virtual ICollection<Prestamo> Prestamos { get; set; }
    }
}