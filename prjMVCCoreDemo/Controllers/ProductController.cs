﻿using Microsoft.AspNetCore.Mvc;
using prjMVCCoreDemo.Models;
using prjMVCCoreDemo.ViewModels;

namespace prjMVCCoreDemo.Controllers
{
    public class ProductController : Controller
    {
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

            var productWrap = products.Select(p => p.toWrap());
            
            return View(productWrap);
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

            return View(query);
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
                    string photoName = Guid.NewGuid().ToString();
                    productInDb.FImagePath = photoName;
                    //productInForm.photo.CopyTo();
                }

                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}