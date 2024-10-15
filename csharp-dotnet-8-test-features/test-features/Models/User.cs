using System.ComponentModel.DataAnnotations;

namespace testFeatures.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is mandatory")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is mandatory")]
        public string Email { get; set; }
        public string LongEmail { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; }
    }
}
