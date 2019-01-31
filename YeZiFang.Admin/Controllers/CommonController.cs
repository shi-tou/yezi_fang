using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using YeZiFang.Service;
using YeZiFang.DataContract.Request;
using YeZiFang.DataContract.Response;
using Shitou.Framework.ORM;
using YeZiFang.Model;
using YeZiFang.Application.Models;
using Microsoft.AspNetCore.Authorization;
using YeZiFang.Common;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace YeZiFang.Admin.Controllers
{
    /// <summary>
    /// 公共模块
    /// </summary>
    [Authorize]
    public class CommonController : BaseController
    {
        public ICommonService CommonService { get; set; }
        public CommonController(ICommonService commonService)
        {
            CommonService = commonService;
        }

        /// <summary>
        /// 城市选择页
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult CitySelect()
        {
            List<CityInfo> cityList = CommonService.GetList<CityInfo>();
            List<ProvinceInfo> provinceList = CommonService.GetList<ProvinceInfo>();
            List<SelectCityResponse> selCitys = (from c in cityList
                                                 select new SelectCityResponse
                                                 {
                                                     ID = c.ID,
                                                     CityName = c.CityName,
                                                     ProvinceName = provinceList.FirstOrDefault(a => a.ID == c.ProvinceID).ProvinceName
                                                 }).ToList();
            Dictionary<string, List<SelectCityResponse>> dic = selCitys.GroupBy(g => g.ProvinceName).ToDictionary(m => m.Key, m => m.ToList());
            return View(dic);
        }
        /// <summary>
        /// 城市切换
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CityReplace(string cityId, string cityName)
        {
            var claimIdentity = new ClaimsIdentity("Cookie");
            claimIdentity.AddClaim(new Claim(ClaimTypes.Sid, LoginUserInfo.ID.ToString()));
            claimIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, LoginUserInfo.ID.ToString()));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Name, LoginUserInfo.UserName));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Authentication, JsonConvert.SerializeObject(LoginUserAuthList)));
            claimIdentity.AddClaim(new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(LoginUserInfo)));
            claimIdentity.AddClaim(new Claim("CityId", cityId));
            claimIdentity.AddClaim(new Claim("CityName", cityName));
            var principal = new ClaimsPrincipal(claimIdentity);
            HttpContext.SignInAsync(principal);
            return Json(new { flag = true });
        }
    }
}