using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using YeZiFang.App.Models;

namespace YeZiFang.App.Models
{
    /// <summary>
    /// 楼盘
    /// </summary>
    [Collection("project")]
    public class ProjectModel
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string project_id { get; set; }
        public string project_name { get; set; }
        public string feature { get; set; }
        public string purpose { get; set; }
        public decimal avg_price { get; set; }
        public string fitment { get; set; }
        public string developer { get; set; }
        public string property_company { get; set; }
        public int city_id { get; set; }
        public string area { get; set; }
        public string address { get; set; }
        public decimal lng { get; set; }
        public decimal lat { get; set; }
        public int total_house_count { get; set; }
        public decimal land_area { get; set; }
        public decimal building_area { get; set; }
        public decimal green_rate { get; set; }
        public decimal plot_rate { get; set; }
    }
}
