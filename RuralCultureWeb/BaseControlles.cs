using Microsoft.AspNetCore.Mvc;

namespace RuralCultureWeb
{
    public class BaseController: Controller
    {
        protected string Account { get; set; }

        public BaseController() 
        {
            Account = CacheHelper.CacheValue("Account") == null ?
                Convert.ToString(CacheHelper.CacheValue("Account")) :
                string.Empty;
            ViewData.Add("Account", Account);
        }
    }
}
