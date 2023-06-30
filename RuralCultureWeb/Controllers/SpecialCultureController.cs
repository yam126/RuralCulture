using Microsoft.AspNetCore.Mvc;

namespace RuralCultureWeb.Controllers
{
    public class SpecialCultureController : Controller
    {
        [SessionFilter]
        public IActionResult SpecialCulture()
        {
            return View();
        }
    }
}
