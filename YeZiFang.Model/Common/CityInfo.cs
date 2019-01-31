using Shitou.Framework.ORM.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YeZiFang.Model
{
    /// <summary>
    /// 城市信息
    /// </summary>
    [Table("T_City")]
    public class CityInfo
    {
        public int ID { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 拼音
        /// </summary>
        public string Spell { get; set; }
        /// <summary>
        /// 首字母
        /// </summary>
        public string FirstLetter { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// 所属省
        /// </summary>
        public int ProvinceID { get; set; }
        /// 热门城市
        /// </summary>
        public bool IsHot { get; set; }
        /// <summary>
        /// 是否开放
        /// </summary>
        public bool IsOpen { get; set; }

    }
}
