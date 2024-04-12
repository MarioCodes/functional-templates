using System.ComponentModel.DataAnnotations;

namespace EntityFramework.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio")]
        public string NombreCategoria { get; set; }

        [Required(ErrorMessage = "Orden es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El orden debe de ser mayor a cero")]
        public int MostrarOrden { get; set; }
    }
}
