using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog.Models
{
    public class Posts: Main
    {
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        [ForeignKey("Users")]

        public string UserId { get; set; }
        public IdentityUser User { get; set; }

    }
}
