

using Npoi.Mapper.Attributes;

namespace RuralCultureWebApi.Models
{
    
    /// <summary>
    /// 返回组织架构树形数据
    /// </summary>
    public class ResultOrganizationTreeData 
    {

        /// <summary>
        ///组织架构编号
        /// </summary>
        public System.String? OrgId { get; set; }

        /// <summary>
        ///组织架构名称
        /// </summary>
        public System.String? OrgName { get; set; }

        /// <summary>
        ///字体颜色[为空默认色]
        /// </summary>
        public System.String? FontColor { get; set; }

        /// <summary>
        /// 组织架构图固定值
        /// </summary>
        public System.String Relationship { get; set; } = "001";

        /// <summary>
        /// 子节点
        /// </summary>
        public List<ResultOrganizationTreeData>? Children { get; set; }
    }

    /// <summary>
    /// 组织架构表 
    /// </summary>
    public class OrganizationParameter
    {
        /// <summary>
        ///组织架构编号
        /// </summary>
        [Column("组织架构编号")]
        public System.String? OrgId { get; set; }

        /// <summary>
        ///上级组织架构编号[为0表示顶级]
        /// </summary>
        [Column("上级组织架构编号")]
        public System.String? ParentId { get; set; }

        /// <summary>
        ///组织架构名称
        /// </summary>
        [Column("组织架构名称")]
        public System.String? OrgName { get; set; }

        /// <summary>
        ///字体颜色[为空默认色]
        /// </summary>
        [Column("字体颜色")]
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
}
