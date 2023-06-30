using AutoMapper;
using Common;
using ePioneer.Data.Kernel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RuralCultureDataAccess;
using RuralCultureDataAccess.Models;
using RuralCultureWebApi.Models;
using Snowflake.Net;
using System.Data;

namespace RuralCultureWebApi.Controllers
{
    /// <summary>
    /// 文艺小组
    /// </summary>
    [Route("api/group")]
    [ApiController]
    public class GroupController : Controller
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
        public GroupController(IMOIRepository repository, IMapper _mapper, IWebHostEnvironment webHostEnvironment)
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
        public ActionResult<PageResultColumn<GroupParameter>> GetPage(
            string? where = "",
            string? condition = "",
            int? pageIndex = 1,
            int? pageSize = 10,
            string? pageName = "",
            string? tableName = "",
            string sortField = "CreatedTime",
            string sortMethod = "DESC"
            )
        {
            #region 声明变量

            //总页数
            int pageCount = 0;

            //总记录数
            int totalRecordCount = 0;

            //字段列表
            string FieldStr = string.Empty;

            //方法返回错误消息
            string message = string.Empty;

            //错误消息
            string checkMessage = string.Empty;

            //页面返回值
            List<Group>? pageData = null;

            //接口返回值
            List<GroupParameter> pageResultData = new List<GroupParameter>();

            //返回值
            var result = new PageResultColumn<GroupParameter>();

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
                result = new PageResultColumn<GroupParameter>()
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
                result = new PageResultColumn<GroupParameter>()
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
                    sqlWhere.Add($" GroupName like '%{where}%' ");
                    sqlWhere.Add($" Created like '%{where}%' ");
                    sqlWhere.Add($" Modified like '%{where}%' ");
                }
                else
                    sqlWhere.Add($" {condition} like '%{where}%' ");
                where = String.Join(" Or ", sqlWhere.ToArray());
            }
            else if (where == "null")
                where = null;
            pageData = m_repository.QueryPageGroup(
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
                    result = new PageResultColumn<GroupParameter>()
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
                    result = new PageResultColumn<GroupParameter>()
                    {
                        Status = 0,
                        PageCount = 0,
                        RecordCount = 0,
                        Msg = string.Empty,
                        TableColumn = null,
                        Result = null
                    };
                    return result;
                }
            }
            #endregion

            #region 计算总页数
            if (totalRecordCount % pageSize == 0)
                pageCount = Convert.ToInt32(totalRecordCount / pageSize);
            else
                pageCount = Convert.ToInt32(totalRecordCount / pageSize) + 1;
            #endregion

            #region 赋值返回值
            pageData!.ForEach(pageDataItem => {
                var resultDataItem = m_mapper.Map<GroupParameter>(pageDataItem);
                resultDataItem.CreatedTime = pageDataItem.CreatedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");
                pageResultData.Add(resultDataItem);
            });
            result = new PageResultColumn<GroupParameter>()
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
        /// 根据文艺小组编号获得文艺小组数据
        /// </summary>
        /// <param name="groupId">文艺小组编号</param>
        /// <returns>返回数据</returns>
        [HttpGet]
        public ActionResult<ListResult<GroupParameter>> GetGroupById(string groupId)
        {
            return QueryGroup($" GroupId='{groupId}' ");
        }

        /// <summary>
        /// 根据文艺小组名称获得文艺小组数据
        /// </summary>
        /// <param name="searchKey">文艺小组名称</param>
        /// <returns>返回数据</returns>
        [HttpGet("search/name")]
        public ActionResult<ListResult<GroupParameter>> GroupFullTextSearch(string searchKey) 
        {
            return QueryGroup($" GroupName like '%{searchKey}%' ");
        }

        [HttpGet("top")]
        public ActionResult<ListResult<GroupParameter>> GroupTop() 
        {
            #region 声明变量

            //返回状态
            int resultState = 0;

            //方法返回错误消息
            string message = string.Empty;

            //查询条件
            string SqlWhere = string.Empty;

            //文艺小组数据
            List<Group> groups = new List<Group>();

            //返回数据
            List<GroupParameter> groupsParameters = new List<GroupParameter>();

            //返回值
            ListResult<GroupParameter> result = null;
            #endregion

            #region 查询数据
            groups = m_repository.QueryGroupTopData(out message, SqlWhere, 11);
            if (groups == null || groups.Count <= 0)
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
                result = new ListResult<GroupParameter>()
                {
                    Status = resultState,
                    Msg = message,
                    Result = null
                };
                return result;
            }
            #endregion

            #region 赋值返回值
            groups.ForEach(articleItem => {
                groupsParameters.Add(m_mapper.Map<GroupParameter>(articleItem));
            });
            result = new ListResult<GroupParameter>()
            {
                Status = 0,
                Msg = string.Empty,
                Result = groupsParameters
            };
            #endregion

            return result;
        }

        /// <summary>
        /// 查询所有文艺小组
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public ActionResult<ListResult<GroupParameter>> QueryAllGroup() 
        {
            return QueryGroup(string.Empty);
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="selectedIDStr">选择要导出的编号</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        [HttpGet("/api/group/export/excel")]
        public IActionResult ExportExcel(
            string selectedIDStr = "null",
            string where = "null",
            string condition = "null",
            string? pageName = "",
            string? tableName = ""
            ) 
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
            List<Group> groupsInfo = null;

            //页面字段信息
            List<PageTableColumn> tableColumns = new List<PageTableColumn>();

            //导出数据
            List<GroupParameter> exportData = new List<GroupParameter>();
            #endregion

            #region 处理ID字符串查询条件
            if (!string.IsNullOrEmpty(selectedIDStr) && selectedIDStr != "null")
            {
                IDStr = "'" + selectedIDStr.Replace("-", "','") + "'";
                whereList.Add(" GroupId in (" + IDStr + ")");
            }
            if (!string.IsNullOrEmpty(where) && where != "null")
            {
                List<string> sqlWhere = new List<string>();
                if (string.IsNullOrEmpty(condition) || condition == "null")
                {
                    sqlWhere.Add($" GroupName like '%{where}%' ");
                    sqlWhere.Add($" Created like '%{where}%' ");
                    sqlWhere.Add($" Modified like '%{where}%' ");
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
            groupsInfo = m_repository.QueryGroup(SqlWhere, out message);
            if (groupsInfo == null || groupsInfo.Count <= 0)
            {
                Response.ContentType = "text/html";
                if (!string.IsNullOrEmpty(message))
                    return Content($"文艺小组数据读取出错，原因[{message}]");
                else
                    return Content(string.Empty);
            }
            #endregion

            #region 转换数据
            groupsInfo.ForEach(item => {
                var itemData = m_mapper.Map<Group, GroupParameter>(item);
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
                DataTable ExcelDt = new DataTable();
                foreach (var column in tableColumns)
                {
                    ExcelDt.Columns.Add(new DataColumn()
                    {
                        ColumnName = column.ShowName,
                        Caption = column.FieldName,
                        DataType = typeof(System.String)
                    });
                }
                foreach (var data in exportData)
                {
                    DataRow excelRow = ExcelDt.NewRow();
                    foreach (var propInfo in data.GetType().GetProperties())
                    {
                        object propValue = propInfo.GetValue(data, null);
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
                excelMap.Put<GroupParameter>(exportData, "sheet1", false);
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
        public ActionResult<Result> AddData([FromBody] GroupParameter parameter)
        {
            return SaveData("Add", parameter, null);
        }

        /// <summary>
        /// 批量导入文章
        /// </summary>
        /// <param name="formData"></param>
        /// <returns></returns>
        [HttpPost("import/excel")]
        public async Task<ActionResult<Result>> ImportExcel([FromForm] IFormCollection formData) 
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

            //用户名
            string userName = string.Empty;

            //返回值
            Result result = null;

            //上传的文件
            IFormFile? formFile = null;

            //Excel数据
            DataSet? excelData = null;

            //雪花ID
            IdWorker snowId = new IdWorker(1, 1);

            //保存数据
            List<Group> saveData=new List<Group>();

            //数据库返回值
            Message? dbResult = null;
            #endregion

            formFile = (formData == null || formData.Files == null || formData.Files.Count == 0) ? null : formData.Files[0];
            userName = formData["userName"];

            #region 验证文件类型
            fileExtension = Path.GetExtension(formFile.FileName);
            if (FileInfoController.checkUploadFileExtension(fileExtension, out message) == false)
            {
                result = new Result()
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
                result = new Result()
                {
                    Status = -1,
                    Msg = $"文件保存出错，原因[{ex.Message}]"
                };
                return result;
            }
            #endregion

            #region 读取数据
            excelData = NPOIExcelHelper.ExcelToDataTable($@"{realUploadFilePath}\{SaveFileName}",false,out message);
            if (excelData == null || excelData.Tables.Count <= 0)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    result = new Result()
                    {
                        Status = -1,
                        Msg = $"读取Excel数据出错,原因[{message}]"
                    };
                    return result;
                }
            }
            else if (excelData.Tables[0].Rows.Count <= 0) 
            {
                result = new Result()
                {
                    Status = -1,
                    Msg = "导入数据为空"
                };
                return result;
            }
            #endregion

            #region 赋值
            foreach (DataRow row in excelData.Tables[0].Rows) 
            {
                saveData.Add(new Group()
                {
                    GroupId = snowId.NextId(),
                    GroupName = Utils.GetDataRow(row, 0)==null?string.Empty:Convert.ToString(Utils.GetDataRow(row, 0)),
                    Created= userName,
                    CreatedTime = DateTime.Now,
                    Modified=userName,
                    ModifiedTime=DateTime.Now
                });
            }
            #endregion

            #region 保存数据
            dbResult = m_repository.InsertGroup(saveData);
            if(dbResult != null && dbResult.Successful == false) 
            {
                result = new Result()
                {
                    Status= -1,
                    Msg=$"添加文艺小组出错,原因[{dbResult.Content}]"
                };
                return result;
            }
            #endregion

            result = new Result()
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
        [HttpPut("{GroupId}")]
        public ActionResult<Result> ModifyData(string GroupId, [FromBody] GroupParameter parameter)
        {
            parameter.GroupId = GroupId;
            return SaveData("Edit", parameter, null);
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


            dbResult = m_repository.DeleteGroup(IDStr);
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

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="SaveMethod">[Add|Edit]</param>
        /// <param name="parameter">参数</param>
        /// <returns>返回结果</returns>
        private Result SaveData(string SaveMethod, GroupParameter parameter, IdWorker? snowId)
        {
            #region 声明和初始化

            //数据库返回值
            Message? dbResult = null;

            //返回值
            Result? result = null;

            //查询条件
            string SqlWhere = string.Empty;

            //错误消息
            string message = string.Empty;

            //验证消息
            string checkMessage = string.Empty;

            //是否存在文艺小组
            List<Group> isHave = new List<Group>();

            //文艺小组表
            Group saveData = new Group();
            #endregion

            if (snowId == null)
                snowId = new IdWorker(1, 1);

            #region 非空验证
            if (parameter == null)
            {
                result = new Result()
                {
                    Status = -1,
                    Msg = "参数不能为空"
                };
                return result;
            }
            if (string.IsNullOrEmpty(SaveMethod))
                checkMessage += "保存方式、";
            if (SaveMethod == "Edit" && string.IsNullOrEmpty(parameter.GroupId))
                checkMessage += "文艺小组编号、";
            if (string.IsNullOrEmpty(parameter.GroupName))
                checkMessage += "文艺小组名称、";
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

            #region 查询数据
            if (SaveMethod == "Edit") 
            {
                SqlWhere = $" GroupId='{parameter.GroupId}' ";
                isHave = m_repository.QueryGroup(SqlWhere, out message);
                if (isHave == null || isHave.Count <= 0) 
                {
                    if (!string.IsNullOrEmpty(message))
                        message = $"查询文艺小组信息出错,原因[{message}]";
                    else
                        message = $"查询文艺小组信息出错,原因[没有找到文艺小组数据]";
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
            if (SaveMethod == "Add")
            {
                saveData = m_mapper!.Map<Group>(parameter);
                saveData.GroupId = snowId.NextId();
                saveData.CreatedTime = DateTime.Now;
                saveData.ModifiedTime = DateTime.Now;
                dbResult = m_repository!.InsertGroup(new List<Group>() { saveData });
            }
            else if (SaveMethod == "Edit")
            {
                saveData = m_mapper!.Map<Group>(parameter);
                saveData.Created = isHave[0].Created;
                saveData.CreatedTime = isHave[0].CreatedTime;
                saveData.ModifiedTime = DateTime.Now;
                SqlWhere = $" GroupId='{saveData.GroupId}' ";
                dbResult = m_repository!.UpdateGroup(new List<Group>() { saveData }, SqlWhere);
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
                Msg=string.Empty
            };
            return result;
        }

        /// <summary>
        /// 查询文艺小组数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <returns>返回数据</returns>
        private ActionResult<ListResult<GroupParameter>> QueryGroup(string SqlWhere)
        {
            #region 声明变量

            //方法返回错误消息
            string message = string.Empty;

            //文章数据
            List<Group> groups = new List<Group>();

            //返回数据
            List<GroupParameter> groupParameters = new List<GroupParameter>();

            //返回值
            ListResult<GroupParameter> result = null;
            #endregion

            #region 查询数据
            groups = m_repository!.QueryGroup(SqlWhere, out message);
            if (groups == null || groups.Count <= 0)
            {
                if (!string.IsNullOrEmpty(message))
                    message = $"查询文艺小组数据出错,原因[{message}]";
                else
                    message = $"查询文艺小组数据出错,原因[没有查到任何数据]";
                result = new ListResult<GroupParameter>()
                {
                    Status = -1,
                    Msg = message,
                    Result = null
                };
                return result;
            }
            #endregion

            #region 赋值返回值
            groups.ForEach(groupsItem => {
                groupParameters.Add(m_mapper!.Map<GroupParameter>(groupsItem));
            });
            result = new ListResult<GroupParameter>()
            {
                Status = 0,
                Msg = string.Empty,
                Result = groupParameters
            };
            #endregion

            return result;
        }
        #endregion
    }
}
