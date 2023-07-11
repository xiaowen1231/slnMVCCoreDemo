using Microsoft.AspNetCore.Mvc;
using prjMVCCoreDemo.Models;
using prjMVCCoreDemo.ViewModels;

namespace prjMVCCoreDemo.Controllers
{
    public class ProductController : Controller
    {
        private IWebHostEnvironment _environment = null;
        public ProductController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public IActionResult List(CKeywordViewModel vm)
        {
            IEnumerable<TProduct> products = null;
            var db = new DbDemoContext();
            if (string.IsNullOrEmpty(vm.Keyword))
            {
                products = (from c in db.TProducts
                            select c);
            }
            else
            {
                products = db.TProducts.Where(c => c.FName.Contains(vm.Keyword));

            }

            //var productWrap = products.Select(p => p.toWrap());

            List<CProductWrap> list = new List<CProductWrap>();
            foreach(var item in products)
            {
                CProductWrap itemPW = new CProductWrap();
                itemPW.product = item;
                list.Add(itemPW);
            }
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TProduct product)
        {
            if (product != null)
            {
                var db = new DbDemoContext();
                db.TProducts.Add(product);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public ActionResult Delete(int? id)
        {
            var db = new DbDemoContext();
            var query = db.TProducts.FirstOrDefault(p => p.FId == id);
            if (query != null)
            {
                db.TProducts.Remove(query);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("List");

            var db = new DbDemoContext();
            var query = db.TProducts.FirstOrDefault(p => p.FId == id);

            var productWrap = query.toWrap();

            return View(productWrap);
        }

        [HttpPost]
        public ActionResult Edit(CProductWrap productInForm)
        {
            var db = new DbDemoContext();
            var productInDb = db.TProducts.FirstOrDefault(p => p.FId == productInForm.FId);
            if (productInDb != null)
            {
                productInDb.FName = productInForm.FName;
                productInDb.FQty = productInForm.FQty;
                productInDb.FCost = productInForm.FCost;
                productInDb.FPrice = productInForm.FPrice;
                if (productInForm.photo != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    productInDb.FImagePath = photoName;
                    productInForm.photo.CopyTo(
                        new FileStream(_environment.WebRootPath + "/Images/" + photoName
                        , FileMode.Create));
                }

                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
