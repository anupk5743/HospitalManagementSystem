using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HMS.Web.Filters
{
    public class AuthFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new RedirectToActionResult(
                    "Login",
                    "Account",
                    null
                    );
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

    }
}
