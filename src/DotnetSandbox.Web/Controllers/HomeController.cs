using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DotnetSandbox.Web.Data;
using DotnetSandbox.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetSandbox.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlogContext _context;

        public HomeController(BlogContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var posts = _context.Posts
                .Include(x => x.PostTags)
                .ThenInclude(x => x.Tag)
                .ToArray();

            return View(posts);
        }

        [HttpPost]
        public IActionResult Update(int postId)
        {
            var post = _context.Posts.Single(x => x.Id == postId);

            post.PostTags.Clear();
            post.PostTags.Add(new PostTag
            {
                PostId = 1,
                TagId = 2
            });

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}