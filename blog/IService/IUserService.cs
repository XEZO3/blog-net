using blog.Models;
using Microsoft.AspNetCore.Identity;

namespace blog.IService
{
    public interface IUserService
    {
        public Task<string> login(string email, string password);
        public void logout();
        public Task<string> register(string name, string email, string password);
    }
}
