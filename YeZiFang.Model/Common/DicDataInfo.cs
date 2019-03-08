using Shitou.Framework.ORM.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace YeZiFang.Model.Common
{
    [Table("t_dictdata")]
    public class DicDataInfo
    {
        public int ID { get; set; }
        /// <summary>
        /// 父级id
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 字典key
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 字典值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
