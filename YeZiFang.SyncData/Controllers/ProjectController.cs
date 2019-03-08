using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shitou.Framework.ORM;
using YeZiFang.Common;
using YeZiFang.DataContract.Base;
using YeZiFang.Model;
using YeZiFang.Model.Common;
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
        /// <param name="adoTemplate"></param>
        public ProjectController(IMongoService mongoService, IAdoTemplate adoTemplate)
        {
            _mongoService = mongoService;
            _adoTemplate = adoTemplate;
        }

        /// <summary>
        /// 楼盘同步
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> Sync()
        {
            try
            {
                List<DicDataInfo> dicData = _adoTemplate.GetList<DicDataInfo>();
                List<DicDataInfo> purposeData = dicData.Where(a => a.ParentId == Consts.Purpose).ToList();
                List<DicDataInfo> fitmentData = dicData.Where(a => a.ParentId == Consts.Fitment).ToList();
                List<CityInfo> cityData= _adoTemplate.GetList<CityInfo>();
                List<AreaInfo> areaData = _adoTemplate.GetList<AreaInfo>();
                List<ProjectModel> listSource = _mongoService.List<ProjectModel>();
                List<ProjectInfo> listTarget = new List<ProjectInfo>();
                int index = 0;
                listSource.ForEach(m =>
                {
                    index++;
                    listTarget.Add(new ProjectInfo
                    {
                        ID = StringUtils.GenerateUniqueID(),
                        ProjectName = m.project_name,
                        AvgPrice = m.avg_price,
                        Feature = m.feature,
                        Purpose = GetPurposeId(m.purpose,purposeData),
                        Fitment = GetFitmentId(m.fitment,fitmentData),
                        Developer = m.developer,
                        ProvinceId = cityData.FirstOrDefault(a=>a.ID==m.city_id).ProvinceID,
                        CityId = m.city_id,
                        AreaId = GetAreaId(m.area,areaData),
                        Address = m.address,
                        Longitude = m.lng,
                        Latitude = m.lat,
                        UsefulLife = 0,
                        TotalHouseCount = m.total_house_count,
                        LandArea = m.land_area,
                        BuildingArea = m.building_area,
                        GreenRate = m.green_rate,
                        PlotRate = m.plot_rate,
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
                });
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 获取用途id
        /// </summary>
        /// <param name="purpose"></param>
        /// <param name="purposeData"></param>
        /// <returns></returns>
        [NonAction]
        private int GetPurposeId(string purpose, List<DicDataInfo> purposeData)
        {
            try
            {
                if (string.IsNullOrEmpty(purpose))
                {
                    return 0;
                }
                string[] p = purpose.Split(',');
                if (p.Length == 0)
                    return 0;
                DicDataInfo d = purposeData.FirstOrDefault(a => a.Name == p[0]);
                return d == null ? 0 : int.Parse(d.Value);
            }
            catch(Exception ex)
            {

                return 0;
            }
        }
        /// <summary>
        /// 获取装修id
        /// </summary>
        /// <param name="fitment"></param>
        /// <param name="fitmentData"></param>
        /// <returns></returns>
        [NonAction]
        private int GetFitmentId(string fitment, List<DicDataInfo> fitmentData)
        {
            try
            {
                if (string.IsNullOrEmpty(fitment))
                {
                    return 0;
                }
                string[] f = fitment.Split(',');
                if (f.Length == 0)
                    return 0;
                DicDataInfo d = fitmentData.FirstOrDefault(a => a.Name == f[0]);
                return d == null ? 0 : int.Parse(d.Value);
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
        /// <summary>
        /// 获取装修id
        /// </summary>
        /// <param name="area"></param>
        /// <param name="areaData"></param>
        /// <returns></returns>
        [NonAction]
        private int GetAreaId(string area, List<AreaInfo> areaData)
        {
            try
            {
                if (string.IsNullOrEmpty(area))
                {
                    return 0;
                }

                AreaInfo d = areaData.FirstOrDefault(a => a.AreaName.Contains(area));
                return d == null ? 0 : d.ID;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
    }
}
