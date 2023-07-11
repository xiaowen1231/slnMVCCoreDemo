using Microsoft.AspNetCore.Mvc;
using prjMVCCoreDemo.Models;
using prjMVCCoreDemo.ViewModels;
using System.Text.Json;

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
                string json = "";
                List<CShoppingCartItem> cart = null;
                if(HttpContext.Session.Keys.Contains(CDictionary.SessionKey_ShoppingCart))
                {
                    json = HttpContext.Session.GetString(CDictionary.SessionKey_ShoppingCart);
                    cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(json);
                }
                else
                {
                    cart = new List<CShoppingCartItem>();
                }
                CShoppingCartItem item = new CShoppingCartItem();
                item.price = (decimal)product.FPrice;
                item.productId = vm.fId;
                item.count = vm.fCount;
                item.product = product;
                cart.Add(item);
                json = JsonSerializer.Serialize(cart);
                HttpContext.Session.SetString(CDictionary.SessionKey_ShoppingCart, json);
            }
            return RedirectToAction("List");
        }

        public IActionResult ViewCart()
        {
            if (!HttpContext.Session.Keys.Contains(CDictionary.SessionKey_ShoppingCart))
            {
                return RedirectToAction("List");
            }
            string json = HttpContext.Session.GetString(CDictionary.SessionKey_ShoppingCart) ;
            List<CShoppingCartItem> cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(json);
            if(cart==null)
            {
                return RedirectToAction("List");
            }
            return View(cart);
        }
    }
}
