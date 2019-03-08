using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YeZiFang.SyncData.Model
{
    /// <summary>
    /// 楼盘
    /// </summary>
    [Collection("project")]
    public class ProjectModel
    {
        [BsonId]
        public ObjectId _id { get; set; }
        /// <summary>
        /// 楼盘id
        /// </summary>
        public string project_id { get; set; }
        /// <summary>
        /// 楼盘名称
        /// </summary>
        public string project_name { get; set; }
        /// <summary>
        /// 特色
        /// </summary>
        public string feature { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public string purpose { get; set; }
        /// <summary>
        /// 均价
        /// </summary>
        public decimal avg_price { get; set; }
        /// <summary>
        /// 装修
        /// </summary>
        public string fitment { get; set; }
        /// <summary>
        /// 开发商
        /// </summary>
        public string developer { get; set; }
        /// <summary>
        /// 物业公司
        /// </summary>
        public string property_company { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public int city_id { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string area { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal lng { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal lat { get; set; }
        /// <summary>
        /// 总套数
        /// </summary>
        public int total_house_count { get; set; }
        /// <summary>
        /// 土地面积
        /// </summary>
        public decimal land_area { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public decimal building_area { get; set; }
        /// <summary>
        /// 绿化率
        /// </summary>
        public decimal green_rate { get; set; }
        /// <summary>
        /// 容积率
        /// </summary>
        public decimal plot_rate { get; set; }
    }
}
