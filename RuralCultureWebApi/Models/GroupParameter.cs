using Npoi.Mapper.Attributes;

namespace RuralCultureWebApi.Models
{
    /// <summary>
    /// 文艺小组表 
    /// </summary>
    public class GroupParameter
    {
        /// <summary>
        ///文艺小组编号
        /// </summary>
        [Column("文艺小组编号")]
        public System.String? GroupId { get; set; }

        /// <summary>
        ///文艺小组
        /// </summary>
        [Column("文艺小组")]
        public System.String? GroupName { get; set; }

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
