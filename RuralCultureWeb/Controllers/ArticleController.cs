using Microsoft.AspNetCore.Mvc;
using RestSharpHelper;
using RuralCultureWeb.Models;

namespace RuralCultureWeb.Controllers
{
    public class ArticleController : BaseController
    {
        #region Action

        /// <summary>
        /// 前端文章页面
        /// </summary>
        /// <param name="ArticleId">文章编号</param>
        /// <returns></returns>
        [SessionFilter]
        public IActionResult ArticleView(string ArticleId)
        {
            string message = string.Empty;
            string WebApiHostBase = string.Empty;
            string apiName = $"api/article?ArticleId={ArticleId}";
            WebApiArticleResultRoot apiResult = new WebApiArticleResultRoot();
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            WebApiHostBase= configuration["WebApiHost"];
            if (string.IsNullOrEmpty(WebApiHostBase))
            {
                message= "WebApi地址没有在appsettings.json配置，请检查配置";
                if (ViewData.Keys.Contains("EorrMsg"))
                    ViewData["EorrMsg"] = message;
                else
                    ViewData.Add("EorrMsg", message);
                return View();
            }
            HttpHelper.baseUrl = WebApiHostBase;
            apiResult = HttpHelper.GetApi<WebApiArticleResultRoot>(apiName,null);
            WebApiArticleResultItem articleInfo = new WebApiArticleResultItem();
            if (apiResult == null)
            {
                if (ViewData.Keys.Contains("ArticleInfo"))
                    ViewData["ArticleInfo"] = articleInfo;
                else
                    ViewData.Add("ArticleInfo", articleInfo);
                return View();
            }
            if (apiResult.status != 0) 
            {
                message = $"读取文章出错,原因[{apiResult.msg}]";
                if (ViewData.Keys.Contains("EorrMsg"))
                    ViewData["EorrMsg"] = message;
                else
                    ViewData.Add("EorrMsg", message);
                return View();
            }
            articleInfo = (apiResult.result==null||apiResult.result.Count<=0)? new WebApiArticleResultItem(): apiResult.result[0];
            if (ViewData.Keys.Contains("ArticleInfo"))
                ViewData["ArticleInfo"] = articleInfo;
            else
                ViewData.Add("ArticleInfo", articleInfo);
            UpdateArticleViewCount(ArticleId);
            return View();
        }

        /// <summary>
        /// 文章列表管理
        /// </summary>
        /// <returns></returns>
        [SessionFilter(isAdminPage =true)]
        public IActionResult ArticleAdminList()
        {
            return View();
        }

        /// <summary>
        /// 文章发布页
        /// </summary>
        /// <param name="ArticleType">文章类型</param>
        /// <param name="ArticleId">文章编号</param>
        /// <returns></returns>
        [SessionFilter(isAdminPage = true)]
        public IActionResult ArticlePublish(string ArticleType, string ArticleId) 
        {
            Console.WriteLine($"ArticleType={ArticleType}");
            Console.WriteLine($"ArticleId={ArticleId}");
            switch (ArticleType) 
            {
                case "NormalArticle":
                    ArticleType = "0";
                    break;
                case "VideoArticle":
                    ArticleType = "2";
                    break;
            }
            ViewData.Add("ArticleType", ArticleType);
            ViewData.Add("ArticleId", ArticleId);
            if (!string.IsNullOrEmpty(ArticleId)) 
                ViewData.Add("Action", "Edit");
            else
                ViewData.Add("Action", "Add");
            return View();
        }

        /// <summary>
        /// 文化动态
        /// </summary>
        /// <returns></returns>
        [SessionFilter]
        public IActionResult ArticleDynamics(string? keyword="",string? advancedField="",string advancedValue="") 
        {
            SetViewData("keyword", keyword);
            SetViewData("advancedField", advancedField);
            SetViewData("advancedValue", advancedValue);
            return View();
        }
        #endregion

        #region Private

        private void SetViewData(string KeyName,string? KeyValue) 
        {
            if (ViewData.Keys.Contains(KeyName))
                ViewData[KeyName] = KeyValue;
            else
                ViewData.Add(KeyName, KeyValue);
        }

        /// <summary>
        /// 更新文章浏览量
        /// </summary>
        /// <param name="ArticleId">文章编号</param>
        private void UpdateArticleViewCount(string ArticleId) 
        {
            string WebApiHostBase = string.Empty;
            string apiName = $"api/article/update/viewcount/{ArticleId}";
            WebApiResult apiResult = new WebApiResult();
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            WebApiHostBase = configuration["WebApiHost"];
            apiResult = HttpHelper.PutApi<WebApiResult>(apiName, null);
        }
        #endregion
    }
}
