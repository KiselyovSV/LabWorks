using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcCreditApp.Models;

namespace MvcCreditApp.Controllers;

public class HomeController : Controller
{
    private readonly CreditContext db;

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, CreditContext context)
    {
        _logger = logger;
        db = context;
    }

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

    [HttpGet]
    public ViewResult CreateBid()
    {
        GiveCredits();
        GiveBids();
        return View();
    }

    [HttpPost]
    public ViewResult CreateBid(Bid newBid)
    {
        newBid.bidDate = DateTime.Now;
        // ��������� ����� ������ � ��
        db.Bids.Add(newBid);
        // ��������� � �� ��� ���������
        db.SaveChanges();
        ViewBag.Message = "�������, " + newBid.Name + ", �� ����� ������ �����.���� ������ ����� ����������� � ������� 10 ����.";
        GiveCredits();
        GiveBids();
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
