using System.ComponentModel.DataAnnotations;

namespace EntregableLibreria.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; } 

        public bool Activo { get; set; } = true;

        // Relación: Un usuario puede tener muchos préstamos
        public virtual ICollection<Prestamo> Prestamos { get; set; }
    }
}