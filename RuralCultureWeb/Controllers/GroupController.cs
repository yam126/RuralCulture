using Microsoft.AspNetCore.Mvc;

namespace RuralCultureWeb.Controllers
{
    public class GroupController : Controller
    {
        [SessionFilter(isAdminPage = true)]
        public IActionResult GroupManage()
        {
            return View();
        }
    }
}
