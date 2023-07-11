using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace prjMVCCoreDemo.Models
{
    public class CSuperController:Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (!HttpContext.Session.Keys.Contains(CDictionary.SessionKey_LoginCustomer))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                    (new
                    {
                        controller = "Home",
                        action = "Login"
                    }));
            }
        }
    }
}
