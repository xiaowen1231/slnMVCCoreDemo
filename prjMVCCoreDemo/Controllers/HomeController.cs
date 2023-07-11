using Microsoft.AspNetCore.Mvc;
using prjMVCCoreDemo.Models;
using prjMVCCoreDemo.ViewModels;
using System.Diagnostics;
using System.Text.Json;

namespace prjMVCCoreDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SessionKey_LoginCustomer))
                return View();
            return RedirectToAction("Login");
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
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(CLoginViewModel vm)
        {
            var db = new DbDemoContext();
            var customer = db.TCustomers.FirstOrDefault(c => c.FEmail == vm.Account && c.FPassword == vm.Password);
            if (customer != null && customer.FPassword == vm.Password)
            {
                string json = JsonSerializer.Serialize(customer);
                HttpContext.Session.SetString(CDictionary.SessionKey_LoginCustomer, json);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}