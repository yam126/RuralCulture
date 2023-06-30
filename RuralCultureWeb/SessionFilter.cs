using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
namespace RuralCultureWeb
{
    /// <summary>
    /// Session过滤
    /// </summary>
    public class SessionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 是否管理页面
        /// </summary>
        public bool isAdminPage { get; set; } = false;
        
        #region Public

        /// <summary>
        /// Action 方法执行之前触发
        /// </summary>
        /// <param name="context">Action执行上下文</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? Account = CacheHelper.CacheValue("Account") == null ? string.Empty:Convert.ToString(CacheHelper.CacheValue("Account"));
            if (Account == null||string.IsNullOrEmpty(Account))
            {
                context.Result = new RedirectToActionResult("Index","Login",null);
                return;
            }
            else if (Account != "admin") 
            {
                if(isAdminPage)
                    context.Result = new RedirectToActionResult("Home", "Home", null);
                return;
            }
            base.OnActionExecuting(context);
        }

        /// <summary>
        /// Action 方法执行之后 Result 调用之前触发
        /// </summary>
        /// <param name="context">Action执行上下文</param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        /// <summary>
        /// Result方法调用之前执行
        /// </summary>
        /// <param name="context">Action执行上下文</param>
        public override void OnResultExecuting(ResultExecutingContext context) 
        {
            base.OnResultExecuting(context);
        }

        /// <summary>
        /// Result方法调用之后执行
        /// </summary>
        /// <param name="context">Action执行上下文</param>
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }
        #endregion
    }
}
