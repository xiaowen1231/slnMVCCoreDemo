using Microsoft.AspNetCore.Mvc;
using prjMauiDemo.Models;

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
    }
}
