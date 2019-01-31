using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shitou.Framework.ORM;
using YeZiFang.Common;
using YeZiFang.Model;
using YeZiFang.SyncData.Model;
using YeZiFang.SyncData.Service;

namespace YeZiFang.SyncData.Controllers
{
    /// <summary>
    /// 楼盘同步
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IMongoService _mongoService;
        private IAdoTemplate _adoTemplate;
        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="mongoService"></param>
        public ProjectController(IMongoService mongoService, IAdoTemplate adoTemplate)
        {
            _mongoService = mongoService;
            _adoTemplate = adoTemplate;
        }

        /// <summary>
        /// 楼盘同步
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        [HttpGet("{cityId}")]
        public ActionResult<string> Sync(int cityId)
        {
            try
            {
                Hashtable hs = new Hashtable();
                hs.Add("city_id", cityId);
                List<Project> listSource = _mongoService.List<Project>(hs);
                List<ProjectInfo> listTarget = new List<ProjectInfo>();
                int index = 0;
                listSource.ForEach((Action<Project>)(m =>
                {
                    index++;
                    listTarget.Add(new ProjectInfo
                    {
                        ID = StringUtils.GenerateUniqueID(),
                        ProjectName = m.ProjectName,
                        ProjectDeveloper = m.ProjectDeveloper,
                        ProvinceId = m.ProvinceId,
                        CityId = m.CityId,
                        AreaId = m.AreaId,
                        Address = m.ProjectAddress,
                        AvgPrice = string.IsNullOrEmpty(m.ProjectAvgPrice) ? 0 : decimal.Parse(m.ProjectAvgPrice),
                        CreateBy = "system",
                        CreateTime = DateTime.Now,
                        UpdateBy = "system",
                        UpdateTime = DateTime.Now
                    });
                    if (index % 50 == 0 || index == listSource.Count())
                    {
                        _adoTemplate.Insert(listTarget);
                        listTarget.Clear();
                    }
                }));
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
