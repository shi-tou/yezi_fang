
using System;
using System.Collections.Generic;
using System.Text;
using YeZiFang.Model;

namespace YeZiFang.DataContract.Response
{
    public class SelectCityResponse : CityInfo
    {
        /// <summary>
        /// 省名称
        /// </summary>
        public string ProvinceName { get; set; }
    }
}
