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
        public async Task<IActionResult> Register([FromForm] IFormCollection form )
        {
            string? name = form["name"];
            string? password = form["password"];
            string? email = form["email"];
          
            var result = await _userService.register(name, email,password);
            if (result == "success")
            {
                return RedirectToAction("Login", "Users");

            }

            return BadRequest(new { Message = result });
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm]IFormCollection model)
        {
            string? email = model["email"];
            string? password = model["password"];
            var result = await _userService.login(email,password);
            if (result == "success")
            {
                return RedirectToAction("Index", "Home");
            }
            
            ModelState.AddModelError(string.Empty, result);
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
