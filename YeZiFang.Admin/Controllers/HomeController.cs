﻿
using YeZiFang.Application.Model;
using YeZiFang.Application.Models;
using YeZiFang.Model;
using YeZiFang.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace YeZiFang.Admin.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ISystemService SystemService { get; set; }
        public HomeController(ISystemService systemService)
        {
            SystemService = systemService;
        }
        public IActionResult Index()
        {
            //登录人为空
            if (LoginUserInfo == null)
                return View();
            ViewBag.RealName = LoginUserInfo.RealName;
            ViewBag.UserID = LoginUserInfo.ID;
            ViewBag.CityName = CityName;
           
            //权限为空
            if (LoginUserAuthList == null)
                return View();

            //创建左侧菜单
            var listP1 = from p in LoginUserAuthList
                         where p.AuthType == (int)AuthType.Module
                         orderby p.Sort
                         select p;
            StringBuilder sb = new StringBuilder();

            foreach (AuthInfo p1 in listP1)
            {
                if (sb.Length == 0)//第一个菜单展开
                {
                    sb.Append("<li class=\"layui-nav-item layui-nav-itemed\">");
                }
                else
                {
                    sb.Append("<li class=\"layui-nav-item\">");
                }
                sb.Append(string.Format("<a class=\"\" href=\"javascript:; \">{0}&nbsp;&nbsp;{1}</a>",
                    string.IsNullOrEmpty(p1.IconClass) ? "" : string.Format("<i class=\"layui-icon {0}\"></i>", p1.IconClass),
                    p1.AuthName));

                sb.Append("<dl class=\"layui-nav-child\">");
                var listP2 = from p in LoginUserAuthList
                             where p.AuthType == (int)AuthType.Page && p.ParentID == p1.ID
                             orderby p.Sort
                             select p;
                foreach (AuthInfo p2 in listP2)
                {
                    sb.Append(string.Format("<dd><a href = \"javascript:;\" lay-id=\"{0}\" lay-url=\"{1}\">{2}</a></dd>", p2.ID, p2.Url, p2.AuthName));
                }
                sb.Append("</dl>");
                sb.Append("</li>");
            }
            ViewBag.MenuList = sb.ToString();
            return View();
        }

        public IActionResult NoAuthTip()
        {
            return View();
        }
        public IActionResult Welcome()
        {
            return View();
        }
        /// <summary>
        /// 错误页
        /// </summary>
        /// <param name="errorViewModel"></param>
        /// <returns></returns>
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        /// <summary>
        /// 404
        /// </summary>
        /// <param name="errorViewModel"></param>
        /// <returns></returns>
        public IActionResult NoFound()
        {
            return View();
        }
    }
}