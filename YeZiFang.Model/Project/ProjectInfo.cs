using Shitou.Framework.ORM.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YeZiFang.Model
{
    /// <summary>
    /// 楼盘信息
    /// </summary>
    [Table("T_Project")]
    public class ProjectInfo
    {
        public string ID { get; set; }
        /// <summary>
        /// 楼盘名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        public string ProjectOtherName { get; set; }
        /// <summary>
        /// 特色
        /// </summary>
        public string Feature { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public int Purpose { get; set; }
        /// <summary>
        /// 均价
        /// </summary>
        public decimal AvgPrice { get; set; }
        /// <summary>
        /// 装修
        /// </summary>
        public int Fitment { get; set; }
        /// <summary>
        /// 开发商
        /// </summary>
        public string Developer { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public int CityId { get; set; }
        /// 区
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal Latitude { get; set; }
        /// <summary>
        /// 使用年限
        /// </summary>
        public int UsefulLife { get; set; }
        /// <summary>
        /// 总套数
        /// </summary>
        public int TotalHouseCount { get; set; }
        /// <summary>
        /// 占地面积
        /// </summary>
        public decimal LandArea { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public decimal BuildingArea { get; set; }
        /// <summary>
        /// 绿化率
        /// </summary>
        public decimal GreenRate { get; set; }
        /// <summary>
        /// 容积率
        /// </summary>
        public decimal PlotRate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

    }
}
