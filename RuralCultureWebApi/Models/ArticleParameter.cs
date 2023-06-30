using Npoi.Mapper.Attributes;

namespace RuralCultureWebApi.Models
{
    /// <summary>
    /// 文章参数
    /// </summary>
    public class ArticleParameter
    {
        /// <summary>
        ///文章编号
        /// </summary>
        [Column("文章编号")]
        public System.String? ArticleId { get; set; }

        /// <summary>
        ///文章的标题
        /// </summary>
        [Column("文章的标题")]
        public System.String? ArticleTitle { get; set; }

        /// <summary>
        ///文章的内容[富文本]
        /// </summary>
        [Column("文章的内容")]
        public System.String? Content { get; set; }

        /// <summary>
        ///文艺小组编号
        /// </summary>
        [Column("文艺小组编号")]
        public System.String? GroupId { get; set; }

        /// <summary>
        /// 文艺小组名称
        /// </summary>
        [Column("文艺小组名称")]
        public System.String? GroupName { get; set; }

        /// <summary>
        ///文章作者
        /// </summary>
        [Column("文章作者")]
        public System.String? Author { get; set; }

        /// <summary>
        ///组织架构编号[这篇文章属于哪个组织架构]
        /// </summary>
        [Column("组织架构编号")]
        public System.String? OrgId { get; set; }

        /// <summary>
        /// 组织架构名称
        /// </summary>
        [Column("组织架构名称")]
        public System.String? OrgName { get; set; }

        /// <summary>
        ///是否特色文化展示[0否1是]
        /// </summary>
        [Column("是否特色文化展示")]
        public System.String? IsSpecial { get; set; }

        /// <summary>
        ///是否置顶[0否1是]
        /// </summary>
        [Column("是否置顶")]
        public System.String? IsTop { get; set; }

        /// <summary>
        ///状态[0显示、1隐藏]
        /// </summary>
        [Column("状态")]
        public System.String? State { get; set; }

        /// <summary>
        ///文章类型[0普通文章、1图片文章、2视频文章]
        /// </summary>
        [Column("文章类型")]
        public System.String? ArticleType { get; set; }

        /// <summary>
        ///文章封面[文章必须有封面没有会默认一张封面图]
        /// </summary>
        [Column("文章封面")]
        public System.String? CoverUrl { get; set; }

        /// <summary>
        ///点击量
        /// </summary>
        [Column("点击量")]
        public System.String? ViewCount { get; set; }

        /// <summary>
        ///备用字段01
        /// </summary>
        public System.String? Backup01 { get; set; }

        /// <summary>
        ///备用字段02
        /// </summary>
        public System.String? Backup02 { get; set; }

        /// <summary>
        ///备用字段03
        /// </summary>
        public System.String? Backup03 { get; set; }

        /// <summary>
        ///创建人
        /// </summary>
        public System.String? Created { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        public System.String? CreatedTime { get; set; }

        /// <summary>
        ///修改人
        /// </summary>
        public System.String? Modified { get; set; }

        /// <summary>
        ///修改时间
        /// </summary>
        public System.String? ModifiedTime { get; set; }
    }

    /// <summary>
    /// 文章参数[导出Excel]
    /// </summary>
    public class ArticleExcelParameter
    {
        /// <summary>
        ///文章编号
        /// </summary>
        [Column("文章编号")]
        public System.String? ArticleId { get; set; }

        /// <summary>
        ///文章的标题
        /// </summary>
        [Column("文章的标题")]
        public System.String? ArticleTitle { get; set; }

        /// <summary>
        ///文章的内容[富文本]
        /// </summary>
        [Column("文章的内容")]
        public System.String? Content { get; set; }

        /// <summary>
        ///文艺小组编号
        /// </summary>
        [Column("文艺小组编号")]
        public System.String? GroupId { get; set; }

        /// <summary>
        /// 文艺小组名称
        /// </summary>
        [Column("文艺小组名称")]
        public System.String? GroupName { get; set; }

        /// <summary>
        ///文章作者
        /// </summary>
        [Column("文章作者")]
        public System.String? Author { get; set; }

        /// <summary>
        ///组织架构编号[这篇文章属于哪个组织架构]
        /// </summary>
        [Column("组织架构编号")]
        public System.String? OrgId { get; set; }

        /// <summary>
        /// 组织架构名称
        /// </summary>
        [Column("组织架构名称")]
        public System.String? OrgName { get; set; }

        /// <summary>
        ///是否特色文化展示[0否1是]
        /// </summary>
        [Column("是否特色文化展示")]
        public System.String? IsSpecial { get; set; }

        /// <summary>
        ///是否置顶[0否1是]
        /// </summary>
        [Column("是否置顶")]
        public System.String? IsTop { get; set; }

        /// <summary>
        ///状态[0显示、1隐藏]
        /// </summary>
        [Column("状态")]
        public System.String? State { get; set; }

        /// <summary>
        ///文章类型[0普通文章、1图片文、2视频文章]
        /// </summary>
        [Column("文章类型")]
        public System.String? ArticleType { get; set; }

        /// <summary>
        ///文章封面[文章必须有封面没有会默认一张封面图]
        /// </summary>
        [Column("文章封面")]
        public System.String? CoverUrl { get; set; }

        /// <summary>
        ///点击量
        /// </summary>
        [Column("点击量")]
        public System.String? ViewCount { get; set; }

        /// <summary>
        ///创建人
        /// </summary>
        [Column("创建人")]
        public System.String? Created { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        [Column("创建时间")]
        public System.String? CreatedTime { get; set; }

        /// <summary>
        ///修改人
        /// </summary>
        [Column("修改人")]
        public System.String? Modified { get; set; }

        /// <summary>
        ///修改时间
        /// </summary>
        [Column("修改时间")]
        public System.String? ModifiedTime { get; set; }
    }

    /// <summary>
    /// 文章导入参数
    /// </summary>
    public class ArticleImportParameter
    {
        /// <summary>
        ///文章编号
        /// </summary>
        public System.String? ArticleId { get; set; }

        /// <summary>
        ///文章的标题
        /// </summary>
        [Column("文章的标题")]
        public System.String? ArticleTitle { get; set; }

        /// <summary>
        ///文章的内容[富文本]
        /// </summary>
        [Column("文章的内容")]
        public System.String? Content { get; set; }

        /// <summary>
        ///文艺小组编号
        /// </summary>
        public System.String? GroupId { get; set; }

        /// <summary>
        /// 文艺小组名称
        /// </summary>
        [Column("文艺小组名称")]
        public System.String? GroupName { get; set; }

        /// <summary>
        ///文章作者
        /// </summary>
        [Column("文章作者")]
        public System.String? Author { get; set; }

        /// <summary>
        ///组织架构编号[这篇文章属于哪个组织架构]
        /// </summary>
        public System.String? OrgId { get; set; }

        /// <summary>
        /// 组织架构名称
        /// </summary>
        [Column("组织架构名称")]
        public System.String? OrgName { get; set; }

        /// <summary>
        ///是否特色文化展示[0否1是]
        /// </summary>
        [Column("是否特色文化展示")]
        public System.String? IsSpecial { get; set; }

        /// <summary>
        ///是否置顶[0否1是]
        /// </summary>
        [Column("是否置顶")]
        public System.String? IsTop { get; set; }

        /// <summary>
        ///状态[0显示、1隐藏]
        /// </summary>
        [Column("状态")]
        public System.String? State { get; set; }

        /// <summary>
        ///文章类型[0普通文章、1图片文、2视频文章]
        /// </summary>
        public System.String? ArticleType { get; set; }

        /// <summary>
        ///文章封面[文章必须有封面没有会默认一张封面图]
        /// </summary>
        public System.String? CoverUrl { get; set; }

        /// <summary>
        ///点击量
        /// </summary>
        public System.String? ViewCount { get; set; }

        /// <summary>
        ///备用字段01
        /// </summary>
        public System.String? Backup01 { get; set; }

        /// <summary>
        ///备用字段02
        /// </summary>
        public System.String? Backup02 { get; set; }

        /// <summary>
        ///备用字段03
        /// </summary>
        public System.String? Backup03 { get; set; }

        /// <summary>
        ///创建人
        /// </summary>
        public System.String? Created { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        public System.String? CreatedTime { get; set; }

        /// <summary>
        ///修改人
        /// </summary>
        public System.String? Modified { get; set; }

        /// <summary>
        ///修改时间
        /// </summary>
        public System.String? ModifiedTime { get; set; }
    }
}
