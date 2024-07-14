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
        public async Task<Response<Users>> login(string username, string password)
        {
            Response<Users> response = new Response<Users>();
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
            if (result.Succeeded)
            {
                response.IsSuccess = true;
            }
            else { 
                response.IsSuccess =false;
                response.Message = "username or password is incorrect";
            }
            return response;
            
        }
        public async void logout() {
            await _signInManager.SignOutAsync();
        }
        public async Task<Response<Users>> register(string name,string email, string password)
        {
            Response<Users> response = new Response<Users>();
            var user = new Users {Email = email ,Name = name,UserName = email,EmailConfirmed = true};
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                response.IsSuccess = true;
            }
            else {
                response.IsSuccess = false;
                foreach (var error in result.Errors)
                {
                    response.Message += error.Description;
                }
            }
            
           
            return response;                      
        }
    }
}
