using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using blog.Data;
using blog.Models;
using blog.IService;
using blog.Models.VM;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace blog.Controllers
{
    public class UsersController : Controller
    {
        private readonly DBContext _context;
        private readonly IUserService _userService;
        public UsersController(DBContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model )
        {
         
          
            var response = await _userService.register(model.Name, model.Email,model.Password);
            if (response.IsSuccess)
            {
                return RedirectToAction("Login", "Users");

            }
            ModelState.AddModelError("CustomError", response.Message);
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            
            var response = await _userService.login(model.Email, model.Password);
            if (response.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }
            
            ModelState.AddModelError("CustomError", response.Message);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
             _userService.logout();
            return RedirectToAction("Index", "Home");
        }

    }
}
