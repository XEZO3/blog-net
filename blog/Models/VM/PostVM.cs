using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog.Models.VM
{
    public class PostVM
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public string? UserId { get; set; }
    }
}
