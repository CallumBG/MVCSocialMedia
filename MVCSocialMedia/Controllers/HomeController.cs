using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCSocialMedia.Data;
using MVCSocialMedia.Models;
using System.Diagnostics;

namespace MVCSocialMedia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if(User.Identity.Name == null)
            {
                return _context.Posts != null ?
                          View(await _context.Posts.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
            }
            else
            {
                return _context.Posts != null ?
                          View("~/Views/Posts/Index.cshtml", await _context.Posts.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}