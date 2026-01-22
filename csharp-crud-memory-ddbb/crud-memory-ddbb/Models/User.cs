using System.ComponentModel.DataAnnotations;

namespace crud.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is mandatory")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is mandatory")]
        public string Email { get; set; }
    }
}
