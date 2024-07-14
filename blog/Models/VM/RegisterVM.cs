using System.ComponentModel.DataAnnotations;

namespace blog.Models.VM
{
    public class RegisterVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        [MinLength(6)]
        public string Name { get; set; }
    }
}
