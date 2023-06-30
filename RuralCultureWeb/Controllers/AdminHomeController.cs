using Microsoft.AspNetCore.Mvc;

namespace RuralCultureWeb.Controllers
{
    public class AdminHomeController : Controller
    {
        [SessionFilter(isAdminPage =true)]
        public IActionResult AdminHome()
        {
            return View();
        }
    }
}
