using Common;
using ePioneer.Data.Kernel;
using Microsoft.Extensions.Configuration;
using RuralCultureDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuralCultureDataAccess
{
    public class MOIRepository : BaseDataProvider, IMOIRepository
    {
        #region Fields

        /// <summary>
        /// 连接字符串
        /// </summary>
        private static string? m_connectionString;

        /// <summary>
        /// 数据库帮助类
        /// </summary>
        private static DbHelper? m_dbHelper = null;
        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public MOIRepository() : base(MOIRepository.m_connectionString)
        {
            CreateDBHelper();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public MOIRepository(String connectionString) : base(connectionString)
        {
            if (Database != null)
                m_dbHelper = Database;
        }
        #endregion

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
        public Task<PagerSet> GetPagerSetAsync(String tableName, Int32 pageIndex, Int32 pageSize, String where, String oderBy, String[] fields)
        {
            return GetPagerSet2Async(new PagerParameters(tableName, "ORDER BY " + oderBy, "WHERE " + where, pageIndex, pageSize, fields)
            {
                CacherSize = 2
            });
        }

        /// <summary>
        /// 分页方法重载
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="where">查询条件</param>
        /// <param name="oderBy">排序字符串</param>
        /// <returns>返回值</returns>
        public Task<PagerSet> GetPagerSetAsync(String tableName, Int32 pageIndex, Int32 pageSize, String where, String orderBy)
        {
            return GetPagerSet2Async(new PagerParameters(tableName, "ORDER BY " + orderBy, "WHERE " + where, pageIndex, pageSize));
        }

        public List<T> QueryPage<T>(
            string SqlWhere,
            string SortField,
            string SortMethod,
            string FieldStr,
            string TableName,
            int PageSize,
            int CurPage,
            Func<DataRow, T, T> ReadDataRow,
            out int TotalNumber,
            out int PageCount,
            out string message)
        {
            message = string.Empty;
            PageCount = 0;
            TotalNumber = 0;
            DataSet ds = null;
            DataTable dt = null;
            List<T> result = new List<T>();
            DbParameter[] sqlparm = new DbParameter[] {
                m_dbHelper.MakeInParam("CurPage",CurPage),
                m_dbHelper.MakeInParam("PageSize",PageSize),
                m_dbHelper.MakeOutParam("TotalNumber",typeof(System.Int32)),
                m_dbHelper.MakeOutParam("PageCount",typeof(System.Int32)),
                m_dbHelper.MakeInParam("FieldStr",FieldStr),
                m_dbHelper.MakeInParam("TableName",TableName),
                m_dbHelper.MakeInParam("SortMethod",SortMethod),
                m_dbHelper.MakeInParam("SortField",SortField),
                m_dbHelper.MakeInParam("SqlWhere",SqlWhere)
            };
            try
            {
                ds = m_dbHelper.ExecuteDataSet(CommandType.StoredProcedure, "QueryPage", sqlparm);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            #region 非空检查
            if (ds == null)
                return result;
            if (ds.Tables == null || ds.Tables.Count == 0)
                return result;
            dt = ds.Tables[0];
            if (dt == null)
                return result;
            if (dt.Rows.Count == 0)
                return result;
            #endregion
            TotalNumber = Convert.ToInt32(sqlparm[2].Value);
            foreach (DataRow dr in dt.Rows)
            {
                T t = default(T);
                result.Add(ReadDataRow(dr, t));
            }
            return result;
        }

        /// <summary>
        /// 异步查询方法(有返回结果)
        /// </summary>
        /// <param name="commandText">SQL字符串</param>
        /// <returns>查询结果</returns>
        public Task<DataSet> ExecuteDataSetAsync(String commandText)
        {
            return m_dbHelper.ExecuteDataSetAsync(CommandType.Text, commandText);
        }

        /// <summary>
        /// 查询方法(有返回结果)
        /// </summary>
        /// <param name="commandText">SQL字符串</param>
        /// <returns>查询结果</returns>
        public DataSet ExecuteDataSet(String commandText)
        {
            return m_dbHelper.ExecuteDataSet(CommandType.Text, commandText);
        }

        /// <summary>
        /// 带参数查询方法(有返回结果)
        /// </summary>
        /// <param name="commandText">SQL字符串</param>
        /// <param name="commandType">SQL语句类型</param>
        /// <param name="sqlparms">参数集合</param>
        /// <param name="message">错误消息</param>
        /// <returns>查询结果</returns>
        public DataSet ExecuteDataSet(string commandText, CommandType commandType, DbParameter[] sqlparms, out string message)
        {
            DataSet result = null;
            message = string.Empty;
            try
            {
                result = m_dbHelper.ExecuteDataSet(commandType, commandText, sqlparms);
            }
            catch (Exception exp)
            {
                message = exp.Message;
            }
            return result;
        }

        /// <summary>
        /// 异步查询方法(增删改语句)
        /// </summary>
        /// <param name="commandText">SQL字符串</param>
        /// <returns>查询结果</returns>
        public Task<Int32> ExecuteNonQueryAsync(String commandText)
        {
            return m_dbHelper.ExecuteNonQueryAsync(commandText);
        }

        /// <summary>
        /// 查询方法(增删改查语句)
        /// </summary>
        /// <param name="commandText">SQL字符串</param>
        /// <returns>查询结果</returns>
        public Int32 ExecuteNonQuery(String commandText)
        {
            return m_dbHelper.ExecuteNonQuery(commandText);
        }

        /// <summary>
        /// 查询方法(增删改语句)
        /// </summary>
        /// <param name="commandType">查询语句类型</param>
        /// <param name="commandText">查询语句</param>
        /// <param name="commandParameters">参数</param>
        public Int32 ExecuteNonQuery(CommandType commandType, String commandText, params DbParameter[] commandParameters)
        {
            return m_dbHelper.ExecuteNonQuery(commandType, commandText, commandParameters);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public List<string> GetAllTableName(string TableName,out string message) 
        {
            message=string.Empty;
            List<string> result= new List<string>();
            string SqlWhere=string.IsNullOrEmpty(TableName)?string.Empty:$" and [name] in ({TableName}) ";
            string sql = $"SELECT [name] [tableName] FROM sysobjects WHERE xtype = 'u' {SqlWhere} ORDER BY [name]";
            DataSet ds = new DataSet();
            DataTable dt = null;
            try
            {
                ds = m_dbHelper.ExecuteDataSet(sql);
                #region 非空检查
                if (ds == null)
                    return result;
                if (ds.Tables == null || ds.Tables.Count == 0)
                    return result;
                dt = ds.Tables[0];
                if (dt == null)
                    return result;
                if (dt.Rows.Count == 0)
                    return result;
                #endregion
                foreach(DataRow dr in dt.Rows) 
                    result.Add(Convert.ToString(dr[0]));
            }
            catch (Exception exp) 
            {
                message=exp.Message;
            }
            return result;
        }
        #endregion

        #region Public

        #region Article增删改查
        /// <summary>
        /// 添加文章表 
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="message">错误消息</param>
        /// <returns>添加条数</returns>
        public Message InsertArticle(List<Article> lists)
        {
            int DbState = -1;
            Message result = null;
            string message = string.Empty;
            if (lists == null)
                return new Message(false, "参数为空不能添加");
            CheckEmpty(ref lists);
            CheckMaxLength(ref lists, out message);
            if (!string.IsNullOrEmpty(message))
                return new Message(true, message);
            List<string> sqls = this.MarkInsertSql(lists);
            try
            {
                DbState = ExecuteNonQuery(string.Join(';', sqls.ToArray()));
            }
            catch (Exception ex)
            {
                result = new Message(false, ex.Message);
            }
            if (result == null)
            {
                result = new Message(true, string.Empty);
            }
            return result;
        }

        /// <summary>
        /// 批量删除文章
        /// </summary>
        /// <param name="ArticleIds"></param>
        /// <returns></returns>
        public Message DeleteArticle(string IDStr) 
        {
            #region 声明变量
            int DbState = -1;
            Message result = new Message(true, string.Empty);
            string where = string.Empty;
            string sql = string.Empty;
            string message = string.Empty;
            string[] articleIdAry = null;
            #endregion

            articleIdAry = string.IsNullOrEmpty(IDStr) ? null : IDStr.Split('-');
            #region 参数验证
            if (articleIdAry == null || articleIdAry.Length == 0)
            {
                result = new Message()
                {
                    Successful=false,
                    Content= "参数为空无法删除"
                };
                return result;
            }
            else
            {
                for (var i = 0; i < articleIdAry.Length; i++)
                {
                    if (!Utils.isLong(articleIdAry[i]))
                        message += $"第{i}条数据非数字、";
                }
                if (!string.IsNullOrEmpty(message))
                {
                    result = new Message()
                    {
                        Successful = false,
                        Content = $"参数有非数字参数无法删除,原因[{message}]"
                    };
                    return result;
                }
            }
            #endregion

            where= $"'{string.Join("','", articleIdAry)}'";
            sql = $" delete from Article where ArticleId in ({where}) ";
            try
            {
                DbState = ExecuteNonQuery(CommandType.Text, sql);
            }
            catch (Exception ex)
            {
                result = new Message(false, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// 修改文章表 
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="SqlWhere">更新条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>修改条数</returns>
        public async Task<Message> UpdateArticle(List<Article> lists, string SqlWhere)
        {
            Message result = null;
            int DbState = -1;
            string message = string.Empty;
            CheckEmpty(ref lists);
            List<string> sqls = this.MarkUpdateSql(lists, SqlWhere);
            try
            {
                DbState = ExecuteNonQuery(CommandType.Text, string.Join(';', sqls.ToArray()));
            }
            catch (Exception ex)
            {
                message = ex.Message;
                result = new Message(false, ex.Message);
            }
            if (result == null)
            {
                result = new Message(true, string.Empty);
            }
            return result;
        }

        /// <summary>
        /// 清空修改文章组织架构编号
        /// </summary>
        /// <param name="orgIdList">组织架构编号</param>
        /// <returns>返回值</returns>
        public Message UpdateCleanArticleByOrgId(List<string> orgIdList) 
        {
            Message result = null;
            int DbState = -1;
            string sqlwhere = $"'{string.Join("','",orgIdList)}'";
            string sql = $"update Article set OrgId='0' where OrgId in ({sqlwhere})";
            try
            {
                DbState = ExecuteNonQuery(CommandType.Text, sql);
            }
            catch (Exception ex)
            {
                result = new Message(false, ex.Message);
            }
            if (result == null)
            {
                result = new Message(true, string.Empty);
            }
            return result;
        }

        /// <summary>
        /// 更新文章浏览数量
        /// </summary>
        /// <param name="ArticleId">文章编号</param>
        /// <returns>返回值</returns>
        public Message UpdateArticleViewCount(string ArticleId) 
        {
            Message result = null;
            int DbState = -1;
            string sql = $" update Article set ViewCount=ViewCount+1 where ArticleId='{ArticleId}' ";
            try
            {
                DbState = ExecuteNonQuery(CommandType.Text, sql);
            }
            catch (Exception ex)
            {
                result = new Message(false, ex.Message);
            }
            if (result == null)
            {
                result = new Message(true, string.Empty);
            }
            return result;
        }

        /// <summary>
        /// 查询Article数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>返回值</returns>
        public List<Article> QueryArticle(string SqlWhere, out string message)
        {
            List<Article> result = new List<Article>();
            string sql = $"select {FieldArticle()} from Article";
            DataSet ds = new DataSet();
            DataTable dt = null;
            message = string.Empty;
            if (!string.IsNullOrEmpty(SqlWhere))
                sql += $" where {SqlWhere} ";
            try
            {
                ds = m_dbHelper.ExecuteDataSet(sql);
                #region 非空检查
                if (ds == null)
                    return result;
                if (ds.Tables == null || ds.Tables.Count == 0)
                    return result;
                dt = ds.Tables[0];
                if (dt == null)
                    return result;
                if (dt.Rows.Count == 0)
                    return result;
                #endregion
                foreach (DataRow dtRow in dt.Rows)
                    result.Add(ReadDataRow(dtRow, new Article()));
            }
            catch (Exception exp)
            {
                message = exp.Message;
            }
            return result;
        }

        /// <summary>
        /// 查询文章前几项
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="TopNumber"></param>
        /// <param name="OrderStr"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public List<vw_Article> QueryArticleTopData(out string message,string? SqlWhere="", int? TopNumber=1, string? OrderStr= " CreatedTime desc ") 
        {
            List<vw_Article> result = new List<vw_Article>();
            DataSet ds = new DataSet();
            DataTable dt = null;
            message = string.Empty;
            string sql = $" select top {TopNumber} {FieldVWArticle()} from vw_Article ";
            if (!string.IsNullOrEmpty(SqlWhere))
                sql += $" where {SqlWhere} ";
            sql += $" order by {OrderStr} ";
            try
            {
                ds = m_dbHelper.ExecuteDataSet(sql);
                #region 非空检查
                if (ds == null)
                    return result;
                if (ds.Tables == null || ds.Tables.Count == 0)
                    return result;
                dt = ds.Tables[0];
                if (dt == null)
                    return result;
                if (dt.Rows.Count == 0)
                    return result;
                #endregion
                foreach (DataRow dtRow in dt.Rows)
                    result.Add(ReadDataRow(dtRow, new vw_Article()));
            }
            catch (Exception exp)
            {
                message = exp.Message;
            }
            return result;
        }

        /// <summary>
        /// 查询Article数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>返回值</returns>
        public List<vw_Article> QueryArticleViewCountTopTen(string SqlWhere, out string message)
        {
            List<vw_Article> result = new List<vw_Article>();
            string sql = $" select top 10 {FieldVWArticle()} from vw_Article ";
            DataSet ds = new DataSet();
            DataTable dt = null;
            message = string.Empty;
            if (!string.IsNullOrEmpty(SqlWhere))
                sql += $" where {SqlWhere} ";
            sql += " order by ViewCount desc ";
            try
            {
                ds = m_dbHelper.ExecuteDataSet(sql);
                #region 非空检查
                if (ds == null)
                    return result;
                if (ds.Tables == null || ds.Tables.Count == 0)
                    return result;
                dt = ds.Tables[0];
                if (dt == null)
                    return result;
                if (dt.Rows.Count == 0)
                    return result;
                #endregion
                foreach (DataRow dtRow in dt.Rows)
                    result.Add(ReadDataRow(dtRow, new vw_Article()));
            }
            catch (Exception exp)
            {
                message = exp.Message;
            }
            return result;
        }

        /// <summary>
        /// 分页查询Article数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="SortMethod">排序方法</param>
        /// <param name="PageSize">每页分页数据</param>
        /// <param name="CurPage">当前页</param>
        /// <param name="TotalNumber">总数据量</param>
        /// <param name="PageCount">总页数</param>
        /// <param name="message">错误消息</param>
        /// <returns>返回数据</returns>
        public List<Article> QueryPageArticle(
            string SqlWhere,
            string SortField,
            string SortMethod,
            int PageSize,
            int CurPage,
            out int TotalNumber,
            out int PageCount,
            out string message
            )
        {
            List<Article> result = null;
            string FieldStr = FieldArticle();
            Func<DataRow, Article, Article> func = (DataRow dr, Article model) => {
                return ReadDataRow(dr, model);
            };
            result = QueryPage<Article>(
                SqlWhere,
                SortField,
                SortMethod,
                FieldStr,
                "Article",
                PageSize,
                CurPage,
                func,
                out TotalNumber,
                out PageCount,
                out message
                );
            return result;
        }
        #endregion

        #region vw_Article 增删改查

        /// <summary>
        /// 查询Article数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>返回值</returns>
        public List<vw_Article> QueryVWArticle(string SqlWhere, out string message) 
        {
            return QueryVWArticle(SqlWhere, string.Empty,string.Empty,out message);
        }

        /// <summary>
        /// 查询Article数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="OrderStr">排序字符串</param>
        /// <param name="TopNumber">TOP字符串</param>
        /// <param name="message">错误消息</param>
        /// <returns>返回值</returns>
        public List<vw_Article> QueryVWArticle(string SqlWhere,string OrderStr,string TopNumber,out string message)
        {
            List<vw_Article> result = new List<vw_Article>();
            string sql = $"select {TopNumber} {FieldVWArticle()} from vw_Article";
            DataSet ds = new DataSet();
            DataTable dt = null;
            message = string.Empty;
            if (!string.IsNullOrEmpty(SqlWhere))
                sql += $" where {SqlWhere} ";
            if (!string.IsNullOrEmpty(OrderStr))
                sql += $" order by {OrderStr} ";
            try
            {
                ds = m_dbHelper.ExecuteDataSet(sql);
                #region 非空检查
                if (ds == null)
                    return result;
                if (ds.Tables == null || ds.Tables.Count == 0)
                    return result;
                dt = ds.Tables[0];
                if (dt == null)
                    return result;
                if (dt.Rows.Count == 0)
                    return result;
                #endregion
                foreach (DataRow dtRow in dt.Rows)
                    result.Add(ReadDataRow(dtRow, new vw_Article()));
            }
            catch (Exception exp)
            {
                message = exp.Message;
            }
            return result;
        }



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
        /// <returns>返回数据</returns>
        public List<vw_Article> QueryPageVWArticle(
            string SqlWhere,
            string SortField,
            string SortMethod,
            int PageSize,
            int CurPage,        
            out int TotalNumber,
            out int PageCount,
            out string message,
            string FieldStr = ""
            )
        {
            List<vw_Article> result = null;
            if(string.IsNullOrEmpty(FieldStr))
                FieldStr = FieldVWArticle();
            Func<DataRow, vw_Article, vw_Article> func = (DataRow dr, vw_Article model) => {
                return ReadDataRow(dr, model);
            };
            result = QueryPage<vw_Article>(
                SqlWhere,
                SortField,
                SortMethod,
                FieldStr,
                "vw_Article",
                PageSize,
                CurPage,
                func,
                out TotalNumber,
                out PageCount,
                out message
                );
            return result;
        }

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
        public List<vw_Article> QueryArticleAdvnancePage(
            string SqlWhere,
            string SortStr,
            int PageSize,
            int CurPage,
            out int TotalNumber,
            out int PageCount,
            out string message)
        {
            message = string.Empty;
            PageCount = 0;
            TotalNumber = 0;
            DataSet ds = null;
            DataTable dt = null;
            List<vw_Article> result = new List<vw_Article>();
            DbParameter[] sqlparm = new DbParameter[] {
                m_dbHelper.MakeInParam("StartRow",((CurPage - 1) * PageSize + 1)),
                m_dbHelper.MakeInParam("EndRow",(CurPage * PageSize)),
                m_dbHelper.MakeOutParam("TotalNumber",typeof(System.Int32)),
                m_dbHelper.MakeInParam("SortStr",SortStr),
                m_dbHelper.MakeInParam("SqlWhere",SqlWhere)
            };
            try
            {
                ds = m_dbHelper.ExecuteDataSet(CommandType.StoredProcedure, "QueryArticleAdvnancePage", sqlparm);
                TotalNumber = Convert.ToInt32(sqlparm.Where(q => q.ParameterName == "@TotalNumber").First().Value);
                if ((TotalNumber % PageSize) == 0)
                    PageCount = Convert.ToInt32(TotalNumber % PageSize);
                else
                    PageCount = Convert.ToInt32(TotalNumber % PageSize)+1;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            #region 非空检查
            if (ds == null)
                return result;
            if (ds.Tables == null || ds.Tables.Count == 0)
                return result;
            dt = ds.Tables[0];
            if (dt == null)
                return result;
            if (dt.Rows.Count == 0)
                return result;
            #endregion
            TotalNumber = Convert.ToInt32(sqlparm[2].Value);
            foreach (DataRow dr in dt.Rows)
            {
                vw_Article model = new vw_Article();
                result.Add(ReadDataRow(dr, model));
            }
            return result;
        }


        #endregion

        #region FileInfo增删改查
        /// <summary>
        /// 添加文件表 
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="message">错误消息</param>
        /// <returns>添加条数</returns>
        public Message InsertFileInfo(List<Models.FileInfo> lists)
        {
            int DbState = -1;
            Message result = new Message(true, string.Empty);
            string message = string.Empty;
            if (lists == null)
                return new Message(false, "参数为空不能添加");
            CheckEmpty(ref lists);
            CheckMaxLength(ref lists, out message);
            if (!string.IsNullOrEmpty(message))
                return new Message(false, message);
            List<string> sqls = this.MarkInsertSql(lists);
            try
            {
                DbState = ExecuteNonQuery(string.Join(';', sqls.ToArray()));
            }
            catch (Exception ex)
            {
                result = new Message(false, ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 修改文件表 
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="SqlWhere">更新条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>修改条数</returns>
        public Message UpdateFileInfo(List<Models.FileInfo> lists, string SqlWhere)
        {
            Message result = new Message(true, string.Empty);
            int DbState = -1;
            string message = string.Empty;
            CheckEmpty(ref lists);
            List<string> sqls = this.MarkUpdateSql(lists, SqlWhere);
            try
            {
                DbState = ExecuteNonQuery(CommandType.Text, string.Join(';', sqls.ToArray()));
            }
            catch (Exception ex)
            {
                message = ex.Message;
                result = new Message(false, ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 批量删除文件信息
        /// </summary>
        /// <param name="IDStr"></param>
        /// <returns></returns>
        public Message DeleteFileInfo(string IDStr)
        {
            #region 声明变量
            int DbState = -1;
            Message result = new Message(true, string.Empty);
            string where = string.Empty;
            string sql = string.Empty;
            string message = string.Empty;
            string[] IdAry = null;
            #endregion

            IdAry = string.IsNullOrEmpty(IDStr) ? null : IDStr.Split('-');
            #region 参数验证
            if (IdAry == null || IdAry.Length == 0)
            {
                result = new Message()
                {
                    Successful = false,
                    Content = "参数为空无法删除"
                };
                return result;
            }
            else
            {
                for (var i = 0; i < IdAry.Length; i++)
                {
                    if (!Utils.isLong(IdAry[i]))
                        message += $"第{i}条数据非数字、";
                }
                if (!string.IsNullOrEmpty(message))
                {
                    result = new Message()
                    {
                        Successful = false,
                        Content = $"参数有非数字参数无法删除,原因[{message}]"
                    };
                    return result;
                }
            }
            #endregion

            where = $"'{string.Join("','", IdAry)}'";
            sql = $" delete from [FileInfo] where [FileId] in ({where}) ";
            try
            {
                DbState = ExecuteNonQuery(CommandType.Text, sql);
            }
            catch (Exception ex)
            {
                result = new Message(false, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// 查询FileInfo数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>返回值</returns>
        public List<Models.FileInfo> QueryFileInfo(string SqlWhere, out string message)
        {
            List<Models.FileInfo> result = new List<Models.FileInfo>();
            string sql = $"select {FieldFileInfo()} from FileInfo";
            DataSet ds = new DataSet();
            DataTable dt = null;
            message = string.Empty;
            if (!string.IsNullOrEmpty(SqlWhere))
                sql += $" where {SqlWhere} ";
            try
            {
                ds = m_dbHelper.ExecuteDataSet(sql);
                #region 非空检查
                if (ds == null)
                    return result;
                if (ds.Tables == null || ds.Tables.Count == 0)
                    return result;
                dt = ds.Tables[0];
                if (dt == null)
                    return result;
                if (dt.Rows.Count == 0)
                    return result;
                #endregion
                foreach (DataRow dtRow in dt.Rows)
                    result.Add(ReadDataRow(dtRow, new Models.FileInfo()));
            }
            catch (Exception exp)
            {
                message = exp.Message;
            }
            return result;
        }

        /// <summary>
        /// 分页查询FileInfo数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="SortMethod">排序方法</param>
        /// <param name="PageSize">每页分页数据</param>
        /// <param name="CurPage">当前页</param>
        /// <param name="TotalNumber">总数据量</param>
        /// <param name="PageCount">总页数</param>
        /// <param name="message">错误消息</param>
        /// <returns>返回数据</returns>
        public List<Models.FileInfo> QueryPageFileInfo(
            string SqlWhere,
            string SortField,
            string SortMethod,
            int PageSize,
            int CurPage,
            out int TotalNumber,
            out int PageCount,
            out string message,
            string FieldStr = ""
            )
        {
            List<Models.FileInfo> result = null;
            if(string.IsNullOrEmpty(FieldStr))
                FieldStr = FieldFileInfo();
            Func<DataRow, Models.FileInfo, Models.FileInfo> func = (DataRow dr, Models.FileInfo model) => {
                return ReadDataRow(dr, model);
            };
            result = QueryPage<Models.FileInfo>(
                SqlWhere,
                SortField,
                SortMethod,
                FieldStr,
                "FileInfo",
                PageSize,
                CurPage,
                func,
                out TotalNumber,
                out PageCount,
                out message
                );
            return result;
        }
        #endregion

        #region Group增删改查
        /// <summary>
        /// 添加文艺小组表 
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="message">错误消息</param>
        /// <returns>添加条数</returns>
        public Message InsertGroup(List<Group> lists)
        {
            int DbState = -1;
            Message result = new Message(true, string.Empty);
            string message = string.Empty;
            if (lists == null)
                return new Message(false, "参数为空不能添加");
            CheckEmpty(ref lists);
            CheckMaxLength(ref lists, out message);
            if (!string.IsNullOrEmpty(message))
                return new Message(false, message);
            List<string> sqls = this.MarkInsertSql(lists);
            try
            {
                DbState = ExecuteNonQuery(string.Join(';', sqls.ToArray()));
            }
            catch (Exception ex)
            {
                result = new Message(false, ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 批量删除文艺小组信息
        /// </summary>
        /// <param name="IDStr"></param>
        /// <returns></returns>
        public Message DeleteGroup(string IDStr)
        {
            #region 声明变量
            int DbState = -1;
            Message result = new Message(true, string.Empty);
            string where = string.Empty;
            string sql = string.Empty;
            string message = string.Empty;
            string[] IdAry = null;
            #endregion

            IdAry = string.IsNullOrEmpty(IDStr) ? null : IDStr.Split('-');
            #region 参数验证
            if (IdAry == null || IdAry.Length == 0)
            {
                result = new Message()
                {
                    Successful = false,
                    Content = "参数为空无法删除"
                };
                return result;
            }
            else
            {
                for (var i = 0; i < IdAry.Length; i++)
                {
                    if (!Utils.isLong(IdAry[i]))
                        message += $"第{i}条数据非数字、";
                }
                if (!string.IsNullOrEmpty(message))
                {
                    result = new Message()
                    {
                        Successful = false,
                        Content = $"参数有非数字参数无法删除,原因[{message}]"
                    };
                    return result;
                }
            }
            #endregion

            where = $"'{string.Join("','", IdAry)}'";
            sql = $" delete from [Group] where [GroupId] in ({where}) ";
            try
            {
                DbState = ExecuteNonQuery(CommandType.Text, sql);
            }
            catch (Exception ex)
            {
                result = new Message(false, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// 修改文艺小组表 
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="SqlWhere">更新条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>修改条数</returns>
        public Message UpdateGroup(List<Group> lists, string SqlWhere)
        {
            Message result = new Message(true, string.Empty);
            int DbState = -1;
            string message = string.Empty;
            CheckEmpty(ref lists);
            List<string> sqls = this.MarkUpdateSql(lists, SqlWhere);
            try
            {
                DbState = ExecuteNonQuery(CommandType.Text, string.Join(';', sqls.ToArray()));
            }
            catch (Exception ex)
            {
                message = ex.Message;
                result = new Message(false, ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 查询Group数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>返回值</returns>
        public List<Group> QueryGroup(string SqlWhere, out string message)
        {
            List<Group> result = new List<Group>();
            string sql = $"select {FieldGroup()} from [Group]";
            DataSet ds = new DataSet();
            DataTable dt = null;
            message = string.Empty;
            if (!string.IsNullOrEmpty(SqlWhere))
                sql += $" where {SqlWhere} ";
            try
            {
                ds = m_dbHelper.ExecuteDataSet(sql);
                #region 非空检查
                if (ds == null)
                    return result;
                if (ds.Tables == null || ds.Tables.Count == 0)
                    return result;
                dt = ds.Tables[0];
                if (dt == null)
                    return result;
                if (dt.Rows.Count == 0)
                    return result;
                #endregion
                foreach (DataRow dtRow in dt.Rows)
                    result.Add(ReadDataRow(dtRow, new Group()));
            }
            catch (Exception exp)
            {
                message = exp.Message;
            }
            return result;
        }

        /// <summary>
        /// 查询文章前几项
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="TopNumber"></param>
        /// <param name="OrderStr"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public List<Group> QueryGroupTopData(out string message, string? SqlWhere = "", int? TopNumber = 1, string? OrderStr = " CreatedTime desc ")
        {
            List<Group> result = new List<Group>();
            DataSet ds = new DataSet();
            DataTable dt = null;
            message = string.Empty;
            string sql = $" select top {TopNumber} {FieldGroup()} from [Group] ";
            if (!string.IsNullOrEmpty(SqlWhere))
                sql += $" where {SqlWhere} ";
            sql += $" order by {OrderStr} ";
            try
            {
                ds = m_dbHelper.ExecuteDataSet(sql);
                #region 非空检查
                if (ds == null)
                    return result;
                if (ds.Tables == null || ds.Tables.Count == 0)
                    return result;
                dt = ds.Tables[0];
                if (dt == null)
                    return result;
                if (dt.Rows.Count == 0)
                    return result;
                #endregion
                foreach (DataRow dtRow in dt.Rows)
                    result.Add(ReadDataRow(dtRow, new Group()));
            }
            catch (Exception exp)
            {
                message = exp.Message;
            }
            return result;
        }

        /// <summary>
        /// 分页查询Group数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="SortMethod">排序方法</param>
        /// <param name="PageSize">每页分页数据</param>
        /// <param name="CurPage">当前页</param>
        /// <param name="TotalNumber">总数据量</param>
        /// <param name="PageCount">总页数</param>
        /// <param name="message">错误消息</param>
        /// <returns>返回数据</returns>
        public List<Group> QueryPageGroup(
            string SqlWhere,
            string SortField,
            string SortMethod,
            int PageSize,
            int CurPage,
            out int TotalNumber,
            out int PageCount,
            out string message,
            string FieldStr = ""
            )
        {
            List<Group> result = null;
            if(!string.IsNullOrEmpty(FieldStr))
                FieldStr = FieldGroup();
            Func<DataRow, Group, Group> func = (DataRow dr, Group model) => {
                return ReadDataRow(dr, model);
            };
            result = QueryPage<Group>(
                SqlWhere,
                SortField,
                SortMethod,
                FieldStr,
                "[Group]",
                PageSize,
                CurPage,
                func,
                out TotalNumber,
                out PageCount,
                out message
                );
            return result;
        }
        #endregion

        #region Organization增删改查

        /// <summary>
        /// 添加组织架构表 
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="message">错误消息</param>
        /// <returns>添加条数</returns>
        public Message InsertOrganization(List<Organization> lists)
        {
            int DbState = -1;
            Message result = new Message(true, string.Empty);
            string message = string.Empty;
            if (lists == null)
                return new Message(false, "参数为空不能添加");
            CheckEmpty(ref lists);
            CheckMaxLength(ref lists, out message);
            if (!string.IsNullOrEmpty(message))
                return new Message(false, message);
            List<string> sqls = this.MarkInsertSql(lists);
            try
            {
                DbState = ExecuteNonQuery(string.Join(';', sqls.ToArray()));
            }
            catch (Exception ex)
            {
                result = new Message(false, ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 修改组织架构表 
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="SqlWhere">更新条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>修改条数</returns>
        public Message UpdateOrganization(List<Organization> lists, string SqlWhere)
        {
            Message result = new Message(true,string.Empty);
            int DbState = -1;
            string message = string.Empty;
            CheckEmpty(ref lists);
            List<string> sqls = this.MarkUpdateSql(lists, SqlWhere);
            try
            {
                DbState = ExecuteNonQuery(CommandType.Text, string.Join(';', sqls.ToArray()));
            }
            catch (Exception ex)
            {
                message = ex.Message;
                result = new Message(false, ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 批量删除组织架构
        /// </summary>
        /// <param name="orgIdList">组织架构编号</param>
        /// <returns>返回值</returns>
        public Message DeleteOrganizationByOrgId(List<string> orgIdList)
        {
            Message result = null;
            int DbState = -1;
            string sqlwhere = $"'{string.Join("','", orgIdList)}'";
            string sql = $"delete from Organization where OrgId in ({sqlwhere})";
            try
            {
                DbState = ExecuteNonQuery(CommandType.Text, sql);
            }
            catch (Exception ex)
            {
                result = new Message(false, ex.Message);
            }
            if (result == null)
            {
                result = new Message(true, string.Empty);
            }
            return result;
        }

        /// <summary>
        /// 查询Organization数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>返回值</returns>
        public List<Organization> QueryOrganization(string SqlWhere, out string message)
        {
            List<Organization> result = new List<Organization>();
            string sql = $"select {FieldOrganization()} from Organization";
            DataSet ds = new DataSet();
            DataTable dt = null;
            message = string.Empty;
            if (!string.IsNullOrEmpty(SqlWhere))
                sql += $" where {SqlWhere} ";
            try
            {
                ds = m_dbHelper.ExecuteDataSet(sql);
                #region 非空检查
                if (ds == null)
                    return result;
                if (ds.Tables == null || ds.Tables.Count == 0)
                    return result;
                dt = ds.Tables[0];
                if (dt == null)
                    return result;
                if (dt.Rows.Count == 0)
                    return result;
                #endregion
                foreach (DataRow dtRow in dt.Rows)
                    result.Add(ReadDataRow(dtRow, new Organization()));
            }
            catch (Exception exp)
            {
                message = exp.Message;
            }
            return result;
        }

        /// <summary>
        /// 判断组织架构是否有子数据
        /// </summary>
        /// <param name="OrgId">组织架构编号</param>
        /// <param name="message">错误消息</param>
        /// <returns>是否有子数据</returns>
        public bool OrganizationIsHaveChildren(string OrgId,out string message) 
        {
            message = string.Empty;
            int ChildrenNum = 0;
            bool result= false;
            DataSet ds = new DataSet();
            DataTable dt = null;
            string sql = $" select count(OrgId) from Organization where parentid='{OrgId}' ";
            try
            {
                ds = m_dbHelper.ExecuteDataSet(sql);
                #region 非空检查
                if (ds == null)
                    return result;
                if (ds.Tables == null || ds.Tables.Count == 0)
                    return result;
                dt = ds.Tables[0];
                if (dt == null)
                    return result;
                if (dt.Rows.Count == 0)
                    return result;
                #endregion
                ChildrenNum = Utils.StrToInt(Convert.ToString(dt.Rows[0][0]), 0);
                if (ChildrenNum != 0)
                    result = true;
                else
                    result = false;
            }
            catch (Exception exp)
            {
                message = exp.Message;
            }
            return result;
        }

        /// <summary>
        /// 分页查询Organization数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="SortMethod">排序方法</param>
        /// <param name="PageSize">每页分页数据</param>
        /// <param name="CurPage">当前页</param>
        /// <param name="TotalNumber">总数据量</param>
        /// <param name="PageCount">总页数</param>
        /// <param name="message">错误消息</param>
        /// <returns>返回数据</returns>
        public List<Organization> QueryPageOrganization(
            string SqlWhere,
            string SortField,
            string SortMethod,
            int PageSize,
            int CurPage,
            out int TotalNumber,
            out int PageCount,
            out string message,
            string FieldStr = ""
            )
        {
            List<Organization> result = null;
            if(string.IsNullOrEmpty(FieldStr))
                FieldStr = FieldOrganization();
            Func<DataRow, Organization, Organization> func = (DataRow dr, Organization model) => {
                return ReadDataRow(dr, model);
            };
            result = QueryPage<Organization>(
                SqlWhere,
                SortField,
                SortMethod,
                FieldStr,
                "Organization",
                PageSize,
                CurPage,
                func,
                out TotalNumber,
                out PageCount,
                out message
                );
            return result;
        }
        #endregion

        #region PageTableColumn 增删改查
        /// <summary>
        /// 添加页面表格字段列表 
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="message">错误消息</param>
        /// <returns>添加条数</returns>
        public Task<Message> InsertPageTableColumn(List<PageTableColumn> lists)
        {
            int DbState = -1;
            Task<Message> result = null;
            string message = string.Empty;
            if (lists == null)
                return new Task<Message>(() => new Message(false, "参数为空不能添加"));
            CheckEmpty(ref lists);
            CheckMaxLength(ref lists, out message);
            if (!string.IsNullOrEmpty(message))
                return new Task<Message>(() => new Message(false, message));
            List<string> sqls = this.MarkInsertSql(lists);
            try
            {
                DbState = ExecuteNonQuery(string.Join(';', sqls.ToArray()));
            }
            catch (Exception ex)
            {
                result = new Task<Message>(() => new Message(false, ex.Message));
            }
            if (result == null)
            {
                result = new Task<Message>(() => new Message(true, string.Empty));
            }
            return result;
        }

        /// <summary>
        /// 修改页面表格字段列表 
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="SqlWhere">更新条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>修改条数</returns>
        public async Task<Message> UpdatePageTableColumn(List<PageTableColumn> lists, string SqlWhere)
        {
            Message result = null;
            int DbState = -1;
            string message = string.Empty;
            CheckEmpty(ref lists);
            List<string> sqls = this.MarkUpdateSql(lists, SqlWhere);
            try
            {
                DbState = ExecuteNonQuery(CommandType.Text, string.Join(';', sqls.ToArray()));
            }
            catch (Exception ex)
            {
                message = ex.Message;
                result = new Message(false, ex.Message);
            }
            if (result == null)
            {
                result = new Message(true, string.Empty);
            }
            return result;
        }

        /// <summary>
        /// 查询PageTableColumn数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>返回值</returns>
        public List<PageTableColumn> QueryPageTableColumn(string SqlWhere, out string message)
        {
            List<PageTableColumn> result = new List<PageTableColumn>();
            string sql = $"select {FieldPageTableColumn()} from PageTableColumn";
            DataSet ds = new DataSet();
            DataTable dt = null;
            message = string.Empty;
            if (!string.IsNullOrEmpty(SqlWhere))
                sql += $" where {SqlWhere} ";
            try
            {
                ds = m_dbHelper.ExecuteDataSet(sql);
                #region 非空检查
                if (ds == null)
                    return result;
                if (ds.Tables == null || ds.Tables.Count == 0)
                    return result;
                dt = ds.Tables[0];
                if (dt == null)
                    return result;
                if (dt.Rows.Count == 0)
                    return result;
                #endregion
                foreach (DataRow dtRow in dt.Rows)
                    result.Add(ReadDataRow(dtRow, new PageTableColumn()));
            }
            catch (Exception exp)
            {
                message = exp.Message;
            }
            return result;
        }

        /// <summary>
        /// 分页查询PageTableColumn数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="SortMethod">排序方法</param>
        /// <param name="PageSize">每页分页数据</param>
        /// <param name="CurPage">当前页</param>
        /// <param name="TotalNumber">总数据量</param>
        /// <param name="PageCount">总页数</param>
        /// <param name="message">错误消息</param>
        /// <returns>返回数据</returns>
        public List<PageTableColumn> QueryPagePageTableColumn(
            string SqlWhere,
            string SortField,
            string SortMethod,
            int PageSize,
            int CurPage,
            out int TotalNumber,
            out int PageCount,
            out string message
            )
        {
            List<PageTableColumn> result = null;
            string FieldStr = FieldPageTableColumn();
            Func<DataRow, PageTableColumn, PageTableColumn> func = (DataRow dr, PageTableColumn model) => {
                return ReadDataRow(dr, model);
            };
            result = QueryPage<PageTableColumn>(
                SqlWhere,
                SortField,
                SortMethod,
                FieldStr,
                "PageTableColumn",
                PageSize,
                CurPage,
                func,
                out TotalNumber,
                out PageCount,
                out message
                );
            return result;
        }
        #endregion

        #endregion

        #region Private

        #region Article基础方法
        /// <summary>
        /// 返回Article字段列表
        /// </summary>
        /// <returns>字段列表</returns>
        private string FieldArticle()
        {
            return @"
                    [ArticleId],
                    [ArticleTitle],
                    [Content],
                    [GroupId],
                    [Author],
                    [OrgId],
                    [IsSpecial],
                    [IsTop],
                    [State],
                    [ArticleType],
                    [CoverUrl],
                    [ViewCount],
                    [Backup01],
                    [Backup02],
                    [Backup03],
                    [Created],
                    [CreatedTime],
                    [Modified],
                    [ModifiedTime]
                     "
                     .Trim()
                     .Replace("\t", "")
                     .Replace("\r", "")
                     .Replace("\n", "");
        }

        /// <summary>
        /// 读取数据行到model(Article)
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="dr">数据行</param>
        private Article ReadDataRow(DataRow dr, Article model)
        {
            model = new Article();
            //文章编号
            model.ArticleId = Convert.IsDBNull(dr["ArticleId"]) ? 0 : Convert.ToInt64(dr["ArticleId"]);
            //文章的标题
            model.ArticleTitle = Convert.IsDBNull(dr["ArticleTitle"]) ? string.Empty : Convert.ToString(dr["ArticleTitle"]).Trim();
            //文章的内容[富文本]
            model.Content = Convert.IsDBNull(dr["Content"]) ? string.Empty : Convert.ToString(dr["Content"]).Trim();
            //文艺小组编号
            model.GroupId = Convert.IsDBNull(dr["GroupId"]) ? 0 : Convert.ToInt64(dr["GroupId"]);
            //文章作者
            model.Author = Convert.IsDBNull(dr["Author"]) ? string.Empty : Convert.ToString(dr["Author"]).Trim();
            //组织架构编号[这篇文章属于哪个组织架构]
            model.OrgId = Convert.IsDBNull(dr["OrgId"]) ? 0 : Convert.ToInt64(dr["OrgId"]);
            //是否特色文化展示[0否1是]
            model.IsSpecial = Convert.IsDBNull(dr["IsSpecial"]) ? 0 : Convert.ToInt32(dr["IsSpecial"]);
            //是否置顶[0否1是]
            model.IsTop = Convert.IsDBNull(dr["IsTop"]) ? 0 : Convert.ToInt32(dr["IsTop"]);
            //状态[0显示、1隐藏]
            model.State = Convert.IsDBNull(dr["State"]) ? 0 : Convert.ToInt32(dr["State"]);
            //文章类型[0普通文章、1图片文、2视频文章]
            model.ArticleType = Convert.IsDBNull(dr["ArticleType"]) ? 0 : Convert.ToInt32(dr["ArticleType"]);
            //文章封面[文章必须有封面没有会默认一张封面图]
            model.CoverUrl = Convert.IsDBNull(dr["CoverUrl"]) ? string.Empty : Convert.ToString(dr["CoverUrl"]).Trim();
            //点击量
            model.ViewCount = Convert.IsDBNull(dr["ViewCount"]) ? 0 : Convert.ToInt32(dr["ViewCount"]);
            //备用字段01
            model.Backup01 = Convert.IsDBNull(dr["Backup01"]) ? string.Empty : Convert.ToString(dr["Backup01"]).Trim();
            //备用字段02
            model.Backup02 = Convert.IsDBNull(dr["Backup02"]) ? string.Empty : Convert.ToString(dr["Backup02"]).Trim();
            //备用字段03
            model.Backup03 = Convert.IsDBNull(dr["Backup03"]) ? string.Empty : Convert.ToString(dr["Backup03"]).Trim();
            //创建人
            model.Created = Convert.IsDBNull(dr["Created"]) ? string.Empty : Convert.ToString(dr["Created"]).Trim();
            //创建时间
            model.CreatedTime = Convert.IsDBNull(dr["CreatedTime"]) ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(dr["CreatedTime"]);
            //修改人
            model.Modified = Convert.IsDBNull(dr["Modified"]) ? string.Empty : Convert.ToString(dr["Modified"]).Trim();
            //修改时间
            model.ModifiedTime = Convert.IsDBNull(dr["ModifiedTime"]) ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(dr["ModifiedTime"]);

            return model;
        }

        ///<summary>
        ///检查是否空值(Article)
        ///</summary>
        private void CheckEmpty(ref List<Article> lists)
        {
            for (int i = 0; i < lists.Count; i++)
            {
                //文章编号
                lists[i].ArticleId = lists[i].ArticleId == null ? Convert.ToInt64(0) : Convert.ToInt64(lists[i].ArticleId);
                //文章的标题
                lists[i].ArticleTitle = string.IsNullOrEmpty(lists[i].ArticleTitle) ? string.Empty : Convert.ToString(lists[i].ArticleTitle).Trim();
                //文章的内容[富文本]
                lists[i].Content = string.IsNullOrEmpty(lists[i].Content) ? string.Empty : Convert.ToString(lists[i].Content).Trim();
                //文艺小组编号
                lists[i].GroupId = lists[i].GroupId == null ? Convert.ToInt64(0) : Convert.ToInt64(lists[i].GroupId);
                //文章作者
                lists[i].Author = string.IsNullOrEmpty(lists[i].Author) ? string.Empty : Convert.ToString(lists[i].Author).Trim();
                //组织架构编号[这篇文章属于哪个组织架构]
                lists[i].OrgId = lists[i].OrgId == null ? Convert.ToInt64(0) : Convert.ToInt64(lists[i].OrgId);
                //是否特色文化展示[0否1是]
                lists[i].IsSpecial = lists[i].IsSpecial == null ? Convert.ToInt32(0) : Convert.ToInt32(lists[i].IsSpecial);
                //是否置顶[0否1是]
                lists[i].IsTop = lists[i].IsTop == null ? Convert.ToInt32(0) : Convert.ToInt32(lists[i].IsTop);
                //状态[0显示、1隐藏]
                lists[i].State = lists[i].State == null ? Convert.ToInt32(0) : Convert.ToInt32(lists[i].State);
                //文章类型[0普通文章、1图片文、2视频文章]
                lists[i].ArticleType = lists[i].ArticleType == null ? Convert.ToInt32(0) : Convert.ToInt32(lists[i].ArticleType);
                //文章封面[文章必须有封面没有会默认一张封面图]
                lists[i].CoverUrl = string.IsNullOrEmpty(lists[i].CoverUrl) ? string.Empty : Convert.ToString(lists[i].CoverUrl).Trim();
                //点击量
                lists[i].ViewCount = lists[i].ViewCount == null ? Convert.ToInt32(0) : Convert.ToInt32(lists[i].ViewCount);
                //备用字段01
                lists[i].Backup01 = string.IsNullOrEmpty(lists[i].Backup01) ? string.Empty : Convert.ToString(lists[i].Backup01).Trim();
                //备用字段02
                lists[i].Backup02 = string.IsNullOrEmpty(lists[i].Backup02) ? string.Empty : Convert.ToString(lists[i].Backup02).Trim();
                //备用字段03
                lists[i].Backup03 = string.IsNullOrEmpty(lists[i].Backup03) ? string.Empty : Convert.ToString(lists[i].Backup03).Trim();
                //创建人
                lists[i].Created = string.IsNullOrEmpty(lists[i].Created) ? string.Empty : Convert.ToString(lists[i].Created).Trim();
                //创建时间
                lists[i].CreatedTime = lists[i].CreatedTime == null ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(lists[i].CreatedTime.GetValueOrDefault());
                //修改人
                lists[i].Modified = string.IsNullOrEmpty(lists[i].Modified) ? string.Empty : Convert.ToString(lists[i].Modified).Trim();
                //修改时间
                lists[i].ModifiedTime = lists[i].ModifiedTime == null ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(lists[i].ModifiedTime.GetValueOrDefault());
            }
        }

        ///<summary>
        ///检查是否超过长度(Article)
        ///</summary>
        ///<param name="lists">数据集</param>
        ///<param name="message">错误消息</param>
        private void CheckMaxLength(ref List<Article> lists, out string message)
        {
            #region 声明变量

            //错误消息
            message = string.Empty;

            //超过的长度
            int OutLength = 0;
            #endregion

            #region 循环验证长度
            for (int i = 0; i < lists.Count; i++)
            {
                if (!string.IsNullOrEmpty(lists[i].ArticleTitle))
                {
                    if (lists[i].ArticleTitle.Length > 100)
                    {
                        OutLength = lists[i].ArticleTitle.Length - 100;
                        message += "字段名[ArticleTitle]描述[文章的标题]超长、字段最长[100]实际" + lists[i].ArticleTitle.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Content))
                {
                    if (lists[i].Content.Length > 4000)
                    {
                        OutLength = lists[i].Content.Length - 4000;
                        message += "字段名[Content]描述[文章的内容[富文本]]超长、字段最长[4000]实际" + lists[i].Content.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Author))
                {
                    if (lists[i].Author.Length > 200)
                    {
                        OutLength = lists[i].Author.Length - 200;
                        message += "字段名[Author]描述[文章作者]超长、字段最长[200]实际" + lists[i].Author.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].CoverUrl))
                {
                    if (lists[i].CoverUrl.Length > 300)
                    {
                        OutLength = lists[i].CoverUrl.Length - 300;
                        message += "字段名[CoverUrl]描述[文章封面[文章必须有封面没有会默认一张封面图]]超长、字段最长[300]实际" + lists[i].CoverUrl.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Backup01))
                {
                    if (lists[i].Backup01.Length > 95)
                    {
                        OutLength = lists[i].Backup01.Length - 95;
                        message += "字段名[Backup01]描述[备用字段01]超长、字段最长[95]实际" + lists[i].Backup01.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Backup02))
                {
                    if (lists[i].Backup02.Length > 95)
                    {
                        OutLength = lists[i].Backup02.Length - 95;
                        message += "字段名[Backup02]描述[备用字段02]超长、字段最长[95]实际" + lists[i].Backup02.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Backup03))
                {
                    if (lists[i].Backup03.Length > 95)
                    {
                        OutLength = lists[i].Backup03.Length - 95;
                        message += "字段名[Backup03]描述[备用字段03]超长、字段最长[95]实际" + lists[i].Backup03.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Created))
                {
                    if (lists[i].Created.Length > 50)
                    {
                        OutLength = lists[i].Created.Length - 50;
                        message += "字段名[Created]描述[创建人]超长、字段最长[50]实际" + lists[i].Created.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Modified))
                {
                    if (lists[i].Modified.Length > 50)
                    {
                        OutLength = lists[i].Modified.Length - 50;
                        message += "字段名[Modified]描述[修改人]超长、字段最长[50]实际" + lists[i].Modified.Length + "超过长度" + OutLength + ",";
                    }
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(message)) message = message.Substring(0, message.Length - 1);
        }

        ///<summary>
        ///生成插入Sql语句(Article)
        ///</summary>
        ///<param name="lists">数据List</param>
        ///<returns>插入Sql语句字符串数组</returns>
        private List<string> MarkInsertSql(List<Article> lists)
        {
            List<string> result = new List<string>();
            foreach (Article model in lists)
            {
                #region 拼写Sql语句
                string Sql = "insert into Article(";
                Sql += "ArticleId,";
                Sql += "ArticleTitle,";
                Sql += "Content,";
                Sql += "GroupId,";
                Sql += "Author,";
                Sql += "OrgId,";
                Sql += "IsSpecial,";
                Sql += "IsTop,";
                Sql += "State,";
                Sql += "ArticleType,";
                Sql += "CoverUrl,";
                Sql += "ViewCount,";
                Sql += "Backup01,";
                Sql += "Backup02,";
                Sql += "Backup03,";
                Sql += "Created,";
                Sql += "CreatedTime,";
                Sql += "Modified,";
                Sql += "ModifiedTime";
                Sql += ") values(";
                Sql += "'" + FilteSQLStr(model.ArticleId) + "',";
                Sql += "'" + FilteSQLStr(model.ArticleTitle) + "',";
                Sql += "'" + model.Content + "',";//注意,这里不能转码
                Sql += "'" + FilteSQLStr(model.GroupId) + "',";
                Sql += "'" + FilteSQLStr(model.Author) + "',";
                Sql += "'" + FilteSQLStr(model.OrgId) + "',";
                Sql += "'" + FilteSQLStr(model.IsSpecial) + "',";
                Sql += "'" + FilteSQLStr(model.IsTop) + "',";
                Sql += "'" + FilteSQLStr(model.State) + "',";
                Sql += "'" + FilteSQLStr(model.ArticleType) + "',";
                Sql += "'" + FilteSQLStr(model.CoverUrl) + "',";
                Sql += "'" + FilteSQLStr(model.ViewCount) + "',";
                Sql += "'" + FilteSQLStr(model.Backup01) + "',";
                Sql += "'" + FilteSQLStr(model.Backup02) + "',";
                Sql += "'" + FilteSQLStr(model.Backup03) + "',";
                Sql += "'" + FilteSQLStr(model.Created) + "',";
                Sql += "CAST('" + model.CreatedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss") + "' AS DATETIME),";
                Sql += "'" + FilteSQLStr(model.Modified) + "',";
                Sql += "CAST('" + model.ModifiedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss") + "' AS DATETIME)";
                Sql += ")";
                #endregion
                result.Add(Sql);
            }
            return result;
        }

        ///<summary>
        ///生成更新Sql语句(Article)
        ///</summary>
        ///<param name="lists">数据List</param>
        ///<param name="SqlWhere">更新条件</param>
        ///<returns>更新Sql语句字符串数组</returns>
        private List<string> MarkUpdateSql(List<Article> lists, string SqlWhere)
        {
            List<string> result = new List<string>();
            foreach (Article model in lists)
            {
                #region 拼写Sql语句
                string Sql = "update Article set ";
                Sql += "ArticleId='" + FilteSQLStr(model.ArticleId) + "',";
                Sql += "ArticleTitle='" + FilteSQLStr(model.ArticleTitle) + "',";
                Sql += "Content='" + model.Content + "',";
                Sql += "GroupId='" + FilteSQLStr(model.GroupId) + "',";
                Sql += "Author='" + FilteSQLStr(model.Author) + "',";
                Sql += "OrgId='" + FilteSQLStr(model.OrgId) + "',";
                Sql += "IsSpecial='" + FilteSQLStr(model.IsSpecial) + "',";
                Sql += "IsTop='" + FilteSQLStr(model.IsTop) + "',";
                Sql += "State='" + FilteSQLStr(model.State) + "',";
                Sql += "ArticleType='" + FilteSQLStr(model.ArticleType) + "',";
                Sql += "CoverUrl='" + FilteSQLStr(model.CoverUrl) + "',";
                Sql += "ViewCount='" + FilteSQLStr(model.ViewCount) + "',";
                Sql += "Backup01='" + FilteSQLStr(model.Backup01) + "',";
                Sql += "Backup02='" + FilteSQLStr(model.Backup02) + "',";
                Sql += "Backup03='" + FilteSQLStr(model.Backup03) + "',";
                Sql += "Created='" + FilteSQLStr(model.Created) + "',";
                Sql += "CreatedTime=CAST('" + model.CreatedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss") + "' AS DATETIME),";
                Sql += "Modified='" + FilteSQLStr(model.Modified) + "',";
                Sql += "ModifiedTime=CAST('" + model.ModifiedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss") + "' AS DATETIME)";
                if (!string.IsNullOrEmpty(SqlWhere))
                    Sql += " Where " + SqlWhere;
                #endregion
                result.Add(Sql);
            }
            return result;
        }
        #endregion

        #region vw_Article基础方法

        /// <summary>
        /// 返回Article字段列表
        /// </summary>
        /// <returns>字段列表</returns>
        private string FieldVWArticle()
        {
            return @"
                    [ArticleId],
                    [ArticleTitle],
                    [Content],
                    [Author],
                    [GroupName],
                    [IsSpecial],
                    [IsTop],
                    [State],
                    [ArticleType],
                    [CoverUrl],
                    [ViewCount],
                    [Backup01],
                    [Backup02],
                    [Backup03],
                    [Created],
                    [CreatedTime],
                    [Modified],
                    [ModifiedTime],
                    [OrgName],
                    [FontColor],
                    [GroupId],
                    [OrgId]
                     "
                     .Trim()
                     .Replace("\t", "")
                     .Replace("\r", "")
                     .Replace("\n", "");
        }

        /// <summary>
        /// 读取数据行到model
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="dr">数据行</param>
        private vw_Article ReadDataRow(DataRow dr, vw_Article model)
        {
            model = new vw_Article();
            //文章编号
            model.ArticleId = GetDataRow(dr, "ArticleId") == null ? 0 : Convert.ToInt64(GetDataRow(dr, "ArticleId"));
            //文章标题
            model.ArticleTitle = GetDataRow(dr, "ArticleTitle") == null ? string.Empty : Convert.ToString(GetDataRow(dr, "ArticleTitle")).Trim();
            //文章内容
            model.Content = GetDataRow(dr, "Content") == null ? string.Empty : Convert.ToString(GetDataRow(dr, "Content")).Trim();
            //文章作者
            model.Author = GetDataRow(dr, "Author") == null ? string.Empty : Convert.ToString(GetDataRow(dr, "Author")).Trim();
            //文艺小组名称
            model.GroupName = GetDataRow(dr, "GroupName") == null ? string.Empty : Convert.ToString(GetDataRow(dr, "GroupName")).Trim();
            //是否特色文化展示
            model.IsSpecial = GetDataRow(dr, "IsSpecial") == null ? 0 : Convert.ToInt32(GetDataRow(dr, "IsSpecial"));
            //是否置顶
            model.IsTop = GetDataRow(dr, "IsTop") == null ? 0 : Convert.ToInt32(GetDataRow(dr, "IsTop"));
            //状态
            model.State = GetDataRow(dr, "State") == null ? 0 : Convert.ToInt32(GetDataRow(dr, "State"));
            //文章类型
            model.ArticleType = GetDataRow(dr, "ArticleType") == null ? 0 : Convert.ToInt32(GetDataRow(dr, "ArticleType"));
            //文章封面
            model.CoverUrl = GetDataRow(dr, "CoverUrl") == null ? string.Empty : Convert.ToString(GetDataRow(dr, "CoverUrl")).Trim();
            //点击量
            model.ViewCount = GetDataRow(dr, "ViewCount") == null ? 0 : Convert.ToInt32(GetDataRow(dr, "ViewCount"));
            //备用字段01
            model.Backup01 = GetDataRow(dr, "Backup01") == null ? string.Empty : Convert.ToString(GetDataRow(dr, "Backup01")).Trim();
            //备用字段02
            model.Backup02 = GetDataRow(dr, "Backup02") == null ? string.Empty : Convert.ToString(GetDataRow(dr, "Backup02")).Trim();
            //备用字段03
            model.Backup03 = GetDataRow(dr, "Backup03") == null ? string.Empty : Convert.ToString(GetDataRow(dr, "Backup03")).Trim();
            //创建人
            model.Created = GetDataRow(dr, "Created") == null ? string.Empty : Convert.ToString(GetDataRow(dr, "Created")).Trim();
            //创建时间
            model.CreatedTime = GetDataRow(dr, "CreatedTime") == null ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(GetDataRow(dr, "CreatedTime"));
            //修改人
            model.Modified = GetDataRow(dr, "Modified") == null ? string.Empty : Convert.ToString(GetDataRow(dr, "Modified")).Trim();
            //修改时间
            model.ModifiedTime = GetDataRow(dr, "ModifiedTime") == null ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(GetDataRow(dr, "ModifiedTime"));
            //组织架构名称
            model.OrgName = GetDataRow(dr, "OrgName") == null ? string.Empty : Convert.ToString(GetDataRow(dr, "OrgName")).Trim();
            //字体颜色
            model.FontColor = GetDataRow(dr, "FontColor") == null ? string.Empty : Convert.ToString(GetDataRow(dr, "FontColor")).Trim();
            //文艺小组编号
            model.GroupId = GetDataRow(dr, "GroupId") == null ? 0 : Convert.ToInt64(GetDataRow(dr, "GroupId"));
            //组织架构编号
            model.OrgId = GetDataRow(dr, "OrgId") == null ? 0 : Convert.ToInt64(GetDataRow(dr, "OrgId"));

            return model;
        }
        #endregion

        #region FileInfo基础方法
        /// <summary>
        /// 返回FileInfo字段列表
        /// </summary>
        /// <returns>字段列表</returns>
        private string FieldFileInfo()
        {
            return @"
                    [FileId],
                    [FileName],
                    [SrcFileName],
                    [FileUrl],
                    [FileType],
                    [FileSize],
                    [Backup01],
                    [Backup02],
                    [Backup03],
                    [Created],
                    [CreatedTime]
                     "
                     .Trim()
                     .Replace("\t", "")
                     .Replace("\r", "")
                     .Replace("\n", "");
        }

        /// <summary>
        /// 读取数据行到model(FileInfo)
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="dr">数据行</param>
        private Models.FileInfo ReadDataRow(DataRow dr, Models.FileInfo model)
        {
            model = new Models.FileInfo();
            //文件编号
            model.FileId = Convert.IsDBNull(GetDataRow(dr,"FileId")) ? 0 : Convert.ToInt64(GetDataRow(dr,"FileId"));
            //文件名
            model.FileName = Convert.IsDBNull(GetDataRow(dr,"FileName")) ? string.Empty : Convert.ToString(GetDataRow(dr,"FileName")).Trim();
            //原始文件名
            model.SrcFileName = Convert.IsDBNull(GetDataRow(dr,"SrcFileName")) ? string.Empty : Convert.ToString(GetDataRow(dr,"SrcFileName")).Trim();
            //URL地址
            model.FileUrl = Convert.IsDBNull(GetDataRow(dr,"FileUrl")) ? string.Empty : Convert.ToString(GetDataRow(dr,"FileUrl")).Trim();
            //文件类型[只允许word、excel、ppt、pdf和mp4]
            model.FileType = Convert.IsDBNull(GetDataRow(dr,"FileType")) ? string.Empty : Convert.ToString(GetDataRow(dr,"FileType")).Trim();
            //文件大小
            model.FileSize = Convert.IsDBNull(GetDataRow(dr,"FileSize")) ? 0 : Convert.ToDouble(GetDataRow(dr,"FileSize"));
            //备用字段01
            model.Backup01 = Convert.IsDBNull(GetDataRow(dr,"Backup01")) ? string.Empty : Convert.ToString(GetDataRow(dr, "Backup01")).Trim();
            //备用字段02
            model.Backup02 = Convert.IsDBNull(GetDataRow(dr, "Backup02")) ? string.Empty : Convert.ToString(GetDataRow(dr, "Backup02")).Trim();
            //备用字段03
            model.Backup03 = Convert.IsDBNull(GetDataRow(dr, "Backup03")) ? string.Empty : Convert.ToString(GetDataRow(dr, "Backup03")).Trim();
            //创建人
            model.Created = Convert.IsDBNull(GetDataRow(dr, "Created")) ? string.Empty : Convert.ToString(GetDataRow(dr, "Created")).Trim();
            //创建时间
            model.CreatedTime = Convert.IsDBNull(GetDataRow(dr,"CreatedTime")) ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(GetDataRow(dr, "CreatedTime"));

            return model;
        }

        ///<summary>
        ///检查是否空值(FileInfo)
        ///</summary>
        private void CheckEmpty(ref List<Models.FileInfo> lists)
        {
            for (int i = 0; i < lists.Count; i++)
            {
                //文件编号
                lists[i].FileId = lists[i].FileId == null ? Convert.ToInt64(0) : Convert.ToInt64(lists[i].FileId);
                //文件名
                lists[i].FileName = string.IsNullOrEmpty(lists[i].FileName) ? string.Empty : Convert.ToString(lists[i].FileName).Trim();
                //原始文件名
                lists[i].SrcFileName = string.IsNullOrEmpty(lists[i].SrcFileName) ? string.Empty : Convert.ToString(lists[i].SrcFileName).Trim();
                //URL地址
                lists[i].FileUrl = string.IsNullOrEmpty(lists[i].FileUrl) ? string.Empty : Convert.ToString(lists[i].FileUrl).Trim();
                //文件类型[只允许word、excel、ppt、pdf和mp4]
                lists[i].FileType = string.IsNullOrEmpty(lists[i].FileType) ? string.Empty : Convert.ToString(lists[i].FileType).Trim();
                //文件大小
                lists[i].FileSize = lists[i].FileSize == null ? Convert.ToDouble(0) : Convert.ToDouble(lists[i].FileSize);
                //备用字段01
                lists[i].Backup01 = string.IsNullOrEmpty(lists[i].Backup01) ? string.Empty : Convert.ToString(lists[i].Backup01).Trim();
                //备用字段02
                lists[i].Backup02 = string.IsNullOrEmpty(lists[i].Backup02) ? string.Empty : Convert.ToString(lists[i].Backup02).Trim();
                //备用字段03
                lists[i].Backup03 = string.IsNullOrEmpty(lists[i].Backup03) ? string.Empty : Convert.ToString(lists[i].Backup03).Trim();
                //创建人
                lists[i].Created = string.IsNullOrEmpty(lists[i].Created) ? string.Empty : Convert.ToString(lists[i].Created).Trim();
                //创建时间
                lists[i].CreatedTime = lists[i].CreatedTime == null ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(lists[i].CreatedTime.GetValueOrDefault());
            }
        }

        ///<summary>
        ///检查是否超过长度(FileInfo)
        ///</summary>
        ///<param name="lists">数据集</param>
        ///<param name="message">错误消息</param>
        private void CheckMaxLength(ref List<Models.FileInfo> lists, out string message)
        {
            #region 声明变量

            //错误消息
            message = string.Empty;

            //超过的长度
            int OutLength = 0;
            #endregion

            #region 循环验证长度
            for (int i = 0; i < lists.Count; i++)
            {
                if (!string.IsNullOrEmpty(lists[i].FileName))
                {
                    if (lists[i].FileName.Length > 200)
                    {
                        OutLength = lists[i].FileName.Length - 200;
                        message += "字段名[FileName]描述[文件名]超长、字段最长[200]实际" + lists[i].FileName.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].SrcFileName))
                {
                    if (lists[i].SrcFileName.Length > 200)
                    {
                        OutLength = lists[i].SrcFileName.Length - 200;
                        message += "字段名[SrcFileName]描述[原始文件名]超长、字段最长[200]实际" + lists[i].SrcFileName.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].FileUrl))
                {
                    if (lists[i].FileUrl.Length > 500)
                    {
                        OutLength = lists[i].FileUrl.Length - 500;
                        message += "字段名[FileUrl]描述[URL地址]超长、字段最长[500]实际" + lists[i].FileUrl.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].FileType))
                {
                    if (lists[i].FileType.Length > 80)
                    {
                        OutLength = lists[i].FileType.Length - 80;
                        message += "字段名[FileType]描述[文件类型[只允许word、excel、ppt、pdf和mp4]]超长、字段最长[80]实际" + lists[i].FileType.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Backup01))
                {
                    if (lists[i].Backup01.Length > 95)
                    {
                        OutLength = lists[i].Backup01.Length - 95;
                        message += "字段名[Backup01]描述[备用字段01]超长、字段最长[95]实际" + lists[i].Backup01.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Backup02))
                {
                    if (lists[i].Backup02.Length > 95)
                    {
                        OutLength = lists[i].Backup02.Length - 95;
                        message += "字段名[Backup02]描述[备用字段02]超长、字段最长[95]实际" + lists[i].Backup02.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Backup03))
                {
                    if (lists[i].Backup03.Length > 95)
                    {
                        OutLength = lists[i].Backup03.Length - 95;
                        message += "字段名[Backup03]描述[备用字段03]超长、字段最长[95]实际" + lists[i].Backup03.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Created))
                {
                    if (lists[i].Created.Length > 50)
                    {
                        OutLength = lists[i].Created.Length - 50;
                        message += "字段名[Created]描述[创建人]超长、字段最长[50]实际" + lists[i].Created.Length + "超过长度" + OutLength + ",";
                    }
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(message)) message = message.Substring(0, message.Length - 1);
        }

        ///<summary>
        ///生成插入Sql语句(FileInfo)
        ///</summary>
        ///<param name="lists">数据List</param>
        ///<returns>插入Sql语句字符串数组</returns>
        private List<string> MarkInsertSql(List<Models.FileInfo> lists)
        {
            List<string> result = new List<string>();
            foreach (Models.FileInfo model in lists)
            {
                #region 拼写Sql语句
                string Sql = "insert into FileInfo(";
                Sql += "FileId,";
                Sql += "FileName,";
                Sql += "SrcFileName,";
                Sql += "FileUrl,";
                Sql += "FileType,";
                Sql += "FileSize,";
                Sql += "Backup01,";
                Sql += "Backup02,";
                Sql += "Backup03,";
                Sql += "Created,";
                Sql += "CreatedTime";
                Sql += ") values(";
                Sql += "'" + FilteSQLStr(model.FileId) + "',";
                Sql += "'" + FilteSQLStr(model.FileName) + "',";
                Sql += "'" + FilteSQLStr(model.SrcFileName) + "',";
                Sql += "'" + FilteSQLStr(model.FileUrl) + "',";
                Sql += "'" + FilteSQLStr(model.FileType) + "',";
                Sql += "'" + FilteSQLStr(model.FileSize) + "',";
                Sql += "'" + FilteSQLStr(model.Backup01) + "',";
                Sql += "'" + FilteSQLStr(model.Backup02) + "',";
                Sql += "'" + FilteSQLStr(model.Backup03) + "',";
                Sql += "'" + FilteSQLStr(model.Created) + "',";
                Sql += "CAST('" + model.CreatedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss") + "' AS DATETIME)";
                Sql += ")";
                #endregion
                result.Add(Sql);
            }
            return result;
        }

        ///<summary>
        ///生成更新Sql语句(FileInfo)
        ///</summary>
        ///<param name="lists">数据List</param>
        ///<param name="SqlWhere">更新条件</param>
        ///<returns>更新Sql语句字符串数组</returns>
        private List<string> MarkUpdateSql(List<Models.FileInfo> lists, string SqlWhere)
        {
            List<string> result = new List<string>();
            foreach (Models.FileInfo model in lists)
            {
                #region 拼写Sql语句
                string Sql = "update FileInfo set ";
                Sql += "FileId='" + FilteSQLStr(model.FileId) + "',";
                Sql += "FileName='" + FilteSQLStr(model.FileName) + "',";
                Sql += "SrcFileName='" + FilteSQLStr(model.SrcFileName) + "',";
                Sql += "FileUrl='" + FilteSQLStr(model.FileUrl) + "',";
                Sql += "FileType='" + FilteSQLStr(model.FileType) + "',";
                Sql += "FileSize='" + FilteSQLStr(model.FileSize) + "',";
                Sql += "Backup01='" + FilteSQLStr(model.Backup01) + "',";
                Sql += "Backup02='" + FilteSQLStr(model.Backup02) + "',";
                Sql += "Backup03='" + FilteSQLStr(model.Backup03) + "',";
                Sql += "Created='" + FilteSQLStr(model.Created) + "',";
                Sql += "CreatedTime=CAST('" + model.CreatedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss") + "' AS DATETIME)";
                if (!string.IsNullOrEmpty(SqlWhere))
                    Sql += " Where " + SqlWhere;
                #endregion
                result.Add(Sql);
            }
            return result;
        }
        #endregion

        #region Group基础方法
        /// <summary>
        /// 返回Group字段列表
        /// </summary>
        /// <returns>字段列表</returns>
        private string FieldGroup()
        {
            return @"
                    [GroupId],
                    [GroupName],
                    [Backup01],
                    [Backup02],
                    [Backup03],
                    [Created],
                    [CreatedTime],
                    [Modified],
                    [ModifiedTime]
                     "
                     .Trim()
                     .Replace("\t", "")
                     .Replace("\r", "")
                     .Replace("\n", "");
        }

        /// <summary>
        /// 读取数据行到model(Group)
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="dr">数据行</param>
        private Group ReadDataRow(DataRow dr, Group model)
        {
            model = new Group();
            //文艺小组编号
            model.GroupId = Convert.IsDBNull(dr["GroupId"]) ? 0 : Convert.ToInt64(dr["GroupId"]);
            //文艺小组
            model.GroupName = Convert.IsDBNull(dr["GroupName"]) ? string.Empty : Convert.ToString(dr["GroupName"]).Trim();
            //备用字段01
            model.Backup01 = Convert.IsDBNull(dr["Backup01"]) ? string.Empty : Convert.ToString(dr["Backup01"]).Trim();
            //备用字段02
            model.Backup02 = Convert.IsDBNull(dr["Backup02"]) ? string.Empty : Convert.ToString(dr["Backup02"]).Trim();
            //备用字段03
            model.Backup03 = Convert.IsDBNull(dr["Backup03"]) ? string.Empty : Convert.ToString(dr["Backup03"]).Trim();
            //创建人
            model.Created = Convert.IsDBNull(dr["Created"]) ? string.Empty : Convert.ToString(dr["Created"]).Trim();
            //创建时间
            model.CreatedTime = Convert.IsDBNull(dr["CreatedTime"]) ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(dr["CreatedTime"]);
            //修改人
            model.Modified = Convert.IsDBNull(dr["Modified"]) ? string.Empty : Convert.ToString(dr["Modified"]).Trim();
            //修改时间
            model.ModifiedTime = Convert.IsDBNull(dr["ModifiedTime"]) ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(dr["ModifiedTime"]);

            return model;
        }

        ///<summary>
        ///检查是否空值(Group)
        ///</summary>
        private void CheckEmpty(ref List<Group> lists)
        {
            for (int i = 0; i < lists.Count; i++)
            {
                //文艺小组编号
                lists[i].GroupId = lists[i].GroupId == null ? Convert.ToInt64(0) : Convert.ToInt64(lists[i].GroupId);
                //文艺小组
                lists[i].GroupName = string.IsNullOrEmpty(lists[i].GroupName) ? string.Empty : Convert.ToString(lists[i].GroupName).Trim();
                //备用字段01
                lists[i].Backup01 = string.IsNullOrEmpty(lists[i].Backup01) ? string.Empty : Convert.ToString(lists[i].Backup01).Trim();
                //备用字段02
                lists[i].Backup02 = string.IsNullOrEmpty(lists[i].Backup02) ? string.Empty : Convert.ToString(lists[i].Backup02).Trim();
                //备用字段03
                lists[i].Backup03 = string.IsNullOrEmpty(lists[i].Backup03) ? string.Empty : Convert.ToString(lists[i].Backup03).Trim();
                //创建人
                lists[i].Created = string.IsNullOrEmpty(lists[i].Created) ? string.Empty : Convert.ToString(lists[i].Created).Trim();
                //创建时间
                lists[i].CreatedTime = lists[i].CreatedTime == null ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(lists[i].CreatedTime.GetValueOrDefault());
                //修改人
                lists[i].Modified = string.IsNullOrEmpty(lists[i].Modified) ? string.Empty : Convert.ToString(lists[i].Modified).Trim();
                //修改时间
                lists[i].ModifiedTime = lists[i].ModifiedTime == null ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(lists[i].ModifiedTime.GetValueOrDefault());
            }
        }

        ///<summary>
        ///检查是否超过长度(Group)
        ///</summary>
        ///<param name="lists">数据集</param>
        ///<param name="message">错误消息</param>
        private void CheckMaxLength(ref List<Group> lists, out string message)
        {
            #region 声明变量

            //错误消息
            message = string.Empty;

            //超过的长度
            int OutLength = 0;
            #endregion

            #region 循环验证长度
            for (int i = 0; i < lists.Count; i++)
            {
                if (!string.IsNullOrEmpty(lists[i].GroupName))
                {
                    if (lists[i].GroupName.Length > 80)
                    {
                        OutLength = lists[i].GroupName.Length - 80;
                        message += "字段名[GroupName]描述[文艺小组]超长、字段最长[80]实际" + lists[i].GroupName.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Backup01))
                {
                    if (lists[i].Backup01.Length > 95)
                    {
                        OutLength = lists[i].Backup01.Length - 95;
                        message += "字段名[Backup01]描述[备用字段01]超长、字段最长[95]实际" + lists[i].Backup01.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Backup02))
                {
                    if (lists[i].Backup02.Length > 95)
                    {
                        OutLength = lists[i].Backup02.Length - 95;
                        message += "字段名[Backup02]描述[备用字段02]超长、字段最长[95]实际" + lists[i].Backup02.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Backup03))
                {
                    if (lists[i].Backup03.Length > 95)
                    {
                        OutLength = lists[i].Backup03.Length - 95;
                        message += "字段名[Backup03]描述[备用字段03]超长、字段最长[95]实际" + lists[i].Backup03.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Created))
                {
                    if (lists[i].Created.Length > 50)
                    {
                        OutLength = lists[i].Created.Length - 50;
                        message += "字段名[Created]描述[创建人]超长、字段最长[50]实际" + lists[i].Created.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Modified))
                {
                    if (lists[i].Modified.Length > 50)
                    {
                        OutLength = lists[i].Modified.Length - 50;
                        message += "字段名[Modified]描述[修改人]超长、字段最长[50]实际" + lists[i].Modified.Length + "超过长度" + OutLength + ",";
                    }
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(message)) message = message.Substring(0, message.Length - 1);
        }

        ///<summary>
        ///生成插入Sql语句(Group)
        ///</summary>
        ///<param name="lists">数据List</param>
        ///<returns>插入Sql语句字符串数组</returns>
        private List<string> MarkInsertSql(List<Group> lists)
        {
            List<string> result = new List<string>();
            foreach (Group model in lists)
            {
                #region 拼写Sql语句
                string Sql = "insert into [Group](";
                Sql += "GroupId,";
                Sql += "GroupName,";
                Sql += "Backup01,";
                Sql += "Backup02,";
                Sql += "Backup03,";
                Sql += "Created,";
                Sql += "CreatedTime,";
                Sql += "Modified,";
                Sql += "ModifiedTime";
                Sql += ") values(";
                Sql += "'" + FilteSQLStr(model.GroupId) + "',";
                Sql += "'" + FilteSQLStr(model.GroupName) + "',";
                Sql += "'" + FilteSQLStr(model.Backup01) + "',";
                Sql += "'" + FilteSQLStr(model.Backup02) + "',";
                Sql += "'" + FilteSQLStr(model.Backup03) + "',";
                Sql += "'" + FilteSQLStr(model.Created) + "',";
                Sql += "CAST('" + model.CreatedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss") + "' AS DATETIME),";
                Sql += "'" + FilteSQLStr(model.Modified) + "',";
                Sql += "CAST('" + model.ModifiedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss") + "' AS DATETIME)";
                Sql += ")";
                #endregion
                result.Add(Sql);
            }
            return result;
        }

        ///<summary>
        ///生成更新Sql语句(Group)
        ///</summary>
        ///<param name="lists">数据List</param>
        ///<param name="SqlWhere">更新条件</param>
        ///<returns>更新Sql语句字符串数组</returns>
        private List<string> MarkUpdateSql(List<Group> lists, string SqlWhere)
        {
            List<string> result = new List<string>();
            foreach (Group model in lists)
            {
                #region 拼写Sql语句
                string Sql = "update [Group] set ";
                Sql += "GroupId='" + FilteSQLStr(model.GroupId) + "',";
                Sql += "GroupName='" + FilteSQLStr(model.GroupName) + "',";
                Sql += "Backup01='" + FilteSQLStr(model.Backup01) + "',";
                Sql += "Backup02='" + FilteSQLStr(model.Backup02) + "',";
                Sql += "Backup03='" + FilteSQLStr(model.Backup03) + "',";
                Sql += "Created='" + FilteSQLStr(model.Created) + "',";
                Sql += "CreatedTime=CAST('" + model.CreatedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss") + "' AS DATETIME),";
                Sql += "Modified='" + FilteSQLStr(model.Modified) + "',";
                Sql += "ModifiedTime=CAST('" + model.ModifiedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss") + "' AS DATETIME)";
                if (!string.IsNullOrEmpty(SqlWhere))
                    Sql += " Where " + SqlWhere;
                #endregion
                result.Add(Sql);
            }
            return result;
        }
        #endregion

        #region Organization基础方法
        /// <summary>
        /// 返回Organization字段列表
        /// </summary>
        /// <returns>字段列表</returns>
        private string FieldOrganization()
        {
            return @"
                    [OrgId],
                    [ParentId],
                    [OrgName],
                    [FontColor],
                    [Backup01],
                    [Backup02],
                    [Backup03],
                    [Created],
                    [CreatedTime],
                    [Modified],
                    [ModifiedTime]
                     "
                     .Trim()
                     .Replace("\t", "")
                     .Replace("\r", "")
                     .Replace("\n", "");
        }

        /// <summary>
        /// 读取数据行到model(Organization)
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="dr">数据行</param>
        private Organization ReadDataRow(DataRow dr, Organization model)
        {
            model = new Organization();
            //组织架构编号
            model.OrgId = Convert.IsDBNull(GetDataRow(dr, "OrgId")) ? 0 : Convert.ToInt64(GetDataRow(dr, "OrgId"));
            //上级组织架构编号[为0表示顶级]
            model.ParentId = Convert.IsDBNull(GetDataRow(dr, "ParentId")) ? 0 : Convert.ToInt64(GetDataRow(dr, "ParentId"));
            //组织架构名称
            model.OrgName = Convert.IsDBNull(GetDataRow(dr, "OrgName")) ? string.Empty : Convert.ToString(GetDataRow(dr, "OrgName")).Trim();
            //字体颜色[为空默认色]
            model.FontColor = Convert.IsDBNull(GetDataRow(dr, "FontColor")) ? string.Empty : Convert.ToString(GetDataRow(dr, "FontColor")).Trim();
            //备用字段01
            model.Backup01 = Convert.IsDBNull(GetDataRow(dr, "Backup01")) ? string.Empty : Convert.ToString(GetDataRow(dr, "Backup01")).Trim();
            //备用字段02
            model.Backup02 = Convert.IsDBNull(GetDataRow(dr, "Backup02")) ? string.Empty : Convert.ToString(GetDataRow(dr, "Backup02")).Trim();
            //备用字段03
            model.Backup03 = Convert.IsDBNull(GetDataRow(dr, "Backup03")) ? string.Empty : Convert.ToString(GetDataRow(dr, "Backup03")).Trim();
            //创建人
            model.Created = Convert.IsDBNull(GetDataRow(dr, "Created")) ? string.Empty : Convert.ToString(GetDataRow(dr, "Created")).Trim();
            //创建时间
            model.CreatedTime = Convert.IsDBNull(GetDataRow(dr, "CreatedTime")) ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(GetDataRow(dr, "CreatedTime"));
            //修改人
            model.Modified = Convert.IsDBNull(GetDataRow(dr, "Modified")) ? string.Empty : Convert.ToString(GetDataRow(dr, "Modified")).Trim();
            //修改时间
            model.ModifiedTime = Convert.IsDBNull(GetDataRow(dr, "ModifiedTime")) ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(GetDataRow(dr, "ModifiedTime"));

            return model;
        }

        ///<summary>
        ///检查是否空值(Organization)
        ///</summary>
        private void CheckEmpty(ref List<Organization> lists)
        {
            for (int i = 0; i < lists.Count; i++)
            {
                //组织架构编号
                lists[i].OrgId = lists[i].OrgId == null ? Convert.ToInt64(0) : Convert.ToInt64(lists[i].OrgId);
                //上级组织架构编号[为0表示顶级]
                lists[i].ParentId = lists[i].ParentId == null ? Convert.ToInt64(0) : Convert.ToInt64(lists[i].ParentId);
                //组织架构名称
                lists[i].OrgName = string.IsNullOrEmpty(lists[i].OrgName) ? string.Empty : Convert.ToString(lists[i].OrgName).Trim();
                //字体颜色[为空默认色]
                lists[i].FontColor = string.IsNullOrEmpty(lists[i].FontColor) ? string.Empty : Convert.ToString(lists[i].FontColor).Trim();
                //备用字段01
                lists[i].Backup01 = string.IsNullOrEmpty(lists[i].Backup01) ? string.Empty : Convert.ToString(lists[i].Backup01).Trim();
                //备用字段02
                lists[i].Backup02 = string.IsNullOrEmpty(lists[i].Backup02) ? string.Empty : Convert.ToString(lists[i].Backup02).Trim();
                //备用字段03
                lists[i].Backup03 = string.IsNullOrEmpty(lists[i].Backup03) ? string.Empty : Convert.ToString(lists[i].Backup03).Trim();
                //创建人
                lists[i].Created = string.IsNullOrEmpty(lists[i].Created) ? string.Empty : Convert.ToString(lists[i].Created).Trim();
                //创建时间
                lists[i].CreatedTime = lists[i].CreatedTime == null ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(lists[i].CreatedTime.GetValueOrDefault());
                //修改人
                lists[i].Modified = string.IsNullOrEmpty(lists[i].Modified) ? string.Empty : Convert.ToString(lists[i].Modified).Trim();
                //修改时间
                lists[i].ModifiedTime = lists[i].ModifiedTime == null ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(lists[i].ModifiedTime.GetValueOrDefault());
            }
        }

        ///<summary>
        ///检查是否超过长度(Organization)
        ///</summary>
        ///<param name="lists">数据集</param>
        ///<param name="message">错误消息</param>
        private void CheckMaxLength(ref List<Organization> lists, out string message)
        {
            #region 声明变量

            //错误消息
            message = string.Empty;

            //超过的长度
            int OutLength = 0;
            #endregion

            #region 循环验证长度
            for (int i = 0; i < lists.Count; i++)
            {
                if (!string.IsNullOrEmpty(lists[i].OrgName))
                {
                    if (lists[i].OrgName.Length > 200)
                    {
                        OutLength = lists[i].OrgName.Length - 200;
                        message += "字段名[OrgName]描述[组织架构名称]超长、字段最长[200]实际" + lists[i].OrgName.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].FontColor))
                {
                    if (lists[i].FontColor.Length > 50)
                    {
                        OutLength = lists[i].FontColor.Length - 50;
                        message += "字段名[FontColor]描述[字体颜色[为空默认色]]超长、字段最长[50]实际" + lists[i].FontColor.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Backup01))
                {
                    if (lists[i].Backup01.Length > 95)
                    {
                        OutLength = lists[i].Backup01.Length - 95;
                        message += "字段名[Backup01]描述[备用字段01]超长、字段最长[95]实际" + lists[i].Backup01.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Backup02))
                {
                    if (lists[i].Backup02.Length > 95)
                    {
                        OutLength = lists[i].Backup02.Length - 95;
                        message += "字段名[Backup02]描述[备用字段02]超长、字段最长[95]实际" + lists[i].Backup02.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Backup03))
                {
                    if (lists[i].Backup03.Length > 95)
                    {
                        OutLength = lists[i].Backup03.Length - 95;
                        message += "字段名[Backup03]描述[备用字段03]超长、字段最长[95]实际" + lists[i].Backup03.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Created))
                {
                    if (lists[i].Created.Length > 50)
                    {
                        OutLength = lists[i].Created.Length - 50;
                        message += "字段名[Created]描述[创建人]超长、字段最长[50]实际" + lists[i].Created.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Modified))
                {
                    if (lists[i].Modified.Length > 50)
                    {
                        OutLength = lists[i].Modified.Length - 50;
                        message += "字段名[Modified]描述[修改人]超长、字段最长[50]实际" + lists[i].Modified.Length + "超过长度" + OutLength + ",";
                    }
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(message)) message = message.Substring(0, message.Length - 1);
        }

        ///<summary>
        ///生成插入Sql语句(Organization)
        ///</summary>
        ///<param name="lists">数据List</param>
        ///<returns>插入Sql语句字符串数组</returns>
        private List<string> MarkInsertSql(List<Organization> lists)
        {
            List<string> result = new List<string>();
            foreach (Organization model in lists)
            {
                #region 拼写Sql语句
                string Sql = "insert into Organization(";
                Sql += "OrgId,";
                Sql += "ParentId,";
                Sql += "OrgName,";
                Sql += "FontColor,";
                Sql += "Backup01,";
                Sql += "Backup02,";
                Sql += "Backup03,";
                Sql += "Created,";
                Sql += "CreatedTime,";
                Sql += "Modified,";
                Sql += "ModifiedTime";
                Sql += ") values(";
                Sql += "'" + FilteSQLStr(model.OrgId) + "',";
                Sql += "'" + FilteSQLStr(model.ParentId) + "',";
                Sql += "'" + FilteSQLStr(model.OrgName) + "',";
                Sql += "'" + FilteSQLStr(model.FontColor) + "',";
                Sql += "'" + FilteSQLStr(model.Backup01) + "',";
                Sql += "'" + FilteSQLStr(model.Backup02) + "',";
                Sql += "'" + FilteSQLStr(model.Backup03) + "',";
                Sql += "'" + FilteSQLStr(model.Created) + "',";
                Sql += "CAST('" + model.CreatedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss") + "' AS DATETIME),";
                Sql += "'" + FilteSQLStr(model.Modified) + "',";
                Sql += "CAST('" + model.ModifiedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss") + "' AS DATETIME)";
                Sql += ")";
                #endregion
                result.Add(Sql);
            }
            return result;
        }

        ///<summary>
        ///生成更新Sql语句(Organization)
        ///</summary>
        ///<param name="lists">数据List</param>
        ///<param name="SqlWhere">更新条件</param>
        ///<returns>更新Sql语句字符串数组</returns>
        private List<string> MarkUpdateSql(List<Organization> lists, string SqlWhere)
        {
            List<string> result = new List<string>();
            foreach (Organization model in lists)
            {
                #region 拼写Sql语句
                string Sql = "update Organization set ";
                Sql += "OrgId='" + FilteSQLStr(model.OrgId) + "',";
                Sql += "ParentId='" + FilteSQLStr(model.ParentId) + "',";
                Sql += "OrgName='" + FilteSQLStr(model.OrgName) + "',";
                Sql += "FontColor='" + FilteSQLStr(model.FontColor) + "',";
                Sql += "Backup01='" + FilteSQLStr(model.Backup01) + "',";
                Sql += "Backup02='" + FilteSQLStr(model.Backup02) + "',";
                Sql += "Backup03='" + FilteSQLStr(model.Backup03) + "',";
                Sql += "Created='" + FilteSQLStr(model.Created) + "',";
                Sql += "CreatedTime=CAST('" + model.CreatedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss") + "' AS DATETIME),";
                Sql += "Modified='" + FilteSQLStr(model.Modified) + "',";
                Sql += "ModifiedTime=CAST('" + model.ModifiedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss") + "' AS DATETIME)";
                if (!string.IsNullOrEmpty(SqlWhere))
                    Sql += " Where " + SqlWhere;
                #endregion
                result.Add(Sql);
            }
            return result;
        }
        #endregion

        #region PageTableColumn基础方法
        /// <summary>
        /// 返回PageTableColumn字段列表
        /// </summary>
        /// <returns>字段列表</returns>
        private string FieldPageTableColumn()
        {
            return @"
                    [RecordId],
                    [PageName],
                    [TableName],
                    [FieldName],
                    [ShowName],
                    [OrderSequence],
                    [IsShow],
                    [Backup01],
                    [Backup02],
                    [Backup03],
                    [Created],
                    [CreatedTime],
                    [Modified],
                    [ModifiedTime]
                     "
                     .Trim()
                     .Replace("\t", "")
                     .Replace("\r", "")
                     .Replace("\n", "");
        }

        /// <summary>
        /// 读取数据行到model(PageTableColumn)
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="dr">数据行</param>
        private PageTableColumn ReadDataRow(DataRow dr, PageTableColumn model)
        {
            model = new PageTableColumn();
            //记录编号(雪花ID)
            model.RecordId = GetDataRow(dr,"RecordId")==null ? 0 : Convert.ToInt64(GetDataRow(dr, "RecordId"));
            //页面名称
            model.PageName = GetDataRow(dr, "PageName") == null ? string.Empty : Convert.ToString(dr["PageName"]).Trim();
            //表格编号(GUID)
            model.TableName = Convert.IsDBNull(dr["TableName"]) ? string.Empty : Convert.ToString(dr["TableName"]).Trim();
            //字段名
            model.FieldName = Convert.IsDBNull(dr["FieldName"]) ? string.Empty : Convert.ToString(dr["FieldName"]).Trim();
            //显示名
            model.ShowName = Convert.IsDBNull(dr["ShowName"]) ? string.Empty : Convert.ToString(dr["ShowName"]).Trim();
            //排序顺序
            model.OrderSequence = Convert.IsDBNull(dr["OrderSequence"]) ? 0 : Convert.ToInt32(dr["OrderSequence"]);
            //是否显示[0未显示、1显示]
            model.IsShow = Convert.IsDBNull(dr["IsShow"]) ? 0 : Convert.ToInt32(dr["IsShow"]);
            //备用字段01
            model.Backup01 = Convert.IsDBNull(dr["Backup01"]) ? string.Empty : Convert.ToString(dr["Backup01"]).Trim();
            //备用字段02
            model.Backup02 = Convert.IsDBNull(dr["Backup02"]) ? string.Empty : Convert.ToString(dr["Backup02"]).Trim();
            //备用字段03
            model.Backup03 = Convert.IsDBNull(dr["Backup03"]) ? string.Empty : Convert.ToString(dr["Backup03"]).Trim();
            //创建人
            model.Created = Convert.IsDBNull(dr["Created"]) ? string.Empty : Convert.ToString(dr["Created"]).Trim();
            //创建时间
            model.CreatedTime = Convert.IsDBNull(dr["CreatedTime"]) ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(dr["CreatedTime"]);
            //修改人
            model.Modified = Convert.IsDBNull(dr["Modified"]) ? string.Empty : Convert.ToString(dr["Modified"]).Trim();
            //修改时间
            model.ModifiedTime = Convert.IsDBNull(dr["ModifiedTime"]) ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(dr["ModifiedTime"]);

            return model;
        }

        ///<summary>
        ///检查是否空值(PageTableColumn)
        ///</summary>
        private void CheckEmpty(ref List<PageTableColumn> lists)
        {
            for (int i = 0; i < lists.Count; i++)
            {
                //记录编号(雪花ID)
                lists[i].RecordId = lists[i].RecordId == null ? Convert.ToInt64(0) : Convert.ToInt64(lists[i].RecordId);
                //页面名称
                lists[i].PageName = string.IsNullOrEmpty(lists[i].PageName) ? string.Empty : Convert.ToString(lists[i].PageName).Trim();
                //表格编号(GUID)
                lists[i].TableName = string.IsNullOrEmpty(lists[i].TableName) ? string.Empty : Convert.ToString(lists[i].TableName).Trim();
                //字段名
                lists[i].FieldName = string.IsNullOrEmpty(lists[i].FieldName) ? string.Empty : Convert.ToString(lists[i].FieldName).Trim();
                //显示名
                lists[i].ShowName = string.IsNullOrEmpty(lists[i].ShowName) ? string.Empty : Convert.ToString(lists[i].ShowName).Trim();
                //排序顺序
                lists[i].OrderSequence = lists[i].OrderSequence == null ? Convert.ToInt32(0) : Convert.ToInt32(lists[i].OrderSequence);
                //是否显示[0未显示、1显示]
                lists[i].IsShow = lists[i].IsShow == null ? Convert.ToInt32(0) : Convert.ToInt32(lists[i].IsShow);
                //备用字段01
                lists[i].Backup01 = string.IsNullOrEmpty(lists[i].Backup01) ? string.Empty : Convert.ToString(lists[i].Backup01).Trim();
                //备用字段02
                lists[i].Backup02 = string.IsNullOrEmpty(lists[i].Backup02) ? string.Empty : Convert.ToString(lists[i].Backup02).Trim();
                //备用字段03
                lists[i].Backup03 = string.IsNullOrEmpty(lists[i].Backup03) ? string.Empty : Convert.ToString(lists[i].Backup03).Trim();
                //创建人
                lists[i].Created = string.IsNullOrEmpty(lists[i].Created) ? string.Empty : Convert.ToString(lists[i].Created).Trim();
                //创建时间
                lists[i].CreatedTime = lists[i].CreatedTime == null ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(lists[i].CreatedTime.GetValueOrDefault());
                //修改人
                lists[i].Modified = string.IsNullOrEmpty(lists[i].Modified) ? string.Empty : Convert.ToString(lists[i].Modified).Trim();
                //修改时间
                lists[i].ModifiedTime = lists[i].ModifiedTime == null ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(lists[i].ModifiedTime.GetValueOrDefault());
            }
        }

        ///<summary>
        ///检查是否超过长度(PageTableColumn)
        ///</summary>
        ///<param name="lists">数据集</param>
        ///<param name="message">错误消息</param>
        private void CheckMaxLength(ref List<PageTableColumn> lists, out string message)
        {
            #region 声明变量

            //错误消息
            message = string.Empty;

            //超过的长度
            int OutLength = 0;
            #endregion

            #region 循环验证长度
            for (int i = 0; i < lists.Count; i++)
            {
                if (!string.IsNullOrEmpty(lists[i].PageName))
                {
                    if (lists[i].PageName.Length > 850)
                    {
                        OutLength = lists[i].PageName.Length - 850;
                        message += "字段名[PageName]描述[页面名称]超长、字段最长[850]实际" + lists[i].PageName.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].TableName))
                {
                    if (lists[i].TableName.Length > 50)
                    {
                        OutLength = lists[i].TableName.Length - 50;
                        message += "字段名[TableName]描述[表格编号(GUID)]超长、字段最长[50]实际" + lists[i].TableName.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].FieldName))
                {
                    if (lists[i].FieldName.Length > 90)
                    {
                        OutLength = lists[i].FieldName.Length - 90;
                        message += "字段名[FieldName]描述[字段名]超长、字段最长[90]实际" + lists[i].FieldName.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].ShowName))
                {
                    if (lists[i].ShowName.Length > 90)
                    {
                        OutLength = lists[i].ShowName.Length - 90;
                        message += "字段名[ShowName]描述[显示名]超长、字段最长[90]实际" + lists[i].ShowName.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Backup01))
                {
                    if (lists[i].Backup01.Length > 95)
                    {
                        OutLength = lists[i].Backup01.Length - 95;
                        message += "字段名[Backup01]描述[备用字段01]超长、字段最长[95]实际" + lists[i].Backup01.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Backup02))
                {
                    if (lists[i].Backup02.Length > 95)
                    {
                        OutLength = lists[i].Backup02.Length - 95;
                        message += "字段名[Backup02]描述[备用字段02]超长、字段最长[95]实际" + lists[i].Backup02.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Backup03))
                {
                    if (lists[i].Backup03.Length > 95)
                    {
                        OutLength = lists[i].Backup03.Length - 95;
                        message += "字段名[Backup03]描述[备用字段03]超长、字段最长[95]实际" + lists[i].Backup03.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Created))
                {
                    if (lists[i].Created.Length > 50)
                    {
                        OutLength = lists[i].Created.Length - 50;
                        message += "字段名[Created]描述[创建人]超长、字段最长[50]实际" + lists[i].Created.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Modified))
                {
                    if (lists[i].Modified.Length > 50)
                    {
                        OutLength = lists[i].Modified.Length - 50;
                        message += "字段名[Modified]描述[修改人]超长、字段最长[50]实际" + lists[i].Modified.Length + "超过长度" + OutLength + ",";
                    }
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(message)) message = message.Substring(0, message.Length - 1);
        }

        ///<summary>
        ///生成插入Sql语句(PageTableColumn)
        ///</summary>
        ///<param name="lists">数据List</param>
        ///<returns>插入Sql语句字符串数组</returns>
        private List<string> MarkInsertSql(List<PageTableColumn> lists)
        {
            List<string> result = new List<string>();
            foreach (PageTableColumn model in lists)
            {
                #region 拼写Sql语句
                string Sql = "insert into PageTableColumn(";
                Sql += "RecordId,";
                Sql += "PageName,";
                Sql += "TableName,";
                Sql += "FieldName,";
                Sql += "ShowName,";
                Sql += "OrderSequence,";
                Sql += "IsShow,";
                Sql += "Backup01,";
                Sql += "Backup02,";
                Sql += "Backup03,";
                Sql += "Created,";
                Sql += "CreatedTime,";
                Sql += "Modified,";
                Sql += "ModifiedTime";
                Sql += ") values(";
                Sql += "'" + FilteSQLStr(model.RecordId) + "',";
                Sql += "'" + FilteSQLStr(model.PageName) + "',";
                Sql += "'" + FilteSQLStr(model.TableName) + "',";
                Sql += "'" + FilteSQLStr(model.FieldName) + "',";
                Sql += "'" + FilteSQLStr(model.ShowName) + "',";
                Sql += "'" + FilteSQLStr(model.OrderSequence) + "',";
                Sql += "'" + FilteSQLStr(model.IsShow) + "',";
                Sql += "'" + FilteSQLStr(model.Backup01) + "',";
                Sql += "'" + FilteSQLStr(model.Backup02) + "',";
                Sql += "'" + FilteSQLStr(model.Backup03) + "',";
                Sql += "'" + FilteSQLStr(model.Created) + "',";
                Sql += "CAST('" + model.CreatedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss") + "' AS DATETIME),";
                Sql += "'" + FilteSQLStr(model.Modified) + "',";
                Sql += "CAST('" + model.ModifiedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss") + "' AS DATETIME)";
                Sql += ")";
                #endregion
                result.Add(Sql);
            }
            return result;
        }

        ///<summary>
        ///生成更新Sql语句(PageTableColumn)
        ///</summary>
        ///<param name="lists">数据List</param>
        ///<param name="SqlWhere">更新条件</param>
        ///<returns>更新Sql语句字符串数组</returns>
        private List<string> MarkUpdateSql(List<PageTableColumn> lists, string SqlWhere)
        {
            List<string> result = new List<string>();
            foreach (PageTableColumn model in lists)
            {
                #region 拼写Sql语句
                string Sql = "update PageTableColumn set ";
                Sql += "RecordId='" + FilteSQLStr(model.RecordId) + "',";
                Sql += "PageName='" + FilteSQLStr(model.PageName) + "',";
                Sql += "TableName='" + FilteSQLStr(model.TableName) + "',";
                Sql += "FieldName='" + FilteSQLStr(model.FieldName) + "',";
                Sql += "ShowName='" + FilteSQLStr(model.ShowName) + "',";
                Sql += "OrderSequence='" + FilteSQLStr(model.OrderSequence) + "',";
                Sql += "IsShow='" + FilteSQLStr(model.IsShow) + "',";
                Sql += "Backup01='" + FilteSQLStr(model.Backup01) + "',";
                Sql += "Backup02='" + FilteSQLStr(model.Backup02) + "',";
                Sql += "Backup03='" + FilteSQLStr(model.Backup03) + "',";
                Sql += "Created='" + FilteSQLStr(model.Created) + "',";
                Sql += "CreatedTime=CAST('" + model.CreatedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss") + "' AS DATETIME),";
                Sql += "Modified='" + FilteSQLStr(model.Modified) + "',";
                Sql += "ModifiedTime=CAST('" + model.ModifiedTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss") + "' AS DATETIME)";
                if (!string.IsNullOrEmpty(SqlWhere))
                    Sql += " Where " + SqlWhere;
                #endregion
                result.Add(Sql);
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 过滤不安全的字符串
        /// </summary>
        /// <param name="Str">要过滤的值</param>
        /// <returns>返回结果</returns>
        private static string FilteSQLStr(object str)
        {
            if (str == null)
                return string.Empty;
            if (IsNumeric(str))
                return Convert.ToString(str);
            string Str = Convert.ToString(str);
            if (!string.IsNullOrEmpty(Str))
            {
                Str = Str.Replace("'", "");
                Str = Str.Replace("\"", "");
                Str = Str.Replace("&", "&amp");
                Str = Str.Replace("<", "&lt");
                Str = Str.Replace(">", "&gt");

                Str = Str.Replace("delete", "");
                Str = Str.Replace("update", "");
                Str = Str.Replace("insert", "");
            }
            return Str;
        }

        /// <summary>
        /// 判断object是否数字
        /// </summary>
        /// <param name="AObject">要判断的Object</param>
        /// <returns>是否数字</returns>       
        private static bool IsNumeric(object AObject)
        {
            return AObject is sbyte || AObject is byte ||
                AObject is short || AObject is ushort ||
                AObject is int || AObject is uint ||
                AObject is long || AObject is ulong ||
                AObject is double || AObject is char ||
                AObject is decimal || AObject is float ||
                AObject is double;
        }

        /// <summary>
        /// 获得数据行
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <param name="columnName">列名</param>
        /// <returns>返回值</returns>
        private object GetDataRow(DataRow dr, string columnName)
        {
            object result = null;
            if (dr.Table.Columns.Contains(columnName))
                result = Convert.IsDBNull(dr[columnName]) ? null : dr[columnName];
            else
                result = null;
            return result;
        }

        private static void CreateDBHelper()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            m_connectionString = configuration["ConnectionStrings:MOIConnStr"];
            m_dbHelper = new DbHelper(m_connectionString);
        }
        #endregion
    }
}
