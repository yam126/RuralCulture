using AutoMapper;
using ePioneer.Data.Kernel;
using Microsoft.AspNetCore.Mvc;
using RuralCultureDataAccess;
using RuralCultureDataAccess.Models;
using RuralCultureWebApi.Models;
using Snowflake.Net;

namespace RuralCultureWebApi.Controllers
{
    /// <summary>
    /// 页面字段列表
    /// </summary>
    [Route("api/page/table/column")]
    [ApiController]
    public class PageTableColumnController : ControllerBase
    {
        #region Fields

        /// <summary>
        /// 数据库操作类
        /// </summary>
        private IMOIRepository m_repository;

        /// <summary>
        /// AutoMapper参数映射类
        /// </summary>
        private IMapper m_mapper;

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
        public PageTableColumnController(IMOIRepository repository, IMapper _mapper, IWebHostEnvironment webHostEnvironment)
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
        public async Task<ActionResult<PageResultColumn<PageTableColumnParameter>>> GetPageTableColumn(
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
            List<PageTableColumnParameter> pageResultData = new List<PageTableColumnParameter>();

            //返回值
            var result = new PageResultColumn<PageTableColumnParameter>();
            #endregion

            #region 非空验证
            if (pageIndex == null)
                checkMessage += "当前页、";
            if (pageSize == null)
                checkMessage += "每页记录数、";
            if (!string.IsNullOrEmpty(checkMessage))
            {
                checkMessage = checkMessage.Substring(0, checkMessage.Length - 1);
                result = new PageResultColumn<PageTableColumnParameter>()
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
                result = new PageResultColumn<PageTableColumnParameter>()
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
                sqlWhere.Add($" PageName like '%{where}%' ");
                sqlWhere.Add($" TableName like '%{where}%' ");
                sqlWhere.Add($" FieldName like '%{where}%' ");
                sqlWhere.Add($" ShowName like '%{where}%' ");
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
                    result = new PageResultColumn<PageTableColumnParameter>()
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
                    result = new PageResultColumn<PageTableColumnParameter>()
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
                var resultDataItem = m_mapper.Map<PageTableColumnParameter>(pageDataItem);
                resultDataItem.CreatedTime = pageDataItem.CreatedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");
                resultDataItem.ModifiedTime = pageDataItem.ModifiedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");
                pageResultData.Add(resultDataItem);
            });
            result = new PageResultColumn<PageTableColumnParameter>()
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
        public async Task<ActionResult<PageResult<PageTableColumnParameter>>> GetPageTableColumn(
            string? where = "",
            string? condition = "",
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
            List<PageTableColumn> pageData = null;

            //接口返回值
            List<PageTableColumnParameter> pageResultData = new List<PageTableColumnParameter>();

            //返回值
            var result = new PageResult<PageTableColumnParameter>();
            #endregion

            #region 非空验证
            if (pageIndex == null)
                checkMessage += "当前页、";
            if (pageSize == null)
                checkMessage += "每页记录数、";
            if (!string.IsNullOrEmpty(checkMessage))
            {
                checkMessage = checkMessage.Substring(0, checkMessage.Length - 1);
                result = new PageResult<PageTableColumnParameter>()
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
                result = new PageResult<PageTableColumnParameter>()
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
                if (string.IsNullOrEmpty(condition) || condition == "null")
                {
                    sqlWhere.Add($" PageName like '%{where}%' ");
                    sqlWhere.Add($" TableName like '%{where}%' ");
                    sqlWhere.Add($" FieldName like '%{where}%' ");
                    sqlWhere.Add($" ShowName like '%{where}%' ");
                }
                else
                    sqlWhere.Add($" {condition} like '%{where}%' ");
                where = String.Join(" Or ", sqlWhere.ToArray());
            }
            else if (where == "null")
                where = null;
            pageData = m_repository.QueryPagePageTableColumn(
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
                    result = new PageResult<PageTableColumnParameter>()
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
                    result = new PageResult<PageTableColumnParameter>()
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
                var resultDataItem = m_mapper.Map<PageTableColumnParameter>(pageDataItem);
                resultDataItem.CreatedTime = pageDataItem.CreatedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");
                resultDataItem.ModifiedTime = pageDataItem.ModifiedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");
                pageResultData.Add(resultDataItem);
            });
            result = new PageResult<PageTableColumnParameter>()
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
        /// 根据文章ID查询文章
        /// </summary>
        /// <param name="recordId">记录编号</param>
        /// <returns>返回数据</returns>
        [HttpGet]
        public async Task<ActionResult<ListResult<PageTableColumnParameter>>> GetDataById(string recordId)
        {
            #region 声明变量

            //返回状态
            int resultState = 0;

            //方法返回错误消息
            string message = string.Empty;

            //查询条件
            string SqlWhere = string.Empty;

            //查询数据
            List<PageTableColumn> queryData= new List<PageTableColumn>();

            //返回数据
            List<PageTableColumnParameter> pageTableColumnParameters = new List<PageTableColumnParameter>();

            //返回值
            ListResult<PageTableColumnParameter> result = null;
            #endregion

            #region 参数验证
            if (string.IsNullOrEmpty(recordId))
            {
                result = new ListResult<PageTableColumnParameter>()
                {
                    Status = -1,
                    Msg = "文章编号不能为空",
                    Result = null
                };
                return result;
            }
            #endregion

            #region 查询数据
            SqlWhere = $" recordId='{recordId}' ";
            queryData = m_repository.QueryPageTableColumn(SqlWhere, out message);
            if (queryData == null || queryData.Count <= 0)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    resultState = -1;
                    message = $"查询编号为[{recordId}]的文章数据出错,原因[{message}]";
                }
                else
                {
                    resultState = 0;
                    message = $"查询编号为[{recordId}]的文章数据出错,原因[没有查到任何数据]";
                }
                result = new ListResult<PageTableColumnParameter>()
                {
                    Status = resultState,
                    Msg = message,
                    Result = null
                };
                return result;
            }
            #endregion

            #region 赋值返回值
            queryData.ForEach(Item => {
                pageTableColumnParameters.Add(m_mapper.Map<PageTableColumnParameter>(Item));
            });
            result = new ListResult<PageTableColumnParameter>()
            {
                Status = 0,
                Msg = string.Empty,
                Result = pageTableColumnParameters
            };
            #endregion

            return result;
        }

        /// <summary>
        /// 根据文章ID查询文章
        /// </summary>
        /// <param name="PageName">页面名称</param>
        /// <param name="TableName">表格名称</param>
        /// <returns>返回数据</returns>
        [HttpGet("PageName/TableName")]
        public async Task<ActionResult<ListResult<PageTableColumnParameter>>> GetDataByPageNameAndTableName(string PageName,string TableName)
        {
            #region 声明变量

            //返回状态
            int resultState = 0;

            //方法返回错误消息
            string message = string.Empty;

            string checkEmptyMessage = string.Empty;

            //查询条件
            string SqlWhere = string.Empty;

            //查询数据
            List<PageTableColumn> queryData = new List<PageTableColumn>();

            //返回数据
            List<PageTableColumnParameter> pageTableColumnParameters = new List<PageTableColumnParameter>();

            //返回值
            ListResult<PageTableColumnParameter> result = null;
            #endregion

            #region 参数验证
            if (string.IsNullOrEmpty(PageName))
                checkEmptyMessage += "页面名称、";
            if (string.IsNullOrEmpty(TableName))
                checkEmptyMessage += "表格名称、";
            if (!string.IsNullOrEmpty(checkEmptyMessage))
            {
                checkEmptyMessage= checkEmptyMessage.Substring(0, checkEmptyMessage.Length - 1);
                result = new ListResult<PageTableColumnParameter>()
                {
                    Status = -1,
                    Msg = $"非空验证出错,原因[{checkEmptyMessage}]",
                    Result = null
                };
                return result;
            }

            #endregion

            #region 查询数据
            SqlWhere = $" PageName='{PageName}' and TableName='{TableName}' order by OrderSequence asc ";
            queryData = m_repository.QueryPageTableColumn(SqlWhere, out message);
            if (queryData == null || queryData.Count <= 0)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    resultState = -1;
                    message = $"查询页面名称为[{PageName}]和表格名称为[{TableName}]的页面表格数据出错,原因[{message}]";
                }
                else
                {
                    resultState = 0;
                    message = $"查询页面名称为[{PageName}]和表格名称为[{TableName}]的页面表格数据出错,原因[{message}]";
                }
                result = new ListResult<PageTableColumnParameter>()
                {
                    Status = resultState,
                    Msg = message,
                    Result = null
                };
                return result;
            }
            #endregion

            #region 赋值返回值
            queryData.ForEach(Item => {
                pageTableColumnParameters.Add(m_mapper.Map<PageTableColumnParameter>(Item));
            });
            result = new ListResult<PageTableColumnParameter>()
            {
                Status = 0,
                Msg = string.Empty,
                Result = pageTableColumnParameters
            };
            #endregion

            return result;
        }
        #endregion

        #region Post

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="parameter">添加参数</param>
        /// <returns>返回结果</returns>        
        [HttpPost]
        public async Task<ActionResult<Result>> SaveData([FromBody] List<PageTableColumnParameter> parameter)
        {
            return await SaveData(parameter, null);
        }
        #endregion

        #region Private

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns>返回结果</returns>
        private async Task<Result> SaveData(List<PageTableColumnParameter> parameter, IdWorker snowId)
        {
            #region 声明和初始化

            //数据库返回值
            Message dbResult = null;

            //返回值
            Result result = null;

            //查询条件
            string SqlWhere = string.Empty;

            //非空验证消息
            string checkEmptyMessage = string.Empty;

            //错误消息
            string message = string.Empty;

            //验证消息
            string checkMessage = string.Empty;

            //所有表格名称
            List<string> allTableName = new List<string>();

            //文章表
            PageTableColumn saveData = new PageTableColumn();
            #endregion

            if (snowId == null)
                snowId = new IdWorker(1, 1);

            #region 非空验证
            if (parameter == null|| parameter.Count<=0) 
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
            for(var i = 0; i < parameter.Count; i++) 
            {
                var itemParm=parameter[i];
                checkEmptyMessage = string.Empty;
                if (string.IsNullOrEmpty(itemParm.PageName))
                    checkEmptyMessage += "页面名称、";
                if (string.IsNullOrEmpty(itemParm.FieldName))
                    checkEmptyMessage += "字段名、";
                if (string.IsNullOrEmpty(itemParm.ShowName))
                    checkEmptyMessage += "显示名、";
                if (string.IsNullOrEmpty(itemParm.TableName))
                    checkEmptyMessage += "表格编号、";
                if (!string.IsNullOrEmpty(checkEmptyMessage)) 
                {
                    checkEmptyMessage=checkEmptyMessage.Substring(0,checkEmptyMessage.Length-1);
                    message += $"第{(i+1)}行非空验证出错,原因[{checkEmptyMessage}],";
                }
            }
            if (!string.IsNullOrEmpty(message)) 
            {
                message = message.Substring(0,message.Length-1);
                result = new Result()
                {
                    Status = -1,
                    Msg = message
                };
                return result;
            }
            #endregion

            #region 有效验证
            //parameter.ForEach(item => {
            //    if (!allTableName.Contains(item.TableName))
            //    {
            //        SqlWhere += $"'{item.TableName}',";
            //        allTableName.Add(item.TableName);
            //    }
            //});
            //SqlWhere= string.IsNullOrEmpty(SqlWhere)?string.Empty:SqlWhere.Substring(0,SqlWhere.Length-1);
            //allTableName=m_repository.GetAllTableName(SqlWhere,out message);
            //if (allTableName==null|| allTableName.Count<=0) 
            //{
            //    if (!string.IsNullOrEmpty(message)) 
            //    {
            //        result = new Result()
            //        {
            //            Status = -1,
            //            Msg = $"查找表格名称[{SqlWhere}]出错,原因[{message}]"
            //        };
            //        return result;
            //    }
            //    else 
            //    {
            //        result = new Result()
            //        {
            //            Status = -1,
            //            Msg = $"查找表格名称[{SqlWhere}]出错,原因[查找不到表格数据]"
            //        };
            //        return result;
            //    }
            //}
            #endregion

            #region 保存数据
            for(var i= 0; i < parameter.Count; i++)
            {
                var item = parameter[i];
                List<PageTableColumn> insertData=new List<PageTableColumn>();
                SqlWhere = $" PageName='{item.PageName}' and  TableName='{item.TableName}' and FieldName='{item.FieldName}' ";
                List<PageTableColumn> isHave=m_repository.QueryPageTableColumn(SqlWhere,out message);
                if (isHave == null || isHave.Count <= 0) 
                {
                    saveData = m_mapper.Map<PageTableColumn>(parameter[i]);
                    saveData.RecordId=snowId.NextId();
                    saveData.CreatedTime = DateTime.Now;
                    dbResult = await m_repository.InsertPageTableColumn(new List<PageTableColumn>() { saveData });
                }
                else
                {
                    saveData = isHave[0];
                    saveData = m_mapper.Map<PageTableColumn>(parameter[i]);
                    saveData.RecordId = isHave[0].RecordId;
                    saveData.Created = isHave[0].Created;
                    saveData.CreatedTime=isHave[0].CreatedTime;
                    saveData.ModifiedTime = DateTime.Now;
                    SqlWhere = $" RecordId='{isHave[0].RecordId}' ";
                    dbResult = await m_repository.UpdatePageTableColumn(new List<PageTableColumn>() { saveData }, SqlWhere);
                }
                if (!dbResult.Successful) 
                    message += $"第{i+1}行保存数据出错,原因[{dbResult.Content}],";
            }
            #endregion

            #region 处理返回值
            if (!string.IsNullOrEmpty(message))
            {
                result = new Result()
                {
                    Status = -1,
                    Msg = message,
                };
            }
            else
            {
                result = new Result()
                {
                    Status = 0,
                    Msg = string.Empty,
                };
            }
            #endregion

            return result;
        }
        #endregion
    }
}
