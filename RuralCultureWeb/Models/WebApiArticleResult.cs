namespace RuralCultureWeb.Models
{
    /// <summary>
    /// webapi返回值
    /// </summary>
    public class WebApiResult 
    {
        /// <summary>
        /// Api状态返回值
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string msg { get; set; }
    }

    /// <summary>
    /// WebApi文章返回值
    /// </summary>
    public class WebApiArticleResultRoot
    {
        /// <summary>
        /// Api状态返回值
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public List<WebApiArticleResultItem> result { get; set; }
    }

    /// <summary>
    /// 文章WebApi返回项
    /// </summary>
    public class WebApiArticleResultItem
    {
        /// <summary>
        /// 文章编号
        /// </summary>
        public string articleId { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        public string articleTitle { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 文艺小组编号
        /// </summary>
        public string groupId { get; set; }

        /// <summary>
        /// 文艺小组名称
        /// </summary>
        public string groupName { get; set; }
        
        /// <summary>
        /// 文章作者
        /// </summary>
        public string author { get; set; }

        /// <summary>
        /// 组织架构编号
        /// </summary>
        public string orgId { get; set; }

        /// <summary>
        /// 组织架构名称
        /// </summary>
        public string orgName { get; set; }

        /// <summary>
        ///是否特色文化展示[0否1是]
        /// </summary>
        public string isSpecial { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public string isTop { get; set; }

        /// <summary>
        ///状态[0显示、1隐藏]
        /// </summary>
        public string state { get; set; }

        /// <summary>
        ///文章类型[0普通文章、1图片文、2视频文章]
        /// </summary>
        public string articleType { get; set; }

        /// <summary>
        ///文章封面[文章必须有封面没有会默认一张封面图]
        /// </summary>
        public string coverUrl { get; set; }

        /// <summary>
        ///点击量
        /// </summary>
        public string viewCount { get; set; }

        /// <summary>
        ///备用字段01
        /// </summary>
        public string backup01 { get; set; }

        /// <summary>
        ///备用字段02
        /// </summary>
        public string backup02 { get; set; }

        /// <summary>
        ///备用字段03
        /// </summary>
        public string backup03 { get; set; }

        /// <summary>
        ///创建人
        /// </summary>
        public string created { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        public string createdTime { get; set; }

        /// <summary>
        ///修改人
        /// </summary>
        public string modified { get; set; }

        /// <summary>
        ///修改时间
        /// </summary>
        public string modifiedTime { get; set; }
    }
}
