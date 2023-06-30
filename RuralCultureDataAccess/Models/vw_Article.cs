using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuralCultureDataAccess.Models
{
    /// <summary>
    /// 文章视图
    /// </summary>
    [Serializable]
    public class vw_Article
    {


        /// <summary>
        /// 文章编号
        /// </summary>
        public long ArticleId { get; set; }


        /// <summary>
        /// 文章标题
        /// </summary>
        public string ArticleTitle { get; set; }


        /// <summary>
        /// 文章内容
        /// </summary>
        public string Content { get; set; }


        /// <summary>
        /// 文章作者
        /// </summary>
        public string Author { get; set; }


        /// <summary>
        /// 文艺小组名称
        /// </summary>
        public string GroupName { get; set; }


        /// <summary>
        /// 是否特色文化展示
        /// </summary>
        public int IsSpecial { get; set; }


        /// <summary>
        /// 是否置顶
        /// </summary>
        public int IsTop { get; set; }


        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }


        /// <summary>
        /// 文章类型
        /// </summary>
        public int ArticleType { get; set; }


        /// <summary>
        /// 文章封面
        /// </summary>
        public string CoverUrl { get; set; }


        /// <summary>
        /// 点击量
        /// </summary>
        public int ViewCount { get; set; }


        /// <summary>
        /// 备用字段01
        /// </summary>
        public string Backup01 { get; set; }


        /// <summary>
        /// 备用字段02
        /// </summary>
        public string Backup02 { get; set; }


        /// <summary>
        /// 备用字段03
        /// </summary>
        public string Backup03 { get; set; }


        /// <summary>
        /// 创建人
        /// </summary>
        public string Created { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }


        /// <summary>
        /// 修改人
        /// </summary>
        public string Modified { get; set; }


        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedTime { get; set; }


        /// <summary>
        /// 组织架构名称
        /// </summary>
        public string OrgName { get; set; }


        /// <summary>
        /// 字体颜色
        /// </summary>
        public string FontColor { get; set; }


        /// <summary>
        /// 文艺小组编号
        /// </summary>
        public long GroupId { get; set; }


        /// <summary>
        /// 组织架构编号
        /// </summary>
        public long OrgId { get; set; }

    }
}
