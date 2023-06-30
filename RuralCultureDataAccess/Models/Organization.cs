using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuralCultureDataAccess.Models
{
    /// <summary>
    /// 组织架构表 
    /// </summary>
    [Serializable]
    public partial class Organization
    {

        /// <summary>
        ///组织架构编号
        /// </summary>
        public System.Int64? OrgId { get; set; }

        /// <summary>
        ///上级组织架构编号[为0表示顶级]
        /// </summary>
        public System.Int64? ParentId { get; set; }

        /// <summary>
        ///组织架构名称
        /// </summary>
        public System.String? OrgName { get; set; }

        /// <summary>
        ///字体颜色[为空默认色]
        /// </summary>
        public System.String? FontColor { get; set; }

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
