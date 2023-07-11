using Microsoft.AspNetCore.Mvc;
using prjMVCCoreDemo.Models;
using prjMVCCoreDemo.ViewModels;

namespace prjMVCCoreDemo.Controllers
{
    public class ShoppingController : Controller
    {
        public IActionResult List()
        {
            var db = new DbDemoContext();
            var datas = from p in db.TProducts
                        select p;

            return View(datas);
        }

        public IActionResult AddToCart(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("List");
            }
            ViewBag.fId = id;
            return View();
        }
        [HttpPost]
        public IActionResult AddToCart(CAddToCartViewModel vm)
        {
            var db = new DbDemoContext();
            var product = db.TProducts.FirstOrDefault(p => p.FId == vm.fId);
            if(product!=null)
            {

            }
            return RedirectToAction("List");
        }
    }
}
