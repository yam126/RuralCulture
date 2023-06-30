using AutoMapper;
using Common;
using ePioneer.Data.Kernel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Npoi.Mapper.Attributes;
using RuralCultureDataAccess;
using RuralCultureDataAccess.Models;
using RuralCultureWebApi.Models;
using Snowflake.Net;
using System.Data;
using System.Net;
using System.Text;
using System.Text.Encodings;
using System.Text.Encodings.Web;
using System.Web;
using System.Text.RegularExpressions;

namespace RuralCultureWebApi.Controllers
{
    /// <summary>
    /// 文章控制器
    /// </summary>
    [Route("api/article")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        #region Fields

        /// <summary>
        /// 数据库操作类
        /// </summary>
        private readonly IMOIRepository m_repository;

        /// <summary>
        /// AutoMapper参数映射类
        /// </summary>
        private readonly IMapper m_mapper;

        /// <summary>
        /// 获取网站路径
        /// </summary>
        private readonly IWebHostEnvironment m_webHostEnvironment;
        #endregion

        #region Constructors

        /// <summary>
        /// 重载构造函数
        /// </summary>
        /// <param name="repository">数据库操作类</param>
        /// <param name="_mapper">字段映射类</param>
        /// <param name="webHostEnvironment">网站路径类</param>
        public ArticleController(IMOIRepository repository, IMapper _mapper, IWebHostEnvironment webHostEnvironment)
        {
            m_repository = repository;
            m_mapper = _mapper == null ? throw new ArgumentNullException(nameof(_mapper)) : _mapper;
            m_webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Get

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">当前页(默认从1开始)</param>
        /// <param name="pageSize">每页记录数(默认每页10条)</param>
        /// <param name="where">查询条件(SQL查询条件,可以为空,为空返回所有数据)</param>
        /// <param name="SortField">排序字段(默认创建时间)</param>
        /// <param name="SortMethod">排序方法[DESC|ASC(默认DESC)]</param>
        /// <returns>返回查询结果</returns>
        [HttpGet("page")]
        public ActionResult<PageResult<ArticleParameter>> GetPage(
            string? where = "",
            int? pageIndex = 1,
            int? pageSize = 10,
            string sortField = "CreatedTime",
            string sortMethod = "DESC"
            )
        {
            #region 声明变量

            //总页数
            int pageCount = 0;

            //总记录数
            int totalRecordCount = 0;

            //方法返回错误消息
            string message = string.Empty;

            //错误消息
            string checkMessage = string.Empty;

            //页面返回值
            List<Article> pageData = null;

            //接口返回值
            List<ArticleParameter> pageResultData = new List<ArticleParameter>();

            //返回值
            var result = new PageResult<ArticleParameter>();
            #endregion

            #region 非空验证
            if (pageIndex == null)
                checkMessage += "当前页、";
            if (pageSize == null)
                checkMessage += "每页记录数、";
            if (!string.IsNullOrEmpty(checkMessage))
            {
                checkMessage = checkMessage.Substring(0, checkMessage.Length - 1);
                result = new PageResult<ArticleParameter>()
                {
                    Status = -1,
                    PageCount = 0,
                    RecordCount = 0,
                    Msg = $"非空验证出错，原因[{checkMessage}]",
                    Result = null
                };
                return result;
            }
            #endregion

            #region 有效验证
            if (pageIndex <= 0)
                checkMessage += "当前页不能小于或等于0、";
            if (pageSize <= 0)
                checkMessage += "每页记录数不能小于或等于0、";
            if (!string.IsNullOrEmpty(checkMessage))
            {
                checkMessage = checkMessage.Substring(0, checkMessage.Length - 1);
                result = new PageResult<ArticleParameter>()
                {
                    Status = -1,
                    PageCount = 0,
                    RecordCount = 0,
                    Msg = $"有效验证出错，原因[{checkMessage}]",
                    Result = null
                };
                return result;
            }
            #endregion

            #region 查询数据
            if (!string.IsNullOrEmpty(where) && where != "null")
            {
                List<string> sqlWhere = new List<string>();
                sqlWhere.Add($" ArticleTitle like '%{where}%' ");
                sqlWhere.Add($" Content like '%{where}%' ");
                sqlWhere.Add($" Author like '%{where}%' ");
                where = String.Join(" Or ", sqlWhere.ToArray());
            }
            else if (where == "null")
                where = null;
            pageData = m_repository.QueryPageArticle(
                where == null ? string.Empty : where,
                sortField,
                sortMethod,
                pageSize.GetValueOrDefault(),
                pageIndex.GetValueOrDefault(),
                out totalRecordCount,
                out pageCount,
                out message
            );
            if (pageData == null || pageData.Count <= 0)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    result = new PageResult<ArticleParameter>()
                    {
                        Status = -1,
                        PageCount = 0,
                        RecordCount = 0,
                        Msg = $"查询数据出错，原因[{message}]",
                        Result = null
                    };
                    return result;
                }
                else
                {
                    result = new PageResult<ArticleParameter>()
                    {
                        Status = 0,
                        PageCount = 0,
                        RecordCount = 0,
                        Msg = string.Empty,
                        Result = null
                    };
                }
                return result;
            }
            #endregion

            #region 计算总页数
            if (totalRecordCount % pageSize == 0)
                pageCount = Convert.ToInt32(totalRecordCount / pageSize);
            else
                pageCount = Convert.ToInt32(totalRecordCount / pageSize) + 1;
            #endregion

            #region 赋值返回值
            pageData.ForEach(pageDataItem => {
                var resultDataItem = m_mapper.Map<ArticleParameter>(pageDataItem);
                resultDataItem.CreatedTime = pageDataItem.CreatedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");
                resultDataItem.ModifiedTime = pageDataItem.ModifiedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");
                ConvertState(pageDataItem, ref resultDataItem);
                pageResultData.Add(resultDataItem);
            });
            result = new PageResult<ArticleParameter>()
            {
                Status = 0,
                PageCount = pageCount,
                RecordCount = totalRecordCount,
                Msg = string.Empty,
                Result = pageResultData.ToArray()
            };
            #endregion

            return result;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">当前页(默认从1开始)</param>
        /// <param name="pageSize">每页记录数(默认每页10条)</param>
        /// <param name="where">查询条件(SQL查询条件,可以为空,为空返回所有数据)</param>
        /// <param name="SortField">排序字段(默认创建时间)</param>
        /// <param name="SortMethod">排序方法[DESC|ASC(默认DESC)]</param>
        /// <returns>返回查询结果</returns>
        [HttpGet("view/page")]
        public async Task<ActionResult<PageResultColumn<ArticleParameter>>> GetPageVWArticle(
            string? where = "",
            string? condition = "",
            string? pageName = "",
            string? tableName = "",
            int? pageIndex = 1,
            int? pageSize = 10,
            string sortField = "CreatedTime",
            string sortMethod = "DESC"
            )
        {
            #region 声明变量

            //总页数
            int pageCount = 0;

            //总记录数
            int totalRecordCount = 0;

            //方法返回错误消息
            string message = string.Empty;

            //错误消息
            string checkMessage = string.Empty;

            //字段列表
            string FieldStr = string.Empty;

            //页面返回值
            List<vw_Article> pageData = null;

            //接口返回值
            List<ArticleParameter> pageResultData = new List<ArticleParameter>();

            //返回值
            var result = new PageResultColumn<ArticleParameter>();

            //表格字段列表
            List<PageTableColumn> pageTableColumn = new List<PageTableColumn>();

            //表格字段列表
            List<PageTableColumnParameter> pageTableColumnResult = new List<PageTableColumnParameter>();
            #endregion

            #region 非空验证
            if (pageIndex == null)
                checkMessage += "当前页、";
            if (pageSize == null)
                checkMessage += "每页记录数、";
            if (!string.IsNullOrEmpty(checkMessage))
            {
                checkMessage = checkMessage.Substring(0, checkMessage.Length - 1);
                result = new PageResultColumn<ArticleParameter>()
                {
                    Status = -1,
                    PageCount = 0,
                    RecordCount = 0,
                    Msg = $"非空验证出错，原因[{checkMessage}]",
                    TableColumn = null,
                    Result = null
                };
                return result;
            }
            #endregion

            #region 有效验证
            if (pageIndex <= 0)
                checkMessage += "当前页不能小于或等于0、";
            if (pageSize <= 0)
                checkMessage += "每页记录数不能小于或等于0、";
            if (!string.IsNullOrEmpty(checkMessage))
            {
                checkMessage = checkMessage.Substring(0, checkMessage.Length - 1);
                result = new PageResultColumn<ArticleParameter>()
                {
                    Status = -1,
                    PageCount = 0,
                    RecordCount = 0,
                    Msg = $"有效验证出错，原因[{checkMessage}]",
                    TableColumn = null,
                    Result = null
                };
                return result;
            }
            #endregion

            #region 读取页面字段设置列表
            pageTableColumn = m_repository.QueryPageTableColumn($" PageName='{pageName}' and TableName='{tableName}' and IsShow='1' order by OrderSequence asc ", out message);
            if (pageTableColumn != null && pageTableColumn.Count > 0)
            {
                foreach (var item in pageTableColumn)
                    FieldStr += $"[{item.FieldName}],";
                pageTableColumn.ForEach(item => {
                    var resultDataItem = m_mapper.Map<PageTableColumnParameter>(item);
                    pageTableColumnResult.Add(resultDataItem);
                });
                FieldStr = string.IsNullOrEmpty(FieldStr) ? string.Empty : FieldStr.Substring(0, FieldStr.Length - 1);
            }
            #endregion

            #region 查询数据
            if (!string.IsNullOrEmpty(where) && where != "null")
            {
                List<string> sqlWhere = new List<string>();
                if (string.IsNullOrEmpty(condition) || condition == "null")
                {
                    sqlWhere.Add($" ArticleTitle like '%{where}%' ");
                    sqlWhere.Add($" Content like '%{where}%' ");
                    sqlWhere.Add($" Author like '%{where}%' ");
                    sqlWhere.Add($" OrgName like '%{where}%' ");
                    sqlWhere.Add($" GroupName like '%{where}%' ");
                }
                else
                    sqlWhere.Add($" {condition} like '%{where}%' ");
                where = String.Join(" Or ", sqlWhere.ToArray());
            }
            else if (where == "null")
                where = null;
            pageData = m_repository.QueryPageVWArticle(
                where == null ? string.Empty : where,
                sortField,
                sortMethod,
                pageSize.GetValueOrDefault(),
                pageIndex.GetValueOrDefault(),
                out totalRecordCount,
                out pageCount,
                out message,
                FieldStr
            );
            if (pageData == null || pageData.Count <= 0)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    result = new PageResultColumn<ArticleParameter>()
                    {
                        Status = -1,
                        PageCount = 0,
                        RecordCount = 0,
                        Msg = $"查询数据出错，原因[{message}]",
                        TableColumn = null,
                        Result = null
                    };
                    return result;
                }
                else
                {
                    result = new PageResultColumn<ArticleParameter>()
                    {
                        Status = 0,
                        PageCount = 0,
                        RecordCount = 0,
                        Msg = string.Empty,
                        TableColumn = null,
                        Result = null
                    };
                }
                return result;
            }
            #endregion

            #region 计算总页数
            if (totalRecordCount % pageSize == 0)
                pageCount = Convert.ToInt32(totalRecordCount / pageSize);
            else
                pageCount = Convert.ToInt32(totalRecordCount / pageSize) + 1;
            #endregion

            #region 赋值返回值
            pageData.ForEach(pageDataItem => {
                var resultDataItem = m_mapper.Map<ArticleParameter>(pageDataItem);
                resultDataItem.CoverUrl=pageDataItem.CoverUrl;
                resultDataItem.CreatedTime = pageDataItem.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss");
                resultDataItem.ModifiedTime = pageDataItem.ModifiedTime.ToString("yyyy-MM-dd HH:mm:ss");
                ConvertState(pageDataItem, ref resultDataItem);
                pageResultData.Add(resultDataItem);
            });
            result = new PageResultColumn<ArticleParameter>()
            {
                Status = 0,
                PageCount = pageCount,
                RecordCount = totalRecordCount,
                Msg = string.Empty,
                TableColumn = pageTableColumnResult,
                Result = pageResultData.ToArray()
            };
            #endregion

            return result;
        }

        /// <summary>
        /// 获得文化分类页面数据
        /// </summary>
        /// <param name="groupId">文艺小组编号</param>
        /// <returns>返回数据</returns>
        [HttpGet("ruralcategor/ruralcategor")]
        public async Task<ListResult<ArticleParameter>> GetRuralCategorPageData(string? groupId="") 
        {
            #region 声明变量

            //查询条件
            string SqlWhere = string.Empty;

            //排序字符串
            string SqlOrderBy = " CreatedTime desc ";

            //TOP字符串
            string TopStr = " top 12 ";

            //错误消息
            string message = string.Empty;

            //返回值
            var result = new ListResult<ArticleParameter>();

            //数据库返回值
            List<vw_Article> dbResultData = new List<vw_Article>();

            //接口返回值
            List<ArticleParameter> pageResultData = new List<ArticleParameter>();
            #endregion

            #region 查询数据库数据
            SqlWhere = $" GroupId='{groupId}' ";
            dbResultData = m_repository.QueryVWArticle(SqlWhere,SqlOrderBy,TopStr,out message);
            if (dbResultData == null || dbResultData.Count <= 0) 
            {
                if (!string.IsNullOrEmpty(message)) 
                {
                    result = new ListResult<ArticleParameter>() 
                    {
                        Status=-1,
                        Msg=$"查询文化分类数据出错,原因[{message}]",
                        Result=null
                    };
                    return result;
                }
                else 
                {
                    result = new ListResult<ArticleParameter>()
                    {
                        Status = 0,
                        Msg = string.Empty,
                        Result = null
                    };
                    return result;
                }
            }
            #endregion

            #region 转换数据
            dbResultData.ForEach(item => {
                var apiResult=m_mapper.Map<ArticleParameter>(item);
                apiResult.Backup01 = item.CreatedTime.Month.ToString();
                apiResult.Backup02 = item.CreatedTime.Day.ToString();
                if (item.ArticleType != 2)
                {
                    if (NoHTML(item.Content).Length > 300)
                        apiResult.Content = NoHTML(item.Content).Substring(0, 300);
                    else
                        apiResult.Content = NoHTML(item.Content);
                }
                pageResultData.Add(apiResult);
            });
            #endregion

            result = new ListResult<ArticleParameter>() 
            {
                Status=0,
                Msg= string.Empty,
                Result=pageResultData
            };
            return result;
        }

        /// <summary>
        /// 文章查询加强版
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns>返回结果</returns>
        [HttpGet("/api/article/advance/query/page")]
        public async Task<ActionResult<PageResultColumn<ArticleParameter>>> GetArticleAdvancePage(string? where="",string? advancedWhere="",int? pageIndex = 1,int? pageSize = 10) 
        {
            #region 声明变量

            //总页数
            int pageCount = 0;

            //总记录数
            int totalRecordCount = 0;

            //方法返回错误消息
            string message = string.Empty;

            //非空验证
            string checkMessage = string.Empty;

            //排序字符串
            string SortStr = "IsTop DESC,CreatedTime DESC";

            //页面返回值
            List<vw_Article> pageData = null;

            //返回值
            var result = new PageResultColumn<ArticleParameter>();

            //接口返回值
            List<ArticleParameter> pageResultData = new List<ArticleParameter>();
            #endregion

            #region 非空验证
            if (pageIndex == null)
                checkMessage += "当前页、";
            if (pageSize == null)
                checkMessage += "每页记录数、";
            if (!string.IsNullOrEmpty(checkMessage))
            {
                checkMessage = checkMessage.Substring(0, checkMessage.Length - 1);
                result = new PageResultColumn<ArticleParameter>()
                {
                    Status = -1,
                    PageCount = 0,
                    RecordCount = 0,
                    Msg = $"非空验证出错，原因[{checkMessage}]",
                    Result = null
                };
                return result;
            }
            #endregion

            #region 查询数据
            if (string.IsNullOrEmpty(advancedWhere) || advancedWhere == "null")
            {
                if (!string.IsNullOrEmpty(where) && where != "null")
                {
                    List<string> sqlWhere = new List<string>();
                    sqlWhere.Add($" ArticleTitle like '%{where}%' ");
                    sqlWhere.Add($" Content like '%{where}%' ");
                    sqlWhere.Add($" Author like '%{where}%' ");
                    sqlWhere.Add($" OrgName like '%{where}%' ");
                    sqlWhere.Add($" GroupName like '%{where}%' ");
                    where = String.Join(" Or ", sqlWhere.ToArray());
                }
                else if (where == "null")
                    where = null;
            }
            else 
            {
                string condition = advancedWhere.Split("|")[0];
                string value = advancedWhere.Split("|")[1];
                where = $" {condition}='{value}' ";
            }
            pageData = m_repository.QueryArticleAdvnancePage(
                where == null ? string.Empty : where,
                SortStr,
                pageSize.GetValueOrDefault(),
                pageIndex.GetValueOrDefault(),
                out totalRecordCount,
                out pageCount,
                out message
            );
            if (pageData == null || pageData.Count <= 0)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    result = new PageResultColumn<ArticleParameter>()
                    {
                        Status = -1,
                        PageCount = 0,
                        RecordCount = 0,
                        Msg = $"查询数据出错，原因[{message}]",
                        TableColumn = null,
                        Result = null
                    };
                    return result;
                }
                else
                {
                    result = new PageResultColumn<ArticleParameter>()
                    {
                        Status = 0,
                        PageCount = 0,
                        RecordCount = 0,
                        Msg = string.Empty,
                        TableColumn = null,
                        Result = null
                    };
                }
                return result;
            }
            #endregion

            #region 赋值返回值
            pageData.ForEach(pageDataItem => {
                var resultDataItem = m_mapper.Map<ArticleParameter>(pageDataItem);
                resultDataItem.CoverUrl = pageDataItem.CoverUrl;
                resultDataItem.CreatedTime = pageDataItem.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss");
                resultDataItem.ModifiedTime = pageDataItem.ModifiedTime.ToString("yyyy-MM-dd HH:mm:ss");
                ConvertState(pageDataItem, ref resultDataItem);
                pageResultData.Add(resultDataItem);
            });
            result = new PageResultColumn<ArticleParameter>()
            {
                Status = 0,
                PageCount = pageCount,
                RecordCount = totalRecordCount,
                Msg = string.Empty,
                TableColumn = null,
                Result = pageResultData.ToArray()
            };
            #endregion

            return result;
        }

        /// <summary>
        /// 根据文章ID查询文章
        /// </summary>
        /// <param name="ArticleId">文章编号</param>
        /// <returns>返回数据</returns>
        [HttpGet]
        public async Task<ActionResult<ListResult<ArticleParameter>>> GetArticleById(string ArticleId)
        {
            #region 声明变量

            //返回状态
            int resultState = 0;

            //方法返回错误消息
            string message = string.Empty;

            //查询条件
            string SqlWhere = string.Empty;

            //文章数据
            List<vw_Article> articles = new List<vw_Article>();

            //返回数据
            List<ArticleParameter> articleParameters = new List<ArticleParameter>();

            //返回值
            ListResult<ArticleParameter> result = null;
            #endregion

            #region 参数验证
            if (string.IsNullOrEmpty(ArticleId))
            {
                result = new ListResult<ArticleParameter>()
                {
                    Status = -1,
                    Msg = "文章编号不能为空",
                    Result = null
                };
                return result;
            }
            #endregion

            #region 查询数据
            SqlWhere = $" ArticleId='{ArticleId}' ";
            articles = m_repository.QueryVWArticle(SqlWhere, out message);
            if (articles == null || articles.Count <= 0)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    resultState = -1;
                    message = $"查询编号为[{ArticleId}]的文章数据出错,原因[{message}]";
                }
                else
                {
                    resultState = 0;
                    message = $"查询编号为[{ArticleId}]的文章数据出错,原因[没有查到任何数据]";
                }
                result = new ListResult<ArticleParameter>()
                {
                    Status = resultState,
                    Msg = message,
                    Result = null
                };
                return result;
            }
            #endregion

            #region 赋值返回值
            articles.ForEach(articleItem => {
                articleParameters.Add(m_mapper.Map<ArticleParameter>(articleItem));
            });
            result = new ListResult<ArticleParameter>()
            {
                Status = 0,
                Msg = string.Empty,
                Result = articleParameters
            };
            #endregion

            return result;
        }

        /// <summary>
        /// 查询浏览量前10条的Article数据
        /// </summary>
        /// <returns>返回值</returns>
        [HttpGet("viewcount/topten")]
        public async Task<ActionResult<ListResult<ArticleParameter>>> QueryArticleViewCountTopTen() 
        {
            #region 声明变量

            //返回状态
            int resultState = 0;

            //方法返回错误消息
            string message = string.Empty;

            //查询条件
            string SqlWhere = string.Empty;

            //文章数据
            List<vw_Article> articles = new List<vw_Article>();

            //返回数据
            List<ArticleParameter> articleParameters = new List<ArticleParameter>();

            //返回值
            ListResult<ArticleParameter> result = null;
            #endregion

            #region 查询数据
            articles = m_repository.QueryArticleViewCountTopTen(string.Empty, out message);
            if (articles == null || articles.Count <= 0)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    resultState = -1;
                    message = $"查询点击排行文章数据出错,原因[{message}]";
                }
                else
                {
                    resultState = 0;
                    message = $"查询点击排行文章数据出错,原因[没有查到任何数据]";
                }
                result = new ListResult<ArticleParameter>()
                {
                    Status = resultState,
                    Msg = message,
                    Result = null
                };
                return result;
            }
            #endregion

            #region 赋值返回值
            articles.ForEach(articleItem => {
                articleParameters.Add(m_mapper.Map<ArticleParameter>(articleItem));
            });
            result = new ListResult<ArticleParameter>()
            {
                Status = 0,
                Msg = string.Empty,
                Result = articleParameters
            };
            #endregion

            return result;
        }

        /// <summary>
        /// 查询创建时间前3条的Article数据
        /// </summary>
        /// <returns>返回值</returns>
        [HttpGet("createdtime/topten")]
        public async Task<ActionResult<ListResult<ArticleParameter>>> QueryArticleCreatedTimeTop(int? topnum=4) 
        {
            #region 声明变量

            //返回状态
            int resultState = 0;

            //方法返回错误消息
            string message = string.Empty;

            //查询条件
            string SqlWhere = string.Empty;

            //文章数据
            List<vw_Article> articles = new List<vw_Article>();

            //返回数据
            List<ArticleParameter> articleParameters = new List<ArticleParameter>();

            //返回值
            ListResult<ArticleParameter> result = null;
            #endregion

            #region 查询数据
            articles = m_repository.QueryArticleTopData(out message, SqlWhere, topnum);
            if (articles == null || articles.Count <= 0)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    resultState = -1;
                    message = $"查询点击排行文章数据出错,原因[{message}]";
                }
                else
                {
                    resultState = 0;
                    message = $"查询点击排行文章数据出错,原因[没有查到任何数据]";
                }
                result = new ListResult<ArticleParameter>()
                {
                    Status = resultState,
                    Msg = message,
                    Result = null
                };
                return result;
            }
            #endregion

            #region 赋值返回值
            articles.ForEach(articleItem => {
                articleParameters.Add(m_mapper.Map<ArticleParameter>(articleItem));
            });
            result = new ListResult<ArticleParameter>()
            {
                Status = 0,
                Msg = string.Empty,
                Result = articleParameters
            };
            #endregion

            return result;
        }

        /// <summary>
        /// 查询创建时间前3条的Article数据
        /// </summary>
        /// <returns>返回值</returns>
        [HttpGet("top")]
        public async Task<ActionResult<ListResult<ArticleParameter>>> QueryArticleTop()
        {
            #region 声明变量

            //返回状态
            int resultState = 0;

            //方法返回错误消息
            string message = string.Empty;

            //查询条件
            string SqlWhere = string.Empty;

            //文章数据
            List<vw_Article> articles = new List<vw_Article>();

            //返回数据
            List<ArticleParameter> articleParameters = new List<ArticleParameter>();

            //返回值
            ListResult<ArticleParameter> result = null;
            #endregion

            #region 查询数据
            articles = m_repository.QueryArticleTopData(out message, SqlWhere, 3, " IsTop desc ");
            if (articles == null || articles.Count <= 0)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    resultState = -1;
                    message = $"查询点击排行文章数据出错,原因[{message}]";
                }
                else
                {
                    resultState = 0;
                    message = $"查询点击排行文章数据出错,原因[没有查到任何数据]";
                }
                result = new ListResult<ArticleParameter>()
                {
                    Status = resultState,
                    Msg = message,
                    Result = null
                };
                return result;
            }
            #endregion

            #region 赋值返回值
            articles.ForEach(articleItem => {
                articleParameters.Add(m_mapper.Map<ArticleParameter>(articleItem));
            });
            result = new ListResult<ArticleParameter>()
            {
                Status = 0,
                Msg = string.Empty,
                Result = articleParameters
            };
            #endregion

            return result;
        }

        /// <summary>
        /// 获取特色文化
        /// </summary>
        /// <returns></returns>
        [HttpGet("isspecial")]
        public async Task<ActionResult<ListResult<ArticleParameter>>> QueryArticleIsSpecial() 
        {
            #region 声明变量

            //返回状态
            int resultState = 0;

            //方法返回错误消息
            string message = string.Empty;

            //查询条件
            string SqlWhere = " IsSpecial=1 ";

            //文章数据
            List<vw_Article> articles = new List<vw_Article>();

            //返回数据
            List<ArticleParameter> articleParameters = new List<ArticleParameter>();

            //返回值
            ListResult<ArticleParameter> result = null;
            #endregion

            #region 查询数据
            articles = m_repository.QueryArticleTopData(out message, SqlWhere, 1, " CreatedTime desc ");
            if (articles == null || articles.Count <= 0)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    resultState = -1;
                    message = $"查询点击排行文章数据出错,原因[{message}]";
                }
                else
                {
                    resultState = 0;
                    message = $"查询点击排行文章数据出错,原因[没有查到任何数据]";
                }
                result = new ListResult<ArticleParameter>()
                {
                    Status = resultState,
                    Msg = message,
                    Result = null
                };
                return result;
            }
            #endregion

            #region 赋值返回值
            articles.ForEach(articleItem => {
                articleItem.Content=WebUtility.HtmlDecode(articleItem.Content);
                articleItem.Content=NoHTML(articleItem.Content);
                var apiModel = m_mapper.Map<ArticleParameter>(articleItem);
                apiModel.CreatedTime = articleItem.CreatedTime.ToString("yyyy-MM-dd");
                apiModel.ModifiedTime = articleItem.ModifiedTime.ToString("yyyy-MM-dd");
                articleParameters.Add(apiModel);
            });
            result = new ListResult<ArticleParameter>()
            {
                Status = 0,
                Msg = string.Empty,
                Result = articleParameters
            };
            #endregion

            return result;
        }

        /// <summary>
        /// 根据文章编号获得文章内容
        /// </summary>
        /// <param name="ArticleId">文章内容</param>
        /// <returns></returns>
        [HttpGet("/article/{ArticleId}")]
        public EntityResult<string> GetArticleContentById(string ArticleId)
        {
            #region 声明变量

            //返回状态
            int resultState = -1;

            //返回内容
            string resultContent = string.Empty;

            //返回值
            EntityResult<string> result=null;

            byte[] resultData=null;

            //方法返回错误消息
            string message = string.Empty;

            //查询条件
            string SqlWhere = string.Empty;

            //文章数据
            List<vw_Article> articles = new List<vw_Article>();
            #endregion

            #region 参数验证
            if (string.IsNullOrEmpty(ArticleId))
            {
                result = new EntityResult<string>()
                {
                    Status = -1,
                    Msg = "文章编号不能为空"
                };
                return result;
            }
            #endregion

            #region 查询数据
            SqlWhere = $" ArticleId='{ArticleId}' ";
            articles = m_repository.QueryVWArticle(SqlWhere, out message);
            if (articles == null || articles.Count <= 0)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    result = new EntityResult<string>() 
                    { 
                        Status = resultState, 
                        Msg = message,
                        Result = string.Empty
                    };
                    return result;
                }
                result = new EntityResult<string>()
                {
                    Status = 0,
                    Msg = String.Empty,
                    Result = string.Empty
                };
                return result;
            }
            #endregion
            string htmlCode = WebUtility.HtmlDecode(articles[0].Content);
            result = new EntityResult<string>()
            {
                Status = 0,
                Msg = String.Empty,
                Result = htmlCode
            };
            return result;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="selectedIDStr">选择要导出的编号</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        [HttpGet("/export/excel")]
        public IActionResult ExportExcel(
            string selectedIDStr="null",
            string where="null",
            string condition = "null",
            string? pageName = "",
            string? tableName = "")
        {
            #region 声明变量

            //ID字符串
            string IDStr = string.Empty;

            //查询条件
            string SqlWhere = string.Empty;

            //查询条件集合
            List<string> whereList = new List<string>();

            //错误消息
            string message = string.Empty;

            //输出文件类型
            string ContentType = "application/vnd.ms-excel";

            //Stream流
            MemoryStream stream = null;

            //excel文件
            Npoi.Mapper.Mapper excelMap = new Npoi.Mapper.Mapper();

            //公司编号信息
            List<vw_Article> articleInfos = null;

            //页面字段信息
            List<PageTableColumn> tableColumns = new List<PageTableColumn>();

            //导出数据
            List<ArticleExcelParameter> exportData = new List<ArticleExcelParameter>();
            #endregion

            #region 处理ID字符串查询条件
            if (!string.IsNullOrEmpty(selectedIDStr) && selectedIDStr != "null")
            {
                IDStr = "'" + selectedIDStr.Replace("-", "','") + "'";
                whereList.Add(" ArticleId in (" + IDStr + ")");
            }
            if (!string.IsNullOrEmpty(where) && where != "null")
            {
                List<string> sqlWhere = new List<string>();
                if (string.IsNullOrEmpty(condition) || condition == "null")
                {
                    sqlWhere.Add($" ArticleTitle like '%{where}%' ");
                    sqlWhere.Add($" Content like '%{where}%' ");
                    sqlWhere.Add($" Author like '%{where}%' ");
                    sqlWhere.Add($" OrgName like '%{where}%' ");
                    sqlWhere.Add($" GroupName like '%{where}%' ");
                }
                else
                    sqlWhere.Add($" {condition} like {where} ");
                where = String.Join(" Or ", sqlWhere.ToArray());
                whereList.Add($" ({where}) ");
            }
            if (whereList.Count > 0)
                SqlWhere = string.Join(" and ", whereList);
            #endregion

            #region 查询数据
            articleInfos = m_repository.QueryVWArticle(SqlWhere, out message);
            if (articleInfos == null || articleInfos.Count <= 0)
            {
                Response.ContentType = "text/html";
                if (!string.IsNullOrEmpty(message))
                    return Content($"公司数据读取出错，原因[{message}]");
                else
                    return Content(string.Empty);
            }
            #endregion

            #region 转换数据
            articleInfos.ForEach(item => {
                var itemData = m_mapper.Map<vw_Article, ArticleExcelParameter>(item);
                ConvertState(item,ref itemData);
                exportData.Add(itemData);
            });
            #endregion

            #region 读取字段配置信息
            SqlWhere = $"  PageName='{pageName}' and TableName='{tableName}' and IsShow='1' order by OrderSequence asc ";
            tableColumns = m_repository.QueryPageTableColumn(SqlWhere, out message);
            #endregion

            #region 输出Excel
            Response.ContentType = ContentType;
            stream = new MemoryStream();
            if (tableColumns != null && tableColumns.Count > 0)
            {
                DataSet ExcelDs = new DataSet();
                DataTable ExcelDt=new DataTable();
                foreach (var column in tableColumns)
                {
                    ExcelDt.Columns.Add(new DataColumn() { 
                       ColumnName=column.ShowName,
                       Caption=column.FieldName,
                       DataType=typeof(System.String)
                    });
                }
                foreach(var data in exportData) 
                {
                    DataRow excelRow = ExcelDt.NewRow();
                    foreach (var propInfo in data.GetType().GetProperties()) 
                    {
                        object propValue=propInfo.GetValue(data, null);
                        string fieldName = String.Empty;
                        if (tableColumns.Any(q => q.FieldName.ToLower() == propInfo.Name.ToLower()))
                        {
                            fieldName = tableColumns.Where(q => q.FieldName.ToLower() == propInfo.Name.ToLower()).First().ShowName;
                            propValue = propValue == null ? string.Empty : Convert.ToString(propValue);
                            if (ExcelDt.Columns.Contains(fieldName))
                                excelRow[fieldName] = propValue;
                        }
                    }
                    ExcelDt.Rows.Add(excelRow);
                }
                ExcelDs.Tables.Add(ExcelDt);
                NPOIExcelHelper.DataSetToExcel(ExcelDs, ref stream, out message);
            }
            else
            {
                excelMap.Put<ArticleExcelParameter>(exportData, "sheet1", false);
                excelMap.Save(stream);
            }
            #endregion

            return File(stream.ToArray(), ContentType);
        }
        #endregion

        #region Post

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="parameter">添加参数</param>
        /// <returns>返回结果</returns>        
        [HttpPost]
        public async Task<ActionResult<Result>> AddData([FromBody] ArticleParameter parameter)
        {
            return await SaveData("Add", parameter, null);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="formData">提交表单来的数据</param>
        /// <returns>返回数据</returns>
        [HttpPost("save")]
        public async Task<ActionResult<Result>> SaveData([FromForm] IFormCollection formData) 
        {
            ArticleParameter parameter = null;
            string SaveMethod = string.Empty;
            string htmlCode = string.Empty;
            string jsonStr = string.Empty;
            Result result = new Result() { Status=0,Msg=string.Empty };
            if (formData != null&& formData.Count>0) 
            {
                SaveMethod = GetFormData("saveMethod",formData);
                jsonStr = GetFormData("articleInfo", formData);
                htmlCode= GetFormData("htmlCode", formData);
                if (!string.IsNullOrEmpty(jsonStr))
                {
                    try
                    {
                        parameter = JSONHelper.JSONToObject<ArticleParameter>(jsonStr);
                    }
                    catch (Exception exp) 
                    {
                        result.Status = -1;
                        result.Msg = $"转换JSON数据出错,原因[{exp.Message}]";
                        return result;
                    }
                }                
                if (parameter != null)
                    parameter.Content = WebUtility.HtmlEncode(htmlCode);
            }
            return await SaveData(SaveMethod, parameter, null);
        }

        /// <summary>
        /// 批量导入文章
        /// </summary>
        /// <param name="formData"></param>
        /// <returns></returns>
        [HttpPost("import/excel")]
        public async Task<ActionResult<ListResult<Result>>> ImportExcel([FromForm] IFormCollection formData) 
        {
            #region 声明和初始化

            //上传文件保存的文件夹
            string uploadFilePath = "uploadFiles";

            //上传文件保存的物理路径
            string realUploadFilePath = string.Empty;

            //网站根目录
            string wwwrootPath = string.Empty;

            //错误消息
            string message = string.Empty;

            //文件扩展名
            string fileExtension = "";

            //保存的文件名
            string SaveFileName = "";

            //非空验证
            string checkEmptyMessage = string.Empty;

            string userName = string.Empty;

            //返回值
            ListResult<Result> result = null;

            //上传的文件
            IFormFile formFile = null;

            Npoi.Mapper.Mapper excelMap;

            IdWorker snowId=new IdWorker(1,1);
            #endregion

            formFile = (formData == null || formData.Files == null || formData.Files.Count == 0) ? null : formData.Files[0];
            userName = formData["userName"];

            #region 验证文件类型
            fileExtension = Path.GetExtension(formFile.FileName);
            if (FileInfoController.checkUploadFileExtension(fileExtension, out message) == false)
            {
                result = new ListResult<Result>()
                {
                    Status = -1,
                    Msg = $"文件类型验证出错，原因[{message}]"
                };
                return result;
            }
            #endregion

            #region 保存文件
            try
            {
                wwwrootPath = m_webHostEnvironment.WebRootPath;
                realUploadFilePath = @$"{wwwrootPath}\{uploadFilePath}\";
                if (!Directory.Exists(realUploadFilePath))
                    Directory.CreateDirectory(realUploadFilePath);
                SaveFileName = $"{new IdWorker(1, 1).NextId()}{fileExtension}";
                using (var fileStream = new FileStream($@"{realUploadFilePath}\{SaveFileName}", FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                result = new ListResult<Result>()
                {
                    Status = -1,
                    Msg = $"文件保存出错，原因[{ex.Message}]"
                };
                return result;
            }
            #endregion

            #region 导入文件
            excelMap = new Npoi.Mapper.Mapper($@"{realUploadFilePath}\{SaveFileName}");
            var excelData = excelMap.Take<ArticleImportParameter>("sheet1");
            result = new ListResult<Result>();
            result.Result = new List<Result>();
            foreach (var item in excelData)
            {
                item.Value.Created = userName;
                Result itemResult = null;
                ArticleParameter saveData = null;
                try
                {
                    saveData = m_mapper.Map<ArticleParameter>(item.Value);
                    itemResult = await SaveData("Import", saveData, snowId);
                    if (itemResult.Status != 0)
                    {
                        result.Result.Add(new Result()
                        {
                            Status = -1,
                            Msg = $"第{item.RowNumber}行导入出错,原因[{itemResult.Msg}]\r\n<br/>"
                        });
                    }
                }
                catch (Exception exp) 
                {
                    result.Result.Add(new Result()
                    {
                        Status = -1,
                        Msg = $"第{item.RowNumber}行导入出错,原因[{exp.Message}]\r\n<br/>"
                    });
                }
            }
            if (result.Result.Count>0)
            {
                result.Status = -1;
                result.Msg = "部分数据导入出错";
                return result;
            }
            #endregion

            result = new ListResult<Result>() 
            {
                Status=0,
                Msg=string.Empty
            };
            return result;
        }      
        #endregion

        #region Put

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="ArticleId">文章编号</param>
        /// <param name="parameter">修改数据</param>
        /// <returns>返回结果</returns>  
        [HttpPut("{ArticleId}")]
        public async Task<ActionResult<Result>> ModifyData(string ArticleId, [FromBody] ArticleParameter parameter)
        {
            parameter.ArticleId = ArticleId;
            return await SaveData("Edit", parameter, null);
        }

        /// <summary>
        /// 更新浏览量
        /// </summary>
        /// <param name="ArticleId">文章编号</param>
        /// <returns>返回结果</returns>
        [HttpPut("update/viewcount/{ArticleId}")]
        public async Task<ActionResult<Result>> UpdateArticleViewCount(string ArticleId) 
        {
            //返回值
            Result result = new Result() { Status=0,Msg=string.Empty };
            Message message = new Message();
            message = m_repository.UpdateArticleViewCount(ArticleId);
            if (!message.Successful)
                result = new Result() { Status=-1,Msg=message.Content };
            return result;
        }
        #endregion

        #region Delete

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="IDStr">删除文章编号</param>
        /// <returns>删除结果</returns>
        [EnableCors("Cors")]
        [HttpDelete("{IDStr}")]
        public ActionResult<Result> BatchDeleteData(string IDStr)
        {
            //数据库返回值
            Message dbResult = new Message(true, string.Empty);

            //返回值
            Result result = new Result() { Status = 0, Msg = string.Empty };


            dbResult = m_repository.DeleteArticle(IDStr);
            if (!dbResult.Successful)
            {
                result = new Result()
                {
                    Status = -1,
                    Msg = $"删除生长信息关联的文件信息失败，原因[{dbResult.Content}]"
                };
                return result;
            }

            return result;
        }
        #endregion

        #region Private

        private static string GetFormData(string Key, [FromForm] IFormCollection formData) 
        {
            string result = string.Empty;
            if(formData != null && formData.Count > 0) 
            {
                if (formData.ContainsKey(Key)) 
                    result = formData[Key];
            }
            return result;
        }

        ///  <summary>
        ///  去除HTML标记
        ///  </summary>
        ///  <param  name=”NoHTML”>包括HTML的源码  </param>
        ///  <returns>已经去除后的文字</returns>
        private string NoHTML(string Htmlstring)
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "",
            RegexOptions.IgnoreCase);
            //删除HTML 
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([rn])[s]+", "",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"–>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!–.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"",
          
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "  ",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(d+);", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("rn", "");
            Htmlstring = Htmlstring.Replace("&amp;", string.Empty);
            Htmlstring = Htmlstring.Replace("ldquo;", string.Empty);
            Htmlstring = Htmlstring.Replace("rdquo;", string.Empty);
            Htmlstring = WebUtility.HtmlEncode(Htmlstring).Trim();
            Htmlstring = Htmlstring.Replace("&amp;", string.Empty);
            return Htmlstring;
        }

        private static void ConvertState(Article pageDataItem, ref ArticleParameter resultDataItem)
        {
            #region 状态值转换
            switch (pageDataItem.IsSpecial)
            {
                case 0:
                    resultDataItem.IsSpecial = "否";
                    break;
                case 1:
                    resultDataItem.IsSpecial = "是";
                    break;
            }
            switch (pageDataItem.IsTop)
            {
                case 0:
                    resultDataItem.IsTop = "否";
                    break;
                case 1:
                    resultDataItem.IsTop = "是";
                    break;
            }
            switch (pageDataItem.State)
            {
                case 0:
                    resultDataItem.State = "显示";
                    break;
                case 1:
                    resultDataItem.State = "隐藏";
                    break;
            }
            switch (pageDataItem.ArticleType)
            {
                case 0:
                    resultDataItem.ArticleType = "普通文章";
                    break;
                case 1:
                    resultDataItem.ArticleType = "图片文章";
                    break;
                case 2:
                    resultDataItem.ArticleType = "视频文章";
                    break;
            }
            #endregion
        }

        private static void ConvertState(vw_Article pageDataItem,ref ArticleParameter resultDataItem) 
        {
            #region 状态值转换
            switch (pageDataItem.IsSpecial)
            {
                case 0:
                    resultDataItem.IsSpecial = "否";
                    break;
                case 1:
                    resultDataItem.IsSpecial = "是";
                    break;
            }
            switch (pageDataItem.IsTop)
            {
                case 0:
                    resultDataItem.IsTop = "否";
                    break;
                case 1:
                    resultDataItem.IsTop = "是";
                    break;
            }
            switch (pageDataItem.State)
            {
                case 0:
                    resultDataItem.State = "显示";
                    break;
                case 1:
                    resultDataItem.State = "隐藏";
                    break;
            }
            switch (pageDataItem.ArticleType)
            {
                case 0:
                    resultDataItem.ArticleType = "普通文章";
                    break;
                case 1:
                    resultDataItem.ArticleType = "图片文章";
                    break;
                case 2:
                    resultDataItem.ArticleType = "视频文章";
                    break;
            }
            #endregion
        }

        private static void ConvertState(vw_Article pageDataItem,ref ArticleExcelParameter resultDataItem)
        {
            #region 状态值转换
            switch (pageDataItem.IsSpecial)
            {
                case 0:
                    resultDataItem.IsSpecial = "否";
                    break;
                case 1:
                    resultDataItem.IsSpecial = "是";
                    break;
            }
            switch (pageDataItem.IsTop)
            {
                case 0:
                    resultDataItem.IsTop = "否";
                    break;
                case 1:
                    resultDataItem.IsTop = "是";
                    break;
            }
            switch (pageDataItem.State)
            {
                case 0:
                    resultDataItem.State = "显示";
                    break;
                case 1:
                    resultDataItem.State = "隐藏";
                    break;
            }
            switch (pageDataItem.ArticleType)
            {
                case 0:
                    resultDataItem.ArticleType = "普通文章";
                    break;
                case 1:
                    resultDataItem.ArticleType = "图片文章";
                    break;
                case 2:
                    resultDataItem.ArticleType = "视频文章";
                    break;
            }
            #endregion
        }

        private static string RestoreStr(object str)
        {
            if (str == null)
                return string.Empty;
            string Str = Convert.ToString(str);
            if (!string.IsNullOrEmpty(Str))
            {
                Str = Str.Replace("'", "");
                Str = Str.Replace("\"", "");
                Str = Str.Replace("&amp", "&");
                Str = Str.Replace("&lt", "<");
                Str = Str.Replace("&gt", ">");

                Str = Str.Replace("delete", "");
                Str = Str.Replace("update", "");
                Str = Str.Replace("insert", "");
            }
            return Str;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="SaveMethod">[Add|Edit]</param>
        /// <param name="parameter">参数</param>
        /// <returns>返回结果</returns>
        private async Task<Result> SaveData(string SaveMethod, ArticleParameter parameter, IdWorker snowId) 
        {
            #region 声明和初始化

            //数据库返回值
            Message dbResult = null;

            //返回值
            Result result = null;

            //查询条件
            string SqlWhere = string.Empty;

            //错误消息
            string message = string.Empty;

            //验证消息
            string checkMessage = string.Empty;

            //文章表
            Article saveData = new Article();

            //文艺小组
            List<RuralCultureDataAccess.Models.Group> groups = new List<RuralCultureDataAccess.Models.Group>();

            //组织架构
            List<Organization> organizations = new List<Organization>();
            #endregion

            if (snowId == null)
                snowId = new IdWorker(1, 1);

            #region 非空验证
            if (parameter == null)
            {
                if (parameter == null)
                {
                    result = new Result()
                    {
                        Status = -1,
                        Msg = "参数不能为空"
                    };
                }
                return result;
            }
            if (string.IsNullOrEmpty(SaveMethod))
                checkMessage += "保存方式、";
            if (string.IsNullOrEmpty(parameter.ArticleTitle))
                checkMessage += "文章标题、";
            if (string.IsNullOrEmpty(parameter.Content)&&parameter.ArticleType!="2")
                checkMessage += "文章内容、";
            if (string.IsNullOrEmpty(parameter.Author))
                checkMessage += "文章作者、";
            if (!string.IsNullOrEmpty(parameter.IsSpecial) && (SaveMethod == "Import"|| SaveMethod=="Add" || SaveMethod == "Edit")) 
            {
                if (parameter.IsSpecial.Trim() == "是")
                    parameter.IsSpecial = "1";
                else if(parameter.IsSpecial.Trim() =="否" )
                    parameter.IsSpecial = "0";
                else
                    checkMessage += "是否特色文化展示只能填是或否、";
            }
            if (!string.IsNullOrEmpty(parameter.IsTop) && (SaveMethod == "Import" || SaveMethod == "Add" || SaveMethod == "Edit"))
            {
                if (parameter.IsTop.Trim() == "是")
                    parameter.IsTop = "1";
                else if (parameter.IsTop.Trim() == "否")
                    parameter.IsTop = "0";
                else
                    checkMessage += "是否置顶只能填是或否、";
            }
            if (!string.IsNullOrEmpty(parameter.State) && (SaveMethod == "Import" || SaveMethod == "Add" || SaveMethod == "Edit"))
            {
                if (parameter.State.Trim() == "隐藏")
                    parameter.State = "1";
                else if (parameter.State.Trim() == "显示")
                    parameter.State = "0";
                else
                    checkMessage += "是否置顶只能填隐藏或显示、";
            }
            if (!string.IsNullOrEmpty(checkMessage))
            {
                checkMessage = checkMessage.Substring(0, checkMessage.Length - 1);
                result = new Result()
                {
                    Status = -1,
                    Msg = $"非空验证出错，原因[{checkMessage}]不能为空"
                };
                return result;
            }
            #endregion

            #region 有效验证
            if (!string.IsNullOrEmpty(parameter.GroupId))
            {
                groups = m_repository.QueryGroup($" GroupId='{parameter.GroupId}' ", out message);
                if(groups == null||groups.Count<=0)
                {
                    if (!string.IsNullOrEmpty(message))
                        message = $"读取文艺小组错误,原因[{message}]";
                    else
                        message = $"读取文艺小组错误,原因[没有读取到文艺小组数据]";
                }
            }
            if (!string.IsNullOrEmpty(parameter.OrgId)) 
            {
                organizations = m_repository.QueryOrganization($" OrgId='{parameter.OrgId}' ", out message);
                if (organizations == null || organizations.Count <= 0) 
                {
                    if (!string.IsNullOrEmpty(message))
                        message = $"读取组织架构错误,原因[{message}]";
                    else
                        message = $"读取组织架构错误,原因[没有读取到组织架构数据]";
                }
            }
            if (!string.IsNullOrEmpty(message)) 
            {
                result = new Result()
                {
                    Status = -1,
                    Msg = message
                };
                return result;
            }
            #endregion

            #region 导入模式
            if (SaveMethod == "Import") 
            {
                groups = m_repository.QueryGroup($" GroupName like '%{parameter.GroupName}%' ", out message);
                if (groups == null || groups.Count <= 0)
                {
                    if (!string.IsNullOrEmpty(message))
                        message = $"读取文艺小组错误,原因[{message}]";
                    else 
                    {
                        groups.Add(new RuralCultureDataAccess.Models.Group() { 
                            GroupId=snowId.NextId(),
                            GroupName=parameter.GroupName,
                            Created = parameter.Created,
                            CreatedTime = DateTime.Now,
                            Modified = parameter.Created,
                            ModifiedTime = DateTime.Now
                        });
                        dbResult = m_repository.InsertGroup(groups);
                        if(!dbResult.Successful)
                            message = $"添加文艺小组错误,原因[{dbResult.Content}]";
                    }
                }
                organizations = m_repository.QueryOrganization($" OrgName like '%{parameter.OrgName}%' ", out message);
                if (organizations == null || organizations.Count <= 0)
                {
                    if (!string.IsNullOrEmpty(message))
                        message = $"读取组织架构错误,原因[{message}]";
                    else 
                    {
                        organizations.Add(new Organization() 
                        {
                            OrgId=snowId.NextId(),
                            OrgName=parameter.OrgName,
                            ParentId=0,
                            Created = parameter.Created,
                            CreatedTime = DateTime.Now,
                            Modified = parameter.Created,
                            ModifiedTime=DateTime.Now
                        });
                        dbResult = m_repository.InsertOrganization(organizations);
                        if (!dbResult.Successful)
                            message = $"添加组织架构错误,原因[{dbResult.Content}]";
                    }
                }
                if (!string.IsNullOrEmpty(message))
                {
                    result = new Result()
                    {
                        Status = -1,
                        Msg = message
                    };
                    return result;
                }
            }
            #endregion

            #region 保存数据
            if (SaveMethod == "Add"||SaveMethod== "Import")
            {
                saveData.ArticleId = 0;
                saveData.CreatedTime = DateTime.Now;
                saveData.ModifiedTime = DateTime.Now;
                saveData = m_mapper.Map<Article>(parameter);
                saveData.ArticleId = snowId.NextId();
                saveData.OrgId = organizations[0].OrgId;
                saveData.GroupId = groups[0].GroupId;
                saveData.Created = parameter.Created;
                saveData.Modified = parameter.Created;
                saveData.CreatedTime = DateTime.Now;
                saveData.ModifiedTime = DateTime.Now;
                if (saveData.ArticleType == 2)
                    saveData.Content = saveData.Backup01;
                dbResult = m_repository.InsertArticle(new List<Article>() { saveData });           
            }
            else if (SaveMethod == "Edit")
            {
                saveData = m_mapper.Map<Article>(parameter);
                SqlWhere = $" ArticleId='{saveData.ArticleId}' ";
                List<Article> isHave = m_repository.QueryArticle(SqlWhere,out message);
                if(isHave==null||isHave.Count<=0)
                {
                    if (!string.IsNullOrEmpty(message))
                        message = $"寻找原始保存数据失败，原因[{message}]";
                    else 
                        message = $"寻找原始保存数据失败，原因[没有找到数据]";                       
                    result = new Result()
                    {
                        Status = -1,
                        Msg =message
                    };
                    return result;
                }
                saveData.ViewCount= isHave[0].ViewCount.GetValueOrDefault();
                saveData.Modified = parameter.Created;
                saveData.CreatedTime = isHave[0].CreatedTime;
                saveData.ModifiedTime = DateTime.Now;
                if (saveData.ArticleType == 2)
                    saveData.Content = saveData.Backup01;
                dbResult = await m_repository.UpdateArticle(new List<Article>() { saveData }, SqlWhere);
            }
            if (dbResult != null && !dbResult.Successful)
            {
                result = new Result()
                {
                    Status = -1,
                    Msg = $"保存失败，原因[{dbResult.Content}]"
                };
                return result;
            }
            #endregion

            result = new Result()
            {
                Status=0,
                Msg=string.Empty,
            };
            return result;
        }
        #endregion
    }
}
