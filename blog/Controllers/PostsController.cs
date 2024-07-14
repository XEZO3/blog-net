using blog.IService;
using blog.Models;
using blog.Models.VM;
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
            return View(data.result);
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
        public async Task<IActionResult> Create(PostVM post)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    Posts P = new Posts { Content = post.Content, ImageUrl = post.ImageUrl, UserId = userId };

                    var result = await _postService.Create(P);
                    if (result.IsSuccess)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View();
                    }
                }
                return View(post);
            }
            catch
            {
                return View(post);
            }
        }
        [HttpGet("Posts/UserPosts/{id?}")]
        public async Task<IActionResult> UserPosts(string id) {
            Response<IEnumerable<Posts>> response;
            IEnumerable<Posts> posts;
            if (id != null)
            {

                response = await _postService.GetAll(x => x.UserId == id);
                posts = response.result;
                return View(posts);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            response = await _postService.GetAll(x=>x.UserId == userId);
            posts = response.result;
            return View(posts);
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
