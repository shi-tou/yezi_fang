using Shitou.Framework.ORM.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YeZiFang.Model
{
    /// <summary>
    /// 地区信息
    /// </summary>
    [Table("T_Area")]
    public class AreaInfo
    {
        public string ID { get; set; }
        /// <summary>
        /// 地区名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        public int CityID { get; set; }
    }
}
