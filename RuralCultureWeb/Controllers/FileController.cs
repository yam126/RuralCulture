using Microsoft.AspNetCore.Mvc;

namespace RuralCultureWeb.Controllers
{
    public class FileController : BaseController
    {
        [SessionFilter(isAdminPage = true)]
        public IActionResult FileManage()
        {
            return View();
        }
    }
}
