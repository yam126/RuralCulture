using Microsoft.AspNetCore.Mvc;

namespace RuralCultureWeb.Controllers
{
    public class RuralCategorController : Controller
    {
        [SessionFilter]
        public IActionResult RuralCategor(string? groupId="")
        {
            SetViewData("groupId", groupId);
            return View();
        }

        private void SetViewData(string KeyName, string? KeyValue)
        {
            if (ViewData.Keys.Contains(KeyName))
                ViewData[KeyName] = KeyValue;
            else
                ViewData.Add(KeyName, KeyValue);
        }
    }
}
