using blog.Models;
using Microsoft.AspNetCore.Identity;

namespace blog.IService
{
    public interface IUserService
    {
        public Task<Response<Users>> login(string email, string password);
        public void logout();
        public Task<Response<Users>> register(string name, string email, string password);
    }
}
