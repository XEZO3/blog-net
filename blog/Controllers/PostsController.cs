using blog.IService;
using blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace blog.Controllers
{
    [Authorize]

    public class PostsController : Controller
    {

        private readonly IPostService _postService;
        private readonly UserManager<Users> _userManager;

        public PostsController(IPostService postService, UserManager<Users> userManager)
        {
            _postService = postService;
            _userManager = userManager;
        }
        // GET: PostsController
        public async Task<IActionResult> Index()
        {
            var data = await _postService.GetAll();
            return View(data);
        }

        // GET: PostsController/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Posts post)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                post.UserId = user.Id;

                bool result = await _postService.Create(post);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else {
                    return View();
                }
                
            }
            catch
            {
                return View();
            }
        }
        [HttpGet("Posts/UserPosts/{id?}")]
        public async Task<IActionResult> UserPosts(string id) {
            IEnumerable<Posts> data;
            if (id != null)
            {
                
                data = await _postService.GetAll(x => x.UserId == id);
                return View(data);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            data = await _postService.GetAll(x=>x.UserId == userId);
            return View(data);
        }

        // GET: PostsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
