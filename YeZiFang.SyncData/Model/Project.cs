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
    public class Project
    {
        [BsonId]
        public ObjectId _id { get; set; }
        /// <summary>
        /// 楼盘id
        /// </summary>
        [BsonElement("project_id")]
        public string ProjectId { get; set; }
        /// <summary>
        /// 楼盘名称
        /// </summary>
        [BsonElement("project_name")]
        public string ProjectName { get; set; }
        /// <summary>
        /// 均价
        /// </summary>
        [BsonElement("project_avg_price")]
        public string ProjectAvgPrice { get; set; }
        /// <summary>
        /// 开发商
        /// </summary>
        [BsonElement("project_developer")]
        public string ProjectDeveloper { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [BsonElement("project_address")]
        public string ProjectAddress { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        [BsonElement("province_id")]
        public int ProvinceId { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        [BsonElement("city_id")]
        public int CityId { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        [BsonElement("city_name")]
        public string CityName { get; set; }
        /// <summary>
        /// 行政区
        /// </summary>
        [BsonElement("area_id")]
        public int AreaId { get; set; }
        /// <summary>
        /// 行政区
        /// </summary>
        [BsonElement("area_name")]
        public string AreaName { get; set; }
    }
}
