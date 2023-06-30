using Microsoft.AspNetCore.Mvc;

namespace RuralCultureWeb.Controllers
{
    public class HomeController : Controller
    {
        [SessionFilter]
        public IActionResult Home()
        {
            return View();
        }
    }
}
