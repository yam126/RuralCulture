// See https://aka.ms/new-console-template for more information
using RuralCultureWebApi.Controllers;
using RuralCultureWebApi.Models;
using RuralCultureWebApi;
using RuralCultureDataAccess;
using AutoMapper;
using TestProject.appsettings;
using Common;
using System.Text.Encodings;
using System.Text;
using Snowflake.Net;
using Microsoft.AspNetCore.Mvc;
using RuralCultureDataAccess.Models;
using TestProject;

/// <summary>
/// 测试文章Api
/// </summary>
static async Task InsertTestArticleWebApiAsync(IMapper mapper, IMOIRepository repository) 
{
    ArticleParameter parameter = null;
    ArticleController articleController = new ArticleController(repository,mapper,null);
    parameter = new ArticleParameter()
    {
        ArticleTitle = "测试添加的文章标题",
        ArticleType="0",
        Author="测试作者",
        Content="测试文章的内容",
        CoverUrl=string.Empty,
        Created="admin",
        CreatedTime=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
        Modified="admin",
        ModifiedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
    };
    ActionResult<Result> result=await articleController.AddData(parameter);
    if (result.Value.Status == 0)
        Console.WriteLine("执行成功");
    else
        Console.WriteLine($"执行失败,原因[{result.Value.Msg}]");
}

/// <summary>
/// 更新文章
/// </summary>
static async Task TestUpdateArticleWebApiAsync(IMapper mapper, IMOIRepository repository) 
{
    ArticleParameter parameter = null;
    ArticleController articleController = new ArticleController(repository, mapper, null);
    parameter = new ArticleParameter()
    {
        ArticleTitle = "测试添加的文章标题1",
        ArticleType = "0",
        Author = "测试作者1",
        Content = "测试文章的内容1",
        CoverUrl = string.Empty,
        Created = "admin",
        CreatedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
        Modified = "admin",
        ModifiedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
    };
    ActionResult<Result> result = await articleController.ModifyData("1608347394896957440", parameter);
    if (result.Value.Status == 0)
        Console.WriteLine("执行成功");
    else
        Console.WriteLine($"执行失败,原因[{result.Value.Msg}]");
}

/// <summary>
/// 查询数据
/// </summary>
static async void TestGetArticleById(IMapper mapper, IMOIRepository repository) 
{
    string ArticleId = "1608347394896957440";
    ActionResult<ListResult<ArticleParameter>> result = null;
    ArticleController articleController = new ArticleController(repository, mapper, null);
    result= await articleController.GetArticleById(ArticleId);
    if(result.Value.Status == 0)
    {
        List<ArticleParameter> resultList=result.Value.Result.ToList();
        if (resultList == null || resultList.Count == 0)
            Console.WriteLine("数据为空");
        else
        {
            foreach (var item in resultList) 
            {
                string jsonStr = JSONHelper.ObjectToJSONUnicode(item);
                Console.WriteLine(jsonStr);
            }
        }
    }
    else 
        Console.WriteLine(result.Value.Msg);
}

/// <summary>
/// 文章分页
/// </summary>
static async void TestGetArticlePage(IMapper mapper, IMOIRepository repository) 
{
    ActionResult<PageResult<ArticleParameter>> result = null;
    ArticleController articleController = new ArticleController(repository, mapper, null);
    result = articleController.GetPage("测试", 2, 10, "CreatedTime", "DESC");
    if (result.Value.Status == 0) 
    {
        List<ArticleParameter> resultList = result.Value.Result.ToList();
        if (resultList == null || resultList.Count == 0)
            Console.WriteLine("数据为空");
        else
        {
            foreach (var item in resultList)
            {
                string jsonStr = JSONHelper.ObjectToJSONUnicode(item);
                Console.WriteLine(jsonStr);
            }
        }
    }
    else
        Console.WriteLine(result.Value.Msg);
}

/// <summary>
/// 文件信息
/// </summary>
static async void TestInsertFileInfo(IMapper mapper, IMOIRepository repository) 
{
    FileInfoParameter fileInfoParameter = null;
    FileInfoController fileInfoController= new FileInfoController(repository, mapper, null);
    fileInfoParameter = new FileInfoParameter() 
    {
        FileName= "mapOfIndustry-2022-4-18.rar",
        FileSize= "5413788",
        FileType="rar",
        FileUrl= "mapOfIndustry-2022-4-18.rar",
        SrcFileName= "mapOfIndustry-2022-4-18.rar",
        Created="admin",
        CreatedTime=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
    };
    ActionResult<Result> result= fileInfoController.AddData(fileInfoParameter);
    if (result.Value.Status == 0)
        Console.WriteLine("执行成功");
    else
        Console.WriteLine($"执行失败,原因[{result.Value.Msg}]");
}

/// <summary>
/// 获得文件信息
/// </summary>
static void TestGetFileInfoById(IMapper mapper, IMOIRepository repository) 
{
    ActionResult<ListResult<FileInfoParameter>> result = null;
    FileInfoController fileInfoController = new FileInfoController(repository, mapper, null);
    string FileId = "1608387995424133120";
    result=fileInfoController.GetFileInfoById(FileId);
    if (result.Value.Status == 0) 
    {
        List<FileInfoParameter> resultList = result.Value.Result.ToList();
        if (resultList == null || resultList.Count == 0)
            Console.WriteLine("数据为空");
        else
        {
            foreach (var item in resultList)
            {
                string jsonStr = JSONHelper.ObjectToJSONUnicode(item);
                Console.WriteLine(jsonStr);
            }
        }
    }
}

/// <summary>
/// 获得文件信息
/// </summary>
static void TestGetFileInfoPage(IMapper mapper, IMOIRepository repository)
{
    //ActionResult<PageResult<FileInfoParameter>> result = null;
    //FileInfoController fileInfoController = new FileInfoController(repository, mapper, null);
    //result = fileInfoController.GetPage("", 1, 10, "CreatedTime", "DESC");
    //if (result.Value.Status == 0)
    //{
    //    List<FileInfoParameter> resultList = result.Value.Result.ToList();
    //    if (resultList == null || resultList.Count == 0)
    //        Console.WriteLine("数据为空");
    //    else
    //    {
    //        foreach (var item in resultList)
    //        {
    //            string jsonStr = JSONHelper.ObjectToJSONUnicode(item);
    //            Console.WriteLine(jsonStr);
    //        }
    //    }
    //}
}

/// <summary>
/// 更新文件信息
/// </summary>
static async void TestUpdateFileInfo(IMapper mapper, IMOIRepository repository)
{
    FileInfoParameter result = null;
    FileInfoController fileInfoController = new FileInfoController(repository, mapper, null);
    result = new FileInfoParameter() 
    {
        FileName= "mapOfIndustry-2022-4-19.rar",
        SrcFileName = "mapOfIndustry-2022-4-19.rar",
        FileSize= "5413788",
        FileType="rar",
        FileUrl= "mapOfIndustry-2022-4-19.rar",
        Created="admin",
        CreatedTime=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
    };
}
/// <summary>
/// 递归添加组织架构数据
/// </summary>
static void InsertRecurrenceTestOrganization(IMapper mapper, IMOIRepository repository)
{
    IdWorker snowId = new IdWorker(1, 1);
    int maxLevel = 3;
    List<OrganizationTestParameter> orgData = new List<OrganizationTestParameter>();
    var inserData = new List<Organization>();
    string message = string.Empty;
    for(int i = 0; i < 5; i++)
    {
        var orgList = new List<Organization>();
        OrganizationTestParameter test = new OrganizationTestParameter() {
            orgData = new Organization()
            {
                OrgId = snowId.NextId(),
                ParentId = 0,
                OrgName = $"测试组织架构{i}",
                Created = "admin",
                CreatedTime = DateTime.Now,
                Modified = "admin",
                ModifiedTime = DateTime.Now
            },
            orgList = new List<OrganizationTestParameter>()
        };
        InsertRecurrenceTestOrganizationFinal(
            repository,
            ref inserData,
            ref snowId,
            ref test,
            maxLevel
            );
        orgData.Add(test);
    }
    repository.InsertOrganization(inserData);
}

/// <summary>
/// 递归添加组织架构数据
/// </summary>
/// <param name="mapper">参数对应</param>
static void InsertRecurrenceTestOrganizationFinal(IMOIRepository repository,ref List<Organization> insertData,ref IdWorker snowId,ref OrganizationTestParameter data,int level) 
{
    int testDataCount = 10;
    level -= 1;
    if (level < 0)
        return;
    data.orgList = new List<OrganizationTestParameter>();
    for (int i = 0; i < testDataCount; i++) 
    {
        var childPareintId = snowId.NextId();
        Organization orgData = new Organization()
        {
            OrgId = childPareintId,
            ParentId = data.orgData.OrgId,
            OrgName = $"测试组织架构{level}-{i}",
            Created = "admin",
            CreatedTime = DateTime.Now,
            Modified = "admin",
            ModifiedTime = DateTime.Now
        };
        Console.WriteLine(orgData.OrgName);
        insertData.Add(orgData);
        OrganizationTestParameter child = new OrganizationTestParameter();
        child.orgData = orgData;
        data.orgList.Add(child);
        InsertRecurrenceTestOrganizationFinal(repository,ref insertData,ref snowId, ref child, level);
    }   
}

/// <summary>
/// 主要方法
/// </summary>
static void Main() 
{
    var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
    IMapper mapper = config.CreateMapper();
    string basePath = AppDomain.CurrentDomain.BaseDirectory;
    basePath += "appsettings.json";
    string jsonStr = Utils.getTxtFileBody(basePath, Encoding.Default);
    AppSettingRoot settingRoot=JSONHelper.JSONToObject<AppSettingRoot>(jsonStr);
    IMOIRepository repository = new MOIRepository(settingRoot.ConnectionStrings.MOIConnStr);
    InsertRecurrenceTestOrganization(mapper, repository);
    Console.ReadLine();
}

static void MarkSnowID() 
{
    IdWorker snowId = new IdWorker(1, 1);
    long testId = snowId.NextId();
    Console.WriteLine(testId);
}

Main();
