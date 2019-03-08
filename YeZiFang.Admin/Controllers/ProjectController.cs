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
using YeZiFang.Model.Common;
using YeZiFang.DataContract.Base;

namespace YeZiFang.Admin.Controllers
{
    /// <summary>
    /// 楼盘模块
    /// </summary>
    [Authorize]
    public class ProjectController : BaseController
    {
        public IProjectService ProjectService { get; set; }
        public ProjectController(IProjectService projectService)
        {
            ProjectService = projectService;
        }

        #region 楼盘管理
        /// <summary>
        /// 楼盘列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult ProjectList(GetProjectListRequest request)
        {
            //request.CityId = CityId;
            Pager<GetProjectListResponse> list = ProjectService.GetProjectList(request);
            return View(list);
        }
        /// <summary>
        /// 楼盘添加
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectAdd()
        {
            ViewData["AreaData"] = new SelectList(ProjectService.GetList<AreaInfo>("CityId", CityId), "ID", "AreaName");
            return View(new ProjectInfo());
        }
        /// <summary>
        /// 楼盘添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ProjectAdd(ProjectInfo info)
        {
            CityInfo cityInfo = ProjectService.GetModel<CityInfo>("ID", CityId);
            info.ID = StringUtils.GenerateUniqueID();
            info.CreateBy = LoginUserInfo.ID;
            info.CreateTime = DateTime.Now;
            info.ProvinceId = cityInfo.ID;
            if (ProjectService.Insert(info))
            {
                Result.IsOk = true;
                Result.Msg = "添加成功";
            }
            else
            {
                Result.IsOk = false;
                Result.Msg = "添加失败";
            }
            return Json(Result);
        }
        /// <summary>
        /// 楼盘添加
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectEdit(string id)
        {
            ProjectInfo info = ProjectService.GetModel<ProjectInfo>("ID", id);
            List<DicDataInfo> dicData = ProjectService.GetList<DicDataInfo>();
            ViewData["PurposeData"] = new SelectList(dicData.Where(a => a.ParentId == Consts.Purpose).ToList(), "Value", "Name");
            ViewData["FitmentData"] = new SelectList(dicData.Where(a => a.ParentId == Consts.Fitment).ToList(), "Value", "Name");
            ViewData["ProvinceData"] = new SelectList(ProjectService.GetList<ProvinceInfo>(), "ID", "ProvinceName");
            ViewData["CityData"] = new SelectList(ProjectService.GetList<CityInfo>("ProvinceId", info.ProvinceId), "ID", "CityName");
            ViewData["AreaData"] = new SelectList(ProjectService.GetList<AreaInfo>("CityId", info.CityId), "ID", "AreaName");
            
            return View(info);
        }
        /// <summary>
        /// 楼盘添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ProjectEdit(ProjectInfo info)
        {
            if (ProjectService.Update(info))
            {
                Result.IsOk = true;
                Result.Msg = "更新成功";
            }
            else
            {
                Result.IsOk = false;
                Result.Msg = "更新失败";
            }
            return Json(Result);
        }
        #endregion

    }
}