using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebExamApp.Models;
using WebExamApp.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace WebExamApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly StatementDbContext db;

    public HomeController(ILogger<HomeController> logger, StatementDbContext db)
    {
        _logger = logger;
        this.db = db;
    }

    public IActionResult Index()
    {
        var statement = db.Statement.Include(s=>s.Student).Include(l=>l.Lesson).Include(e=>e.Evaluation).ToList();
        ViewBag.Statement = statement;
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
