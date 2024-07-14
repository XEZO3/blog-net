using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace blog.Models
{
    public class Users : IdentityUser
    {
        public string Name { get; set; }      
    }
}
