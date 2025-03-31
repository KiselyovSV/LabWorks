using Microsoft.AspNetCore.Mvc;
using WebMVCR1.Models;

namespace WebMVCR1.Controllers
{
    public class HomeController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public string Index(string param)
        {
            string comma = param != null ? ", " : "!";
            string Greeting = ModelClass.ModelHello() + comma + param ;
            return Greeting;
        }
    }
}
