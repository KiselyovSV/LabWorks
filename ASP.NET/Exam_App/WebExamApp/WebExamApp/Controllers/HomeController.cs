using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebExamApp.Models;
using WebExamApp.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Collections.Immutable;
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

    public ActionResult Top5Students()
    {
        var sts = (from statement in db.Statement
                   join student in db.Students on statement.StudentId equals student.Id
                   join evaluation in db.Evaluations on statement.EvaluationId equals evaluation.Id
                   group new { statement, student, evaluation } by new { statement.StudentId, student.LastName, student.FirstName } into gr
                   select new
                   {
                       gr.Key.StudentId,
                       gr.Key.LastName,
                       gr.Key.FirstName,
                       avg = (from st in gr
                              select Convert.ToDouble(st.evaluation.Name)).Average()
                   }).OrderBy(a => a.avg).ToList().TakeLast(5);

        ViewData["Mes"] = sts.Count() == 0 ? "Записи о студентах в ведомости отсутствуют" : "Список пяти лучших студентов:";
        ViewBag.sts = sts;
        return PartialView(sts);
    }
   
         public ActionResult  Worst5Students()
         { 
                var sts = (from statement in db.Statement
                           join student in db.Students on statement.StudentId equals student.Id
                           join evaluation in db.Evaluations on statement.EvaluationId equals evaluation.Id
                           group new { statement, student, evaluation } by new { statement.StudentId, student.LastName, student.FirstName } into gr
                           select new
                           {
                               gr.Key.StudentId,
                               gr.Key.LastName,
                               gr.Key.FirstName,
                               avg = (from st in gr
                                      select Convert.ToDouble(st.evaluation.Name)).Average()
                           }).OrderBy(a => a.avg).ToList().Take(5);
                
                ViewData["Mes"] = sts.Count() == 0?"Записи о студентах в ведомости отсутствуют": "Список пяти худших студентов:";
                ViewBag.sts = sts;
                return PartialView(sts);
         }

    


}
