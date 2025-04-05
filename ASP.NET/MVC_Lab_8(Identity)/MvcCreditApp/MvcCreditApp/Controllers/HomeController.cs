using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MvcCreditApp1.Models;

namespace MvcCreditApp1.Controllers;

public class HomeController : Controller
{
    private readonly CreditContext db;

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, CreditContext context)
    {
        _logger = logger;
        db = context;
    }

    [HttpGet]
    [OutputCache(Duration = 60)]
    public IActionResult Index()
    {
        GiveCredits();
        return View();
    }

    private void GiveCredits()
    {
        var allCredits = db.Credits.ToList<Credit>();
        ViewBag.Credits = allCredits;
    }
    private void GiveBids()
    {
        var allBids = db.Bids.ToList<Bid>();
        ViewBag.Bids = allBids;
    }

    [Authorize]
    [HttpGet]
    public ViewResult CreateBid()
    {
        GiveCredits();
        GiveBids();
        return View();
    }

    [HttpPost]
    public string CreateBid(Bid newBid)
    {
        newBid.bidDate = DateTime.Now;
        // Добавляем новую заявку в БД
        db.Bids.Add(newBid);
        // Сохраняем в БД все изменения
        db.SaveChanges();
        return "Спасибо, " + newBid.Name + ", за выбор нашего банка.Ваша заявка будет рассмотрена в течение 10 дней.";
        
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

    // Добавляем метод действия контроллера, выполняющий асинхронную операцию «Извлечение из БД нужной информации в частичное представление»:
    public ActionResult BidSearch()
    {
        string? name = Request.Query["Name"];
        var allBids = db.Bids.Where(a => a.CreditHead!.Contains(name!)).ToList();
        if (allBids.Count == 0)
        {
            return Content("Указанный кредит " + name + " не найден");
            //return HttpNotFound();
        }
        return PartialView(allBids);
    }
}
