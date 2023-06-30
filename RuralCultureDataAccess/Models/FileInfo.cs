using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuralCultureDataAccess.Models
{
    /// <summary>
    /// 文件表 
    /// </summary>
    [Serializable]
    public partial class FileInfo
    {

        /// <summary>
        ///文件编号
        /// </summary>
        public System.Int64? FileId { get; set; }

        /// <summary>
        ///文件名
        /// </summary>
        public System.String? FileName { get; set; }

        /// <summary>
        ///原始文件名
        /// </summary>
        public System.String? SrcFileName { get; set; }

        /// <summary>
        ///URL地址
        /// </summary>
        public System.String? FileUrl { get; set; }

        /// <summary>
        ///文件类型[只允许word、excel、ppt、pdf和mp4]
        /// </summary>
        public System.String? FileType { get; set; }

        /// <summary>
        ///文件大小
        /// </summary>
        public System.Double? FileSize { get; set; }

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
    }
}
