using AutoMapper;
using Common;
using ePioneer.Data.Kernel;
using Microsoft.AspNetCore.Mvc;
using RuralCultureDataAccess;
using RuralCultureDataAccess.Models;
using RuralCultureWebApi.Models;
using Snowflake.Net;
using System.Data;

namespace RuralCultureWebApi.Controllers
{
    /// <summary>
    /// 组织架构
    /// </summary>
    [Route("api/organization")]
    [ApiController]
    public class OrganizationController : Controller
    {
        #region Fields

        /// <summary>
        /// 数据库操作类
        /// </summary>
        private readonly IMOIRepository? m_repository;

        /// <summary>
        /// AutoMapper参数映射类
        /// </summary>
        private readonly IMapper? m_mapper;

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
        public OrganizationController(IMOIRepository repository, IMapper _mapper, IWebHostEnvironment webHostEnvironment)
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
        public ActionResult<PageResultColumn<OrganizationParameter>> GetPage(
            string? where = "",
            string? parentId = "",
            int? pageIndex = 1,
            int? pageSize = 10,
            string? pageName = "OrganizationManage.cshtml",
            string? tableName = "Organization",
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

            //查询列表
            List<string> sqlWhere = new List<string>();

            //页面返回值
            List<Organization> pageData = null;

            //接口返回值
            List<OrganizationParameter> pageResultData = new List<OrganizationParameter>();

            //返回值
            var result = new PageResultColumn<OrganizationParameter>();

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
                result = new PageResultColumn<OrganizationParameter>()
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
                result = new PageResultColumn<OrganizationParameter>()
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
                sqlWhere.Add($" OrgName like '%{where}%' ");
            if (!string.IsNullOrEmpty(parentId) && parentId != "null")
                sqlWhere.Add($" parentId='{parentId}' ");
            if (sqlWhere.Count > 0)
                where = string.Join(" and ", sqlWhere);
            else
                where = null;
            pageData = m_repository!.QueryPageOrganization(
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
                    result = new PageResultColumn<OrganizationParameter>()
                    {
                        Status = -1,
                        PageCount = 0,
                        RecordCount = 0,
                        Msg = $"查询数据出错，原因[{message}]",
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
                var resultDataItem = m_mapper!.Map<OrganizationParameter>(pageDataItem);
                resultDataItem.CreatedTime = pageDataItem.CreatedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");
                pageResultData.Add(resultDataItem);
            });
            result = new PageResultColumn<OrganizationParameter>()
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
        /// 根据组织架构编号获得组织架构数据
        /// </summary>
        /// <param name="groupId">组织架构编号</param>
        /// <returns>返回数据</returns>
        [HttpGet]
        public ActionResult<ListResult<OrganizationParameter>> GetOrganizationById(string groupId)
        {
            return QueryOrganization($" OrgId='{groupId}' ");
        }

        /// <summary>
        /// 根据组织架构名称获得组织架构数据
        /// </summary>
        /// <param name="OrgName">组织架构名称</param>
        /// <returns>返回数据</returns>
        [HttpGet("search/name")]
        public ActionResult<ListResult<OrganizationParameter>> SearchOrganizationByName(string OrgName)
        {
            return QueryOrganization($" OrgName like '%{OrgName}%' ");
        }

        /// <summary>
        /// 获得指定父节点的子节点数据
        /// </summary>
        /// <param name="OrgId">父节点编号</param>
        /// <returns></returns>
        [HttpGet("search/child/{OrgId}")]
        public ActionResult<ListResult<OrganizationParameter>> GetChildOrganization(string OrgId)
        {
            string SqlWhere = string.Empty;
            SqlWhere = $" ParentId={OrgId} ";
            return QueryOrganization(SqlWhere);
        }

        /// <summary>
        /// 递归调用组织架构
        /// </summary>
        /// <param name="OrgId">组织架构编号</param>
        /// <returns></returns>
        [HttpGet("recurrence")]
        public ActionResult<EntityResult<ResultOrganizationTreeData>> GetRecurrenceOrgDataByApi(string OrgId,int level) 
        {
            string message = string.Empty;
            ResultOrganizationTreeData resultData = new ResultOrganizationTreeData();
            EntityResult<ResultOrganizationTreeData> result = new EntityResult<ResultOrganizationTreeData>();
            List<Organization> orgData = new List<Organization>();
            if (!string.IsNullOrEmpty(OrgId) && OrgId != "0") 
            {
                orgData = m_repository.QueryOrganization($" OrgId='{OrgId}' ", out message);
                if (orgData != null && orgData.Count > 0) 
                {
                    resultData.OrgName = orgData[0].OrgName;
                    resultData.FontColor= orgData[0].FontColor;
                    resultData.OrgId = OrgId;
                }
            }
            else 
            {
                resultData.OrgName = "组织架构";
                resultData.FontColor = string.Empty;
            }
            resultData.OrgId=OrgId;
            GetRecurrenceOrgData(ref resultData, level,out message);
            if(!string.IsNullOrEmpty(message))
            {
                result = new EntityResult<ResultOrganizationTreeData>()
                {
                    Status = -1,
                    Msg = message,
                    Result = null
                };
                return result;
            }
            result = new EntityResult<ResultOrganizationTreeData>() 
            {
                Status=0,
                Msg = string.Empty,
                Result=resultData
            };
            return result;
        }

        [HttpGet("/api/organization/export/excel")]
        public IActionResult ExportExcel(
            string ParentId = "null",
            string selectedIDStr = "null",
            string where = "null",
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

            //组织架构数据
            List<Organization> organizations = new List<Organization>();

            //页面字段信息
            List<PageTableColumn> tableColumns = new List<PageTableColumn>();

            //导出数据
            List<OrganizationParameter> exportData = new List<OrganizationParameter>();
            #endregion

            #region 处理ID字符串查询条件
            if (!string.IsNullOrEmpty(selectedIDStr) && selectedIDStr != "null")
            {
                IDStr = "'" + selectedIDStr.Replace("-", "','") + "'";
                whereList.Add(" OrgId in (" + IDStr + ")");
            }
            if (!string.IsNullOrEmpty(where) && where != "null")
            {
                SqlWhere = $" OrgName like '%{where}%' ";
                whereList.Add(SqlWhere);
            }
            if (!string.IsNullOrEmpty(ParentId) && ParentId != "null")
            {
                SqlWhere = $" ParentId='{ParentId}' ";
                whereList.Add(SqlWhere);
            }
            if (whereList.Count > 0)
                SqlWhere = string.Join(" and ", whereList);
            #endregion

            #region 查询数据
            organizations = m_repository.QueryOrganization(SqlWhere, out message);
            if (organizations == null || organizations.Count <= 0)
            {
                Response.ContentType = "text/html";
                if (!string.IsNullOrEmpty(message))
                    return Content($"文艺小组数据读取出错，原因[{message}]");
                else
                    return Content(string.Empty);
            }
            #endregion

            #region 转换数据
            organizations.ForEach(item => {
                var itemData = m_mapper.Map<Organization, OrganizationParameter>(item);
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
                excelMap.Put<OrganizationParameter>(exportData, "sheet1", false);
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
        public ActionResult<Result> AddData([FromBody] OrganizationParameter parameter)
        {
            return SaveData("Add", parameter, null);
        }
        #endregion

        #region Put

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="ArticleId">文章编号</param>
        /// <param name="parameter">修改数据</param>
        /// <returns>返回结果</returns>  
        [HttpPut("{OrgId}")]
        public ActionResult<Result> ModifyData(string OrgId, [FromBody] OrganizationParameter parameter)
        {
            parameter.OrgId = OrgId;
            return SaveData("Edit", parameter, null);
        }
        #endregion

        #region Delete

        /// <summary>
        /// 批量删除组织架构
        /// </summary>
        /// <param name="deleteIdStr">删除字符串编号</param>
        /// <returns></returns>
        [HttpDelete]
        public Result BatchDeleteOrganization(string deleteIdStr) 
        {
            #region 声明变量

            //返回消息
            Message dbResult = new Message();

            //错误消息
            string message = string.Empty;

            //要删除的List
            List<string> deleteIdList = null;

            //删除数据的数组
            string[] deleteIdAry = null;

            //返回值
            Result result = new Result();
            #endregion

            if (string.IsNullOrEmpty(deleteIdStr)) 
            {
                result = new Result() 
                {
                    Status=-1,
                    Msg="要删除的组织架构编号不能为空"
                };
                return result;
            }

            #region 处理要删除的组织架构
            try
            {
                deleteIdList = deleteIdStr.Split("-").ToList();
                deleteIdAry = deleteIdStr.Split("-");
            } 
            catch (Exception ex) 
            {
                result = new Result() 
                {
                    Status=-1,
                    Msg=$"处理要删除的组织架构编号出错,原因[{ex.Message}]"
                };
                return result;
            }
            #endregion

            #region 获得要删除的组织架构编号
            for (var i = 0; i < deleteIdAry.Length; i++) 
                GetRecurrenceDeleteData(deleteIdAry[i], ref deleteIdList);
            #endregion

            #region 批量设置文章的组织架构
            dbResult = m_repository!.UpdateCleanArticleByOrgId(deleteIdList);
            if (!dbResult.Successful)
            {
                result = new Result()
                {
                    Status = -1,
                    Msg = dbResult.Content
                };
                return result;
            }
            #endregion

            #region 批量删除组织架构
            dbResult = m_repository!.DeleteOrganizationByOrgId(deleteIdList);
            if (!dbResult.Successful) 
            {
                result = new Result() 
                {
                    Status=-1,
                    Msg=dbResult.Content
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

        #region Private

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="SaveMethod">[Add|Edit]</param>
        /// <param name="parameter">参数</param>
        /// <returns>返回结果</returns>
        private Result SaveData(string SaveMethod, OrganizationParameter parameter, IdWorker? snowId)
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

            //组织架构表
            Organization saveData = new Organization();

            //上级组织架构
            List<Organization> parentOrg = new List<Organization>();
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
            if (SaveMethod == "Edit" && string.IsNullOrEmpty(parameter.OrgId))
                checkMessage += "组织架构编号、";
            if (string.IsNullOrEmpty(parameter.ParentId))
                checkMessage += "上级组织架构编号、";
            if (string.IsNullOrEmpty(parameter.OrgName))
                checkMessage += "组织架构名称、";
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

            #region 验证上级组织架构
            if (parameter.ParentId != "0")
            {
                parentOrg = m_repository!.QueryOrganization($" OrgId='{parameter.ParentId}' ", out message);
                if (parentOrg == null || parentOrg.Count <= 0)
                {
                    if (!string.IsNullOrEmpty(message))
                        message = $"查询编号为[{parameter.ParentId}]上级组织架构出错,原因[{message}]";
                    else
                        message = $"查询编号为[{parameter.ParentId}]上级组织架构出错,原因[没有查询到上级组织架构]";
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
                saveData = m_mapper!.Map<Organization>(parameter);
                saveData.OrgId = snowId.NextId();
                saveData.CreatedTime = DateTime.Now;
                saveData.ModifiedTime = DateTime.Now;
                dbResult = m_repository!.InsertOrganization(new List<Organization>() { saveData });
            }
            else if (SaveMethod == "Edit")
            {
                saveData = m_mapper!.Map<Organization>(parameter);
                saveData.CreatedTime = saveData.CreatedTime;
                saveData.ModifiedTime = DateTime.Now;
                SqlWhere = $" OrgId='{saveData.OrgId}' ";
                dbResult = m_repository!.UpdateOrganization(new List<Organization>() { saveData }, SqlWhere);
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
        /// 查询组织架构数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <returns>返回数据</returns>
        private ActionResult<ListResult<OrganizationParameter>> QueryOrganization(string SqlWhere)
        {
            #region 声明变量

            //方法返回错误消息
            string message = string.Empty;

            //组织架构数据
            List<Organization> organizations = new List<Organization>();

            //返回数据
            List<OrganizationParameter> organizationParameters = new List<OrganizationParameter>();

            //返回值
            ListResult<OrganizationParameter> result = null;
            #endregion

            #region 查询数据
            organizations = m_repository!.QueryOrganization(SqlWhere, out message);
            if (organizations == null || organizations.Count <= 0)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    message = $"查询组织架构数据出错,原因[{message}]";
                    result = new ListResult<OrganizationParameter>()
                    {
                        Status = -1,
                        Msg = message,
                        Result = null
                    };
                    return result;
                }
            }
            #endregion

            #region 赋值返回值
            organizations.ForEach(groupsItem => {
                organizationParameters.Add(m_mapper!.Map<OrganizationParameter>(groupsItem));
            });
            result = new ListResult<OrganizationParameter>()
            {
                Status = 0,
                Msg = string.Empty,
                Result = organizationParameters
            };
            #endregion

            return result;
        }

        /// <summary>
        /// 递归读取组织架构数据
        /// </summary>
        /// <param name="root">当前组织架构数据</param>
        /// <param name="level">读取层级</param>
        /// <returns>返回数据</returns>
        private void GetRecurrenceOrgData(ref ResultOrganizationTreeData root,int level,out string message) 
        {
            message=string.Empty;
            string SqlWhere = string.Empty;
            List<Organization> DBData = new List<Organization>();
            SqlWhere = $" ParentId='{root.OrgId}' ";
            level -= 1;
            root.Children = new List<ResultOrganizationTreeData>();
            DBData = m_repository!.QueryOrganization(SqlWhere, out message);
            if(DBData == null || DBData.Count <= 0)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    message = $"读取组织架构出错,原因[{message}]";
                    return;
                }               
                return;
            }
            foreach(var DBDataItem in DBData) 
            {
                bool isHaveChildren = false;
                var ChildRenItem=new ResultOrganizationTreeData()
                {
                    OrgId = DBDataItem.OrgId.GetValueOrDefault().ToString(),
                    OrgName = DBDataItem.OrgName,
                    FontColor= DBDataItem.FontColor
                };
                isHaveChildren = m_repository.OrganizationIsHaveChildren(DBDataItem.OrgId.GetValueOrDefault().ToString(), out message);
                if(isHaveChildren&& level>0)
                    GetRecurrenceOrgData(ref ChildRenItem, level,out message);
                root.Children.Add(ChildRenItem);
            }
        }

        /// <summary>
        /// 获取要递归删除的数据
        /// </summary>
        /// <param name="OrgId">组织编号</param>
        /// <param name="deleteOrgId">要删除的数组</param>
        /// <returns>返回数据</returns>
        private void GetRecurrenceDeleteData(string OrgId,ref List<string> deleteOrgId) 
        {
            #region 声明变量

            //错误消息
            string message = string.Empty;

            //组织架构数据
            List<Organization> organizations=new List<Organization>();

            //子组织架构数据
            List<Organization> childOrganizations = new List<Organization>();
            #endregion

            if (deleteOrgId == null)
                deleteOrgId = new List<string>();

            organizations=m_repository.QueryOrganization($" ParentId='{OrgId}' ",out message);
            if (organizations != null && organizations.Count > 0) 
            {
                for (var i = 0; i < organizations.Count; i++)
                {
                    string currentOrgId = organizations[i].OrgId.GetValueOrDefault().ToString();
                    deleteOrgId.Add(currentOrgId);
                    childOrganizations = m_repository.QueryOrganization($" ParentId='{currentOrgId}' ", out message);
                    if (childOrganizations != null && childOrganizations.Count > 0)
                        GetRecurrenceDeleteData(currentOrgId, ref deleteOrgId);
                }                
            }
        }
        #endregion
    }
}
