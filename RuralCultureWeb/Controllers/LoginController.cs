using Microsoft.AspNetCore.Mvc;

namespace RuralCultureWeb.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            string Account = string.Empty;
            string Password = string.Empty;
            if (Request.ContentType == "application/x-www-form-urlencoded")
            {
                if (Request.Form != null && Request.Form.Count > 0)
                {
                    if (Request.Form.ContainsKey("Account"))
                        Account = Request.Form["Account"];
                    if (Request.Form.ContainsKey("Password"))
                        Password = Request.Form["Password"];
                    CacheHelper.CacheInsertAddMinutes("Account", Account, 30);
                    CacheHelper.CacheInsertAddMinutes("Password", Password, 30);
                    if (Account == "admin" && Password == "admin")                      
                        return Redirect("/Article/ArticleAdminList");
                    else 
                        return Redirect("/Home/Home");
                }
            }
            return View();
        }
    }
}
