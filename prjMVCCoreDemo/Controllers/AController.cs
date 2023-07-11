using Microsoft.AspNetCore.Mvc;
using prjMauiDemo.Models;
using prjMVCCoreDemo.Models;
using System.Text.Json;

namespace prjMVCCoreDemo.Controllers
{
    public class AController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string sayHello()
        {
            return "Hello,ASP.NET Core MVC.";
        }

        public string lotto()
        {
            return new CLotto().getNumber();
        }

        public IActionResult queryById(int? id)
        {
            ViewBag.id = id;
            return View();
        }

        public IActionResult showCountBySession()
        {
            int count = 0;
            if (HttpContext.Session.Keys.Contains("count"))
            {
                count = (int)HttpContext.Session.GetInt32("count");
            }
            count++;
            HttpContext.Session.SetInt32("count", count);
            ViewBag.count = count;
            return View();
        }
        public string demoObject2Json()
        {
            TCustomer customer = new TCustomer();
            customer.FId = 1;
            customer.FName = "John";
            customer.FPhone = "0912345678";
            customer.FEmail = "john@gmail.com";
            customer.FAddress = "台北市";
            customer.FPassword = "1234";

            string json = JsonSerializer.Serialize(customer);
            return json;
        }
        public string demoJson2Object()
        {
            string json = demoObject2Json();
            TCustomer customer = JsonSerializer.Deserialize<TCustomer>(json);
            return customer.FName + "/" + customer.FEmail;
        }
    }
}
