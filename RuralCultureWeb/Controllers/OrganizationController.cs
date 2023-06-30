using Microsoft.AspNetCore.Mvc;

namespace RuralCultureWeb.Controllers
{
    public class OrganizationController : Controller
    {
        [SessionFilter(isAdminPage = true)]
        public IActionResult OrganizationManage()
        {
            return View();
        }

        [SessionFilter]
        public IActionResult OrganizationView() 
        {
            return View();
        }
    }
}
