﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuralCultureDataAccess.Models
{
    /// <summary>
    /// 页面表格字段列表 
    /// </summary>
    [Serializable]
    public partial class PageTableColumn
    {

        /// <summary>
        ///记录编号(雪花ID)
        /// </summary>
        public System.Int64? RecordId { get; set; }

        /// <summary>
        ///页面名称
        /// </summary>
        public System.String? PageName { get; set; }

        /// <summary>
        ///表格编号(GUID)
        /// </summary>
        public System.String? TableName { get; set; }

        /// <summary>
        ///字段名
        /// </summary>
        public System.String? FieldName { get; set; }

        /// <summary>
        ///显示名
        /// </summary>
        public System.String? ShowName { get; set; }

        /// <summary>
        ///排序顺序
        /// </summary>
        public System.Int32? OrderSequence { get; set; }

        /// <summary>
        ///是否显示[0未显示、1显示]
        /// </summary>
        public System.Int32? IsShow { get; set; }

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
