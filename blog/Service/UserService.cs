using blog.Data;
using blog.IService;
using blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace blog.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public UserService(UserManager<Users> userManager, SignInManager<Users> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<string> login(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
            if (result.Succeeded)
            {
                return "success";
            }
            return "invalid username or password";
        }
        public async void logout() {
            await _signInManager.SignOutAsync();
        }
        public async Task<string> register(string name,string email, string password)
        {
            var user = new Users {Email = email ,Name = name,UserName = email,EmailConfirmed = true};
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return "success";
            }
            Console.WriteLine(result.Errors);
            return ""+result.Errors;                      
        }
    }
}
