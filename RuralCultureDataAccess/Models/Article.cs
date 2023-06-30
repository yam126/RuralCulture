using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuralCultureDataAccess.Models
{
    /// <summary>
    /// 文章表 
    /// </summary>
    [Serializable]
    public partial class Article
    {
        /// <summary>
        ///文章编号
        /// </summary>
        public System.Int64? ArticleId { get; set; }

        /// <summary>
        ///文章的标题
        /// </summary>
        public System.String? ArticleTitle { get; set; }

        /// <summary>
        ///文章的内容[富文本]
        /// </summary>
        public System.String? Content { get; set; }

        /// <summary>
        ///文艺小组编号
        /// </summary>
        public System.Int64? GroupId { get; set; }

        /// <summary>
        ///文章作者
        /// </summary>
        public System.String? Author { get; set; }

        /// <summary>
        ///组织架构编号[这篇文章属于哪个组织架构]
        /// </summary>
        public System.Int64? OrgId { get; set; }

        /// <summary>
        ///是否特色文化展示[0否1是]
        /// </summary>
        public System.Int32? IsSpecial { get; set; }

        /// <summary>
        ///是否置顶[0否1是]
        /// </summary>
        public System.Int32? IsTop { get; set; }

        /// <summary>
        ///状态[0显示、1隐藏]
        /// </summary>
        public System.Int32? State { get; set; }

        /// <summary>
        ///文章类型[0普通文章、1图片文、2视频文章]
        /// </summary>
        public System.Int32? ArticleType { get; set; }

        /// <summary>
        ///文章封面[文章必须有封面没有会默认一张封面图]
        /// </summary>
        public System.String? CoverUrl { get; set; }

        /// <summary>
        ///点击量
        /// </summary>
        public System.Int32? ViewCount { get; set; }

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
        public System.DateTime? CreatedTime { get; set; }

        /// <summary>
        ///修改人
        /// </summary>
        public System.String? Modified { get; set; }

        /// <summary>
        ///修改时间
        /// </summary>
        public System.DateTime? ModifiedTime { get; set; }
    }
}
