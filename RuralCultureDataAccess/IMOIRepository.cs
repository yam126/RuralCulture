using ePioneer.Data.Kernel;
using RuralCultureDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuralCultureDataAccess
{
    public interface IMOIRepository
    {
        #region Common

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="where">查询条件</param>
        /// <param name="oderBy">排序字符串</param>
        /// <param name="fields">字段列表</param>
        /// <returns>返回值</returns>
        public Task<PagerSet> GetPagerSetAsync(String tableName, Int32 pageIndex, Int32 pageSize, String where, String oderBy, String[] fields);

        /// <summary>
        /// 分页方法重载
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="where">查询条件</param>
        /// <param name="oderBy">排序字符串</param>
        /// <returns>返回值</returns>
        public Task<PagerSet> GetPagerSetAsync(String tableName, Int32 pageIndex, Int32 pageSize, String where, String orderBy);

        /// <summary>
        /// 存储过程分页
        /// </summary>
        /// <typeparam name="T">要分页的泛型类型</typeparam>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="SortMethod">排序方法</param>
        /// <param name="FieldStr">字段列表</param>
        /// <param name="TableName">表名</param>
        /// <param name="PageSize">每页数据量</param>
        /// <param name="CurPage">当前页</param>
        /// <param name="ReadDataRow">DataRow转泛型</param>
        /// <param name="TotalNumber">总数据量</param>
        /// <param name="PageCount">总页数</param>
        /// <param name="message">错误消息</param>
        /// <returns></returns>
        public List<T> QueryPage<T>(string SqlWhere, string SortField, string SortMethod, string FieldStr, string TableName, int PageSize, int CurPage, Func<DataRow, T, T> ReadDataRow, out int TotalNumber, out int PageCount, out string message);

        /// <summary>
        /// 异步查询方法(有返回结果)
        /// </summary>
        /// <param name="commandText">SQL字符串</param>
        /// <returns>查询结果</returns>
        public Task<DataSet> ExecuteDataSetAsync(String commandText);

        /// <summary>
        /// 查询方法(有返回结果)
        /// </summary>
        /// <param name="commandText">SQL字符串</param>
        /// <returns>查询结果</returns>
        public DataSet ExecuteDataSet(String commandText);


        /// <summary>
        /// 带参数查询方法(有返回结果)
        /// </summary>
        /// <param name="commandText">SQL字符串</param>
        /// <param name="commandType">SQL语句类型</param>
        /// <param name="sqlparms">参数集合</param>
        /// <param name="message">错误消息</param>
        /// <returns>查询结果</returns>
        public DataSet ExecuteDataSet(string commandText, CommandType commandType, DbParameter[] sqlparms, out string message);

        /// <summary>
        /// 异步查询方法(增删改语句)
        /// </summary>
        /// <param name="commandText">SQL字符串</param>
        /// <returns>查询结果</returns>
        public Task<Int32> ExecuteNonQueryAsync(String commandText);

        /// <summary>
        /// 查询方法(增删改语句)
        /// </summary>
        /// <param name="commandText">SQL字符串</param>
        /// <returns>查询结果</returns>
        public Int32 ExecuteNonQuery(String commandText);

        /// <summary>
        /// 查询方法(增删改语句)
        /// </summary>
        /// <param name="commandType">查询语句类型</param>
        /// <param name="commandText">查询语句</param>
        /// <param name="commandParameters">参数</param>
        /// <returns></returns>
        public Int32 ExecuteNonQuery(CommandType commandType, String commandText, params DbParameter[] commandParameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public List<string> GetAllTableName(string TableName, out string message);
        #endregion

        #region Public


        #region Article增删改查
        /// <summary>
        /// 添加文章表 
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="message">错误消息</param>
        /// <returns>添加条数</returns>
        public Message InsertArticle(List<Article> lists);

        /// <summary>
        /// 修改文章表
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="SqlWhere">更新条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>修改条数</returns>
        public Task<Message> UpdateArticle(List<Article> lists, string SqlWhere);

        /// <summary>
        /// 更新文章浏览数量
        /// </summary>
        /// <param name="ArticleId">文章编号</param>
        /// <returns>返回值</returns>
        public Message UpdateArticleViewCount(string ArticleId);

        /// <summary>
        /// 批量删除文章
        /// </summary>
        /// <param name="ArticleIds"></param>
        /// <returns></returns>
        public Message DeleteArticle(string IDStr);

        /// <summary>
        /// 查询文章表数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>返回值</returns>
        public List<Article> QueryArticle(string SqlWhere, out string message);

        /// <summary>
        /// 查询文章前几项
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="TopNumber"></param>
        /// <param name="OrderStr"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public List<vw_Article> QueryArticleTopData(out string message, string? SqlWhere = "", int? TopNumber = 1, string? OrderStr = " CreatedTime desc ");

        /// <summary>
        /// 查询Article数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>返回值</returns>
        public List<vw_Article> QueryArticleViewCountTopTen(string SqlWhere, out string message);

        /// <summary>
        /// 分页查询文章表数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="SortMethod">排序方法</param>
        /// <param name="PageSize">每页分页数据</param>
        /// <param name="CurPage">当前页</param>
        /// <param name="TotalNumber">总数据量</param>
        /// <param name="PageCount">总页数</param>
        /// <param name="message">错误消息</param>
        /// <returns></returns>
        public List<Article> QueryPageArticle(string SqlWhere, string SortField, string SortMethod, int PageSize, int CurPage, out int TotalNumber, out int PageCount, out string message);

        /// <summary>
        /// 查询Article数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>返回值</returns>
        public List<vw_Article> QueryVWArticle(string SqlWhere, out string message);

        /// <summary>
        /// 查询Article数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="OrderStr">排序字符串</param>
        /// <param name="TopNumber">TOP字符串</param>
        /// <param name="message">错误消息</param>
        /// <returns>返回值</returns>
        public List<vw_Article> QueryVWArticle(string SqlWhere, string OrderStr, string TopNumber, out string message);

        /// <summary>
        /// 分页查询vw_Article数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="SortMethod">排序方法</param>
        /// <param name="PageSize">每页分页数据</param>
        /// <param name="CurPage">当前页</param>
        /// <param name="TotalNumber">总数据量</param>
        /// <param name="PageCount">总页数</param>
        /// <param name="message">错误消息</param>
        /// <param name="FieldStr">字段列表</param>
        /// <returns>返回数据</returns>
        public List<vw_Article> QueryPageVWArticle(string SqlWhere, string SortField, string SortMethod, int PageSize, int CurPage, out int TotalNumber, out int PageCount, out string message, string FieldStr = "");

        /// <summary>
        /// 文章视图查询分页加强版
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="SortStr">排序字段</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="CurPage">当前页</param>
        /// <param name="TotalNumber">总数据量</param>
        /// <param name="PageCount">总页数</param>
        /// <param name="message">错误消息</param>
        /// <returns></returns>
        public List<vw_Article> QueryArticleAdvnancePage(string SqlWhere,string SortStr,int PageSize,int CurPage,out int TotalNumber,out int PageCount,out string message);
        #endregion


        #region FileInfo增删改查
        /// <summary>
        /// 添加文件表 
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="message">错误消息</param>
        /// <returns>添加条数</returns>
        public Message InsertFileInfo(List<Models.FileInfo> lists);

        /// <summary>
        /// 修改文件表
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="SqlWhere">更新条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>修改条数</returns>
        public Message UpdateFileInfo(List<Models.FileInfo> lists, string SqlWhere);

        /// <summary>
        /// 批量删除文件信息
        /// </summary>
        /// <param name="IDStr"></param>
        /// <returns></returns>
        public Message DeleteFileInfo(string IDStr);

        /// <summary>
        /// 查询文件表数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>返回值</returns>
        public List<Models.FileInfo> QueryFileInfo(string SqlWhere, out string message);

        /// <summary>
        /// 分页查询文件表数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="SortMethod">排序方法</param>
        /// <param name="PageSize">每页分页数据</param>
        /// <param name="CurPage">当前页</param>
        /// <param name="TotalNumber">总数据量</param>
        /// <param name="PageCount">总页数</param>
        /// <param name="message">错误消息</param>
        /// <returns></returns>
        public List<Models.FileInfo> QueryPageFileInfo(string SqlWhere, string SortField, string SortMethod, int PageSize, int CurPage, out int TotalNumber, out int PageCount, out string message,string FieldStr="");
        #endregion


        #region Group增删改查
        /// <summary>
        /// 添加文艺小组表 
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="message">错误消息</param>
        /// <returns>添加条数</returns>
        public Message InsertGroup(List<Group> lists);

        /// <summary>
        /// 批量删除文艺小组信息
        /// </summary>
        /// <param name="IDStr"></param>
        /// <returns></returns>
        public Message DeleteGroup(string IDStr);

        /// <summary>
        /// 修改文艺小组表
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="SqlWhere">更新条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>修改条数</returns>
        public Message UpdateGroup(List<Group> lists, string SqlWhere);

        /// <summary>
        /// 查询文章前几项
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="TopNumber"></param>
        /// <param name="OrderStr"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public List<Group> QueryGroupTopData(out string message, string? SqlWhere = "", int? TopNumber = 1, string? OrderStr = " CreatedTime desc ");

        /// <summary>
        /// 查询文艺小组表数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>返回值</returns>
        public List<Group> QueryGroup(string SqlWhere, out string message);

        /// <summary>
        /// 分页查询文艺小组表数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="SortMethod">排序方法</param>
        /// <param name="PageSize">每页分页数据</param>
        /// <param name="CurPage">当前页</param>
        /// <param name="TotalNumber">总数据量</param>
        /// <param name="PageCount">总页数</param>
        /// <param name="message">错误消息</param>
        /// <returns></returns>
        public List<Group> QueryPageGroup(string SqlWhere, string SortField, string SortMethod, int PageSize, int CurPage, out int TotalNumber, out int PageCount, out string message, string FieldStr = "");
        #endregion


        #region Organization增删改查

        /// <summary>
        /// 添加组织架构表 
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="message">错误消息</param>
        /// <returns>添加条数</returns>
        public Message InsertOrganization(List<Organization> lists);

        /// <summary>
        /// 批量删除组织架构
        /// </summary>
        /// <param name="orgIdList">组织架构编号</param>
        /// <returns>返回值</returns>
        public Message DeleteOrganizationByOrgId(List<string> orgIdList);

        /// <summary>
        /// 修改组织架构表
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="SqlWhere">更新条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>修改条数</returns>
        public Message UpdateOrganization(List<Organization> lists, string SqlWhere);

        /// <summary>
        /// 清空修改文章组织架构编号
        /// </summary>
        /// <param name="orgIdList">组织架构编号</param>
        /// <returns>返回值</returns>
        public Message UpdateCleanArticleByOrgId(List<string> orgIdList);

        /// <summary>
        /// 查询组织架构表数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>返回值</returns>
        public List<Organization> QueryOrganization(string SqlWhere, out string message);

        /// <summary>
        /// 判断组织架构是否有子数据
        /// </summary>
        /// <param name="OrgId">组织架构编号</param>
        /// <param name="message">错误消息</param>
        /// <returns>是否有子数据</returns>
        public bool OrganizationIsHaveChildren(string OrgId, out string message);

        /// <summary>
        /// 分页查询组织架构表数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="SortMethod">排序方法</param>
        /// <param name="PageSize">每页分页数据</param>
        /// <param name="CurPage">当前页</param>
        /// <param name="TotalNumber">总数据量</param>
        /// <param name="PageCount">总页数</param>
        /// <param name="message">错误消息</param>
        /// <returns></returns>
        public List<Organization> QueryPageOrganization(string SqlWhere, string SortField, string SortMethod, int PageSize, int CurPage, out int TotalNumber, out int PageCount, out string message, string FieldStr = "");
        #endregion


        #region PageTableColumn 增删改查
        /// <summary>
        /// 添加页面表格字段列表 
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="message">错误消息</param>
        /// <returns>添加条数</returns>
        public Task<Message> InsertPageTableColumn(List<PageTableColumn> lists);

        /// <summary>
        /// 修改页面表格字段列表
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="SqlWhere">更新条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>修改条数</returns>
        public Task<Message> UpdatePageTableColumn(List<PageTableColumn> lists, string SqlWhere);

        /// <summary>
        /// 查询页面表格字段列表数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>返回值</returns>
        public List<PageTableColumn> QueryPageTableColumn(string SqlWhere, out string message);

        /// <summary>
        /// 分页查询页面表格字段列表数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="SortMethod">排序方法</param>
        /// <param name="PageSize">每页分页数据</param>
        /// <param name="CurPage">当前页</param>
        /// <param name="TotalNumber">总数据量</param>
        /// <param name="PageCount">总页数</param>
        /// <param name="message">错误消息</param>
        /// <returns></returns>
        public List<PageTableColumn> QueryPagePageTableColumn(string SqlWhere, string SortField, string SortMethod, int PageSize, int CurPage, out int TotalNumber, out int PageCount, out string message);
        #endregion

        #endregion
    }
}
