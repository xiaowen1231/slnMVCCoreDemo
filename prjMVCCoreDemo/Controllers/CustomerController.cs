using Microsoft.AspNetCore.Mvc;
using prjMVCCoreDemo.Models;
using prjMVCCoreDemo.ViewModels;

namespace prjMVCCoreDemo.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult List(CKeywordViewModel vm)
        {
            IEnumerable<TCustomer> customers = null;
            var db = new DbDemoContext();
            if (string.IsNullOrEmpty(vm.Keyword))
            {
                customers = from c in db.TCustomers
                            select c;
            }
            else
            {
                customers = db.TCustomers.Where(
                c => c.FName.Contains(vm.Keyword) ||
                c.FPhone.Contains(vm.Keyword) ||
                c.FEmail.Contains(vm.Keyword) ||
                c.FAddress.Contains(vm.Keyword));
            }
            return View(customers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TCustomer p)
        {
            var db = new DbDemoContext();
            db.TCustomers.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult Delete(int? id)
        {
            var db = new DbDemoContext();
            var customers = db.TCustomers.FirstOrDefault(c => c.FId == id);
            if (customers != null)
            {
                db.TCustomers.Remove(customers);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public IActionResult Edit(int? id)
        {
            var db = new DbDemoContext();
            var customer = db.TCustomers.FirstOrDefault(c => c.FId.Equals(id));
            if (customer == null)
            {
                return RedirectToAction("List");
            }
            return View(customer);
        }
        [HttpPost]
        public IActionResult Edit(TCustomer customerInForm)
        {
            var db = new DbDemoContext();
            var customerInDb = db.TCustomers.FirstOrDefault(c => c.FId.Equals(customerInForm.FId));
            if (customerInDb != null)
            {
                customerInDb.FName = customerInForm.FName;
                customerInDb.FPhone = customerInForm.FPhone;
                customerInDb.FEmail = customerInForm.FEmail;
                customerInDb.FAddress = customerInForm.FAddress;
                customerInDb.FPassword = customerInForm.FPassword;

                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
