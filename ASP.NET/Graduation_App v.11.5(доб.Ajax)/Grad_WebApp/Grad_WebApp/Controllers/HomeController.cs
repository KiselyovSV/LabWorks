using System.Diagnostics;
using Grad_WebApp.Data;
using Grad_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Grad_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly FitnessDbContext db;

        public HomeController(ILogger<HomeController> logger, FitnessDbContext context)
        {
            _logger = logger;
            db = context;

        }

        public IActionResult Index()
        {
            var timetbls = db.Timetables.Include(c=>c.Coach).Include(w=>w.Workout).ToList();
            ViewData["Timetables"] = timetbls;
            return View();
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
