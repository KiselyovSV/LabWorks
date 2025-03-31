using Microsoft.AspNetCore.Mvc;
using WebMVCR1.Models;

namespace WebMVCR1.Controllers
{
    public class HomeController : Controller
    {
        private static PersonRepository db = new PersonRepository();
        public ViewResult Index()
        {
            int hour = DateTime.Now.Hour;
            if (hour < 18) ViewBag.Greeting = hour < 12 ? "Доброе утро!" : "Добрый день!";
            else ViewBag.Greeting = "Добрый вечер!";
            ViewData["Mes"] = "Отличного Вам настроения!";
            return View();
        }

        [HttpGet]
        public ViewResult InputData()
        {
            return View();
        }

        [HttpPost]
        public ViewResult InputData(Person p)
        {
            db.AddResponse(p);
            return View("Hello", p);
        }

        public ViewResult OutputData()
        {
            ViewBag.Pers = db.GetAllResponses;
            ViewBag.Count = db.NumberOfPerson;
            return View("ListPerson");
        }
        public string Index1(string param)
        {
            string comma = param != null ? ", " : "!";
            string Greeting = ModelClass.ModelHello() + comma + param ;
            return Greeting;
        }
    }
}
