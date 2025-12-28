using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntregableLibreria.Models
{
    public class Prestamo
    {
        [Key]
        public int Id { get; set; }

        public DateTime FechaPrestamo { get; set; } = DateTime.Now;

        public DateTime? FechaDevolucion { get; set; } // Puede ser nulo si aún no lo devuelve

        // Claves foráneas (Foreign Keys)
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        [ForeignKey("Libro")]
        public int LibroId { get; set; }
        public virtual Libro Libro { get; set; }
    }
}