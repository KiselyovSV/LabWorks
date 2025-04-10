using System;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MvcCreditApp1.Models;
using Newtonsoft.Json;

namespace MvcCreditApp1.Controllers;

public class HomeController : Controller
{
    private readonly CreditContext db;

    private readonly ILogger<HomeController> _logger;

    private string? jsonResult;

    private string? txtResult;

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
        // ��������� ����� ������ � ��
        db.Bids.Add(newBid);
        // ��������� � �� ��� ���������
        db.SaveChanges();
        return "�������, " + newBid.Name + ", �� ����� ������ �����.���� ������ ����� ����������� � ������� 10 ����.";

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

    // ��������� ����� �������� �����������, ����������� ����������� �������� ����������� �� �� ������ ���������� � ��������� �������������:
    public ActionResult BidSearch()
    {
        string? name = Request.Query["Name"];
        var allBids = db.Bids.Where(a => a.CreditHead!.Contains(name!)).ToList();
        if (allBids.Count == 0)
        {
            return Content("��������� ������ " + name + " �� ������");
            //return HttpNotFound();
        }
        jsonResult = JsonConvert.SerializeObject(allBids, Formatting.Indented);
        txtResult = GreateTxt(allBids);
        SaveResult(jsonResult,"jsonResult.json");
        SaveResult(txtResult,"txtResult.txt");
        return PartialView(allBids);
    }
    public void SaveResult(string content, string fileName)
    {
        string file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,$"E:\\LabWorks\\ASP.NET\\MVC_Lab_8(Identity)\\MvcCreditApp\\MvcCreditApp\\Files\\{fileName}");
        try
        {
            if (System.IO.File.Exists(file_path)) System.IO.File.Delete(file_path);
            using (var W = new StreamWriter(file_path))
            {
                W.Write(content);
                W.Close();
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            throw;
        }
    }

    public IActionResult GetJsonFile()
    {
        // ���� � �����
        string file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"E:/LabWorks/ASP.NET/MVC_Lab_8(Identity)/MvcCreditApp/MvcCreditApp/Files/jsonResult.json");
        // ��� ����� - content-type
        string file_type = "application/octet-stream"; //   ��� ���: string file_type = "text/json"; "application/octet-stream" - ��� ������������� ���
        // ��� ����� - �������������
        string file_name = "jsonResult.json";
        return PhysicalFile(file_path, file_type, file_name);
    }

    public IActionResult GetTxtFile()
    {
        // ���� � �����
        string file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "E:/LabWorks/ASP.NET/MVC_Lab_8(Identity)/MvcCreditApp/MvcCreditApp/Files/txtResult.txt");
        // ��� ����� - content-type
        string file_type = "application/octet-stream"; //   ��� ���: string file_type = "text/json"; "application/octet-stream" - ��� ������������� ���
        // ��� ����� - �������������
        string file_name = "txtResult.txt";
        return PhysicalFile(file_path, file_type, file_name);
    }

    public string GreateTxt(List<Bid> list)
    {
        string text = "";
        foreach (Bid bid in list)
        {
         text += bid.ToString();
        }
        return text;
    }


}
                         


