using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre del producto es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Descripcion corta del producto es requerido")]
        public string DescripcionCorta { get; set; }

        [Required(ErrorMessage = "Descripcion larga del producto es requerido")]
        public string DescripcionLarga { get; set; }

        [Required(ErrorMessage = "Precio del producto es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "El precio debe estar entre los valores aceptados")]
        public double Precio { get; set; }

        public string ImageUrl { get; set; }

        // Foreign Keys
        public int CategoriaId {  get; set; }

        [ForeignKey("CategoriaId")]
        public virtual Categoria? categoria { get; set; }

        public int TipoAplicacionId { get; set; }

        [ForeignKey("TipoAplicacionId")]
        public virtual TipoAplicacion? tipoAplicacion {  get; set; }
    }
}
