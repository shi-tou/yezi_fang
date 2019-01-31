using Shitou.Framework.ORM.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YeZiFang.Model
{
    /// <summary>
    /// 省份信息
    /// </summary>
    [Table("T_Province")]
    public class ProvinceInfo
    {
        public int ID { get; set; }
        /// <summary>
        /// 省名称
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 拼音
        /// </summary>
        public string Spell { get; set; }
        /// <summary>
        /// 首字母
        /// </summary>
        public string FirstLetter { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        public string ShorName { get; set; }
    }
}
