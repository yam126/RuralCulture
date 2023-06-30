using AutoMapper;
using Common;
using ePioneer.Data.Kernel;
using Microsoft.AspNetCore.Mvc;
using RuralCultureDataAccess;
using RuralCultureDataAccess.Models;
using RuralCultureWebApi.Models;
using Snowflake.Net;

namespace RuralCultureWebApi.Controllers
{
    /// <summary>
    /// 文件信息
    /// </summary>
    [Route("api/fileinfo")]
    [ApiController]
    public class FileInfoController : Controller
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
        public FileInfoController(IMOIRepository repository, IMapper _mapper, IWebHostEnvironment webHostEnvironment)
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
        public ActionResult<PageResultColumn<FileInfoParameter>> GetPage(
            string? where = "",
            string? condition = "",
            int? pageIndex = 1,
            int? pageSize = 10,
            string? pageName = "FileManage.cshtml",
            string? tableName = "FileInfo",
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
            List<RuralCultureDataAccess.Models.FileInfo>? pageData = null;

            //接口返回值
            List<FileInfoParameter> pageResultData = new List<FileInfoParameter>();

            //返回值
            var result = new PageResultColumn<FileInfoParameter>();

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
                result = new PageResultColumn<FileInfoParameter>()
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
                result = new PageResultColumn<FileInfoParameter>()
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
                    sqlWhere.Add($" FileName like '%{where}%' ");
                    sqlWhere.Add($" SrcFileName like '%{where}%' ");
                    sqlWhere.Add($" FileType like '%{where}%' ");
                }
                else
                    sqlWhere.Add($" {condition} like '%{where}%' ");
                where = String.Join(" Or ", sqlWhere.ToArray());
            }
            else if (where == "null")
                where = null;
            pageData = m_repository.QueryPageFileInfo(
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
                    result = new PageResultColumn<FileInfoParameter>()
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
                var resultDataItem = m_mapper.Map<FileInfoParameter>(pageDataItem);
                resultDataItem.CreatedTime = pageDataItem.CreatedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");
                pageResultData.Add(resultDataItem);
            });
            result = new PageResultColumn<FileInfoParameter>()
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
        /// 根据文件ID查询文章
        /// </summary>
        /// <param name="FileId">文件编号</param>
        /// <returns>返回数据</returns>
        [HttpGet]
        public ListResult<FileInfoParameter> GetFileInfoById(string FileId)
        {
            #region 声明变量

            //方法返回错误消息
            string message = string.Empty;

            //查询条件
            string SqlWhere = string.Empty;

            //文章数据
            List<RuralCultureDataAccess.Models.FileInfo> fileInfos = new List<RuralCultureDataAccess.Models.FileInfo>();

            //返回数据
            List<FileInfoParameter> fileInfoParameters = new List<FileInfoParameter>();

            //返回值
            ListResult<FileInfoParameter>? result = null;
            #endregion

            #region 参数验证
            if (string.IsNullOrEmpty(FileId))
            {
                result = new ListResult<FileInfoParameter>()
                {
                    Status = -1,
                    Msg = "文章编号不能为空",
                    Result = null
                };
                return result;
            }
            #endregion

            #region 查询数据
            SqlWhere = $" FileId='{FileId}' ";
            fileInfos = m_repository.QueryFileInfo(SqlWhere, out message);
            if (fileInfos == null || fileInfos.Count <= 0)
            {
                if (!string.IsNullOrEmpty(message))
                    message = $"查询编号为[{FileId}]的文章数据出错,原因[{message}]";
                else
                    message = $"查询编号为[{FileId}]的文章数据出错,原因[没有查到任何数据]";
                result = new ListResult<FileInfoParameter>()
                {
                    Status = -1,
                    Msg = message,
                    Result = null
                };
                return result;
            }
            #endregion

            #region 赋值返回值
            fileInfos.ForEach(fileInfoItem => {
                fileInfoParameters.Add(m_mapper.Map<FileInfoParameter>(fileInfoItem));
            });
            result = new ListResult<FileInfoParameter>()
            {
                Status = 0,
                Msg = string.Empty,
                Result = fileInfoParameters
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
        public ActionResult<Result> AddData([FromBody] FileInfoParameter parameter)
        {
            return SaveData(parameter, null);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns>返回值</returns>
        [HttpPost("uploadfile")]
        public EntityResult<FileUploadResult> UploadFiles([FromForm] IFormCollection formCollection)
        {
            #region 声明变量

            IFormFileCollection fileCollection;

            IConfiguration configuration;

            string message = string.Empty;

            string DirectoryPath = string.Empty;

            string? fileExtension = string.Empty;

            string? MaxFileLength = string.Empty;

            string itemFullFileName = string.Empty;

            string? UploadFilePath = string.Empty;

            string itemFileName = string.Empty;

            long showMaxFileLength = 0;

            long itemFileLength = 0;

            IdWorker snowId = new IdWorker(1, 1);

            List<string> fieldFile = new List<string>();

            #region 返回值
            EntityResult<FileUploadResult> result = new EntityResult<FileUploadResult>()
            {
                Status = 0,
                Msg = string.Empty,
                Result = new FileUploadResult()
                {
                    fieldFiles = new List<string>(),
                    successFiles = new List<string>()
                }
            };
            #endregion

            #endregion

            try
            {
                configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                DirectoryPath = m_webHostEnvironment.WebRootPath + @"\" + configuration["AppSetting:UploadFilePath"] + @"\";
                UploadFilePath = configuration["AppSetting:UploadFilePath"];
                fileCollection = formCollection.Files;
                fileExtension = configuration["AppSetting:Extension"];
                MaxFileLength = configuration["AppSetting:MaxFileLength"];
                if (!Directory.Exists(DirectoryPath))
                    Directory.CreateDirectory(DirectoryPath);
                if (string.IsNullOrEmpty(DirectoryPath))
                    message += "上传文件夹、";
                if (string.IsNullOrEmpty(fileExtension))
                    message += "文件扩展名、";
                if (!string.IsNullOrEmpty(message))
                {
                    result = new EntityResult<FileUploadResult>()
                    {
                        Status = -1,
                        Msg = $"配置文件[{message.Substring(0, message.Length - 1)}]没有配置请检查配置文件",
                        Result = null
                    };
                    return result;
                }
                if (fileCollection == null || fileCollection.Count <= 0)
                {
                    result = new EntityResult<FileUploadResult>()
                    {
                        Status = -1,
                        Msg = "没有文件不能上传",
                        Result = null
                    };
                    return result;
                }
                foreach (var file in fileCollection)
                {
                    string Extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                    List<string> fileExtensions = fileExtension!.ToLower().Split('|').ToList();
                    if (!fileExtensions.Contains(Extension))
                    {
                        fieldFile.Add($"文件名[{file.FileName}]是不允许上传的类型,只允许上传[{fileExtension.ToLower().Replace("|", ",")}]");
                        continue;
                    }
                    if (!string.IsNullOrEmpty(MaxFileLength) && Utils.StrToLong(MaxFileLength) > 0)
                    {
                        showMaxFileLength = Utils.StrToLong(MaxFileLength) / 1024 / 1024;
                        itemFileLength = file.Length / 1024 / 1024;
                        itemFileName = $"{snowId.NextId()}{Path.GetExtension(file.FileName)}";
                        itemFullFileName = $@"{DirectoryPath}\{itemFileName}";
                        if (showMaxFileLength < itemFileLength)
                        {
                            fieldFile.Add($"文件名[{file.FileName}]的大小超过限制,最大[{showMaxFileLength}]MB");
                            continue;
                        }
                    }
                    using (FileStream fileStream = System.IO.File.Create(itemFullFileName))
                    {
                        file.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    FileInfoParameter saveData = new FileInfoParameter()
                    {
                        FileId = Convert.ToString(snowId.NextId()),
                        FileName = itemFileName,
                        FileSize = Convert.ToString(file.Length),
                        FileUrl = $"{UploadFilePath}/{itemFileName}",
                        FileType = Extension.Replace(".", ""),
                        SrcFileName = file.FileName,
                        Created = "admin",
                        CreatedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    Result uploadResult = SaveData(saveData, snowId);
                    if (uploadResult.Status != 0)
                    {
                        fieldFile.Add($"文件名[{itemFileName}]上传失败,原因[{uploadResult.Msg}]");
                        result.Result.fieldFiles.Add(saveData.FileUrl);
                        continue;
                    }
                    result.Result.successFiles.Add(saveData.FileUrl);
                }
                if (fieldFile.Count > 0)
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                result = new EntityResult<FileUploadResult>()
                {
                    Status = -1,
                    Msg = ex.Message + ex.StackTrace,
                    Result = null
                };
                return result;
            }

            return result;
        }
        #endregion

        #region Delete

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="FileId">文件编号</param>
        /// <returns>返回结果</returns>
        [HttpDelete("{FileId}")]
        public ListResult<Result> deleteFile(string FileId)
        {
            #region 声明变量

            int resultStatus = 0;

            string where = string.Empty;

            string[] FileIDAry = null;

            //真实文件路径
            string realFilePath = string.Empty;

            //错误消息
            string message = string.Empty;

            List<Result> resultList = new List<Result>();

            List<RuralCultureDataAccess.Models.FileInfo> fileList = new List<RuralCultureDataAccess.Models.FileInfo>();

            //返回值
            ListResult<Result> result = new ListResult<Result>() { Status = resultStatus, Msg = string.Empty };
            #endregion

            if (string.IsNullOrEmpty(FileId))
            {
                result = new ListResult<Result>()
                {
                    Status = -1,
                    Msg = $"文件编号不能为空"
                };
                return result;
            }

            FileIDAry = string.IsNullOrEmpty(FileId) ? null : FileId.Split('-');
            if (FileIDAry == null || FileIDAry.Length == 0)
            {
                result = new ListResult<Result>()
                {
                    Status = -1,
                    Msg = "参数为空无法删除"
                };
                return result;
            }
            else
            {
                for (var i = 0; i < FileIDAry.Length; i++)
                {
                    if (!Utils.isLong(FileIDAry[i]))
                        message += $"第{i}条数据非数字、";
                }
                if (!string.IsNullOrEmpty(message))
                {
                    result = new ListResult<Result>()
                    {
                        Status = -1,
                        Msg = $"参数有非数字参数无法删除,原因[{message}]"
                    };
                    return result;
                }
            }

            where = $"'{string.Join("','", FileIDAry)}'";
            fileList = m_repository.QueryFileInfo($" FileId in ({where}) ", out message);
            if (fileList == null || fileList.Count <= 0) 
            {
                if (string.IsNullOrEmpty(message))
                    message = $"读取文件信息出错,原因[没有找到要读取的文件信息]";
                else
                    message = $"读取文件信息出错,原因[{message}]";
                result = new ListResult<Result>()
                {
                    Status = -1,
                    Msg = message
                };
                return result;
            }

            for(var i = 0; i < fileList.Count; i++)
            {
                Message resultStatusItem =null;
                var file = fileList[i];
                realFilePath = m_webHostEnvironment.WebRootPath +@"\"+file.FileUrl.Replace(@"/",@"\");
                try
                {
                    if (System.IO.File.Exists(realFilePath))
                        System.IO.File.Delete(realFilePath);
                    resultStatusItem = m_repository.DeleteFileInfo(file.FileId.GetValueOrDefault().ToString());
                    if (resultStatusItem != null && !resultStatusItem.Successful)
                    {
                        message = resultStatusItem.Content;
                        resultList.Add(
                            new Result()
                            {
                                Status = -1,
                                Msg = $"第{i}个文件,删除文件信息出错,原因[{message}]"
                            }
                        );
                    }
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    resultList.Add(
                        new Result()
                        {
                            Status = -1,
                            Msg = $"第{i}个文件,删除文件信息出错,原因[{message}]"
                        }
                    );
                }
            }

            if(resultList.Count>0)
            {
                resultStatus = -1;
                message = "文件删除有错误";
            }
            result = new ListResult<Result>()
            {
                Status = resultStatus,
                Msg = message,
                Result= resultList
            };
            return result;
        }
        #endregion

        #region Public
        /// <summary>
        /// 验证文件扩展名
        /// </summary>
        /// <param name="fileExtension">文件扩展名</param>
        /// <returns>验证结果</returns>
        public static bool checkUploadFileExtension(string fileExtension, out string message)
        {
            #region 声明和初始化

            //错误消息
            message = string.Empty;

            //返回值
            bool result = true;

            //配置文件扩展名配置
            string[] configFileExtension = null;

            //网站配置文件
            IConfiguration configuration;
            #endregion

            #region 读取网站配置文件
            try
            {
                configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .Build();
                if (string.IsNullOrEmpty(configuration["AppSetting:Extension"]))
                {
                    message = "没有配置上传文件限制";
                    return false;
                }
                configFileExtension = configuration["AppSetting:Extension"].Split('|');
            }
            catch (Exception ex)
            {
                message = $"读取网站配置文件出错，原因[{ex.Message}]";
                return false;
            }
            #endregion

            if (configFileExtension.ToList().Any(item => item.ToUpper() == fileExtension.ToUpper()) == false)
            {
                message = $"不是允许上传的文件类型，不能上传";
                return false;
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
        private Result SaveData(FileInfoParameter parameter, IdWorker? snowId) 
        {
            #region 声明和初始化

            //数据库返回值
            Message dbResult = new Message(true,string.Empty);

            //返回值
            Result result = new Result() { Status=0,Msg=string.Empty };

            //验证消息
            string checkMessage = string.Empty;

            //文章表
            RuralCultureDataAccess.Models.FileInfo saveData = new RuralCultureDataAccess.Models.FileInfo();
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
            if (string.IsNullOrEmpty(parameter.FileName))
                checkMessage += "文件名、";
            if (string.IsNullOrEmpty(parameter.SrcFileName))
                checkMessage += "原始文件名、";
            if (string.IsNullOrEmpty(parameter.FileUrl))
                checkMessage += "Url地址、";
            if (string.IsNullOrEmpty(parameter.FileType))
                checkMessage += "文件类型、";
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


            #region 保存数据
            saveData = m_mapper.Map<RuralCultureDataAccess.Models.FileInfo>(parameter);
            saveData.FileId = snowId.NextId();
            saveData.CreatedTime = DateTime.Now;
            dbResult = m_repository.InsertFileInfo(new List<RuralCultureDataAccess.Models.FileInfo>() { saveData });
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
                Status = 0,
                Msg = string.Empty
            };
            return result;
        }
        #endregion
    }
}
