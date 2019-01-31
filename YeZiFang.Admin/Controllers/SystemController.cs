﻿using Newtonsoft.Json;
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

namespace YeZiFang.Admin.Controllers
{
    /// <summary>
    /// 用户模块
    /// </summary>
    [Authorize]
    public class SystemController : BaseController
    {
        public ISystemService SystemService { get; set; }
        public SystemController(ISystemService systemService)
        {
            SystemService = systemService;
        }

        #region 用户管理
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult UserList(GetUserListRequest request)
        {
            Pager<GetUserListResponse> list = SystemService.GetUserList(request);
            return View(list);
        }
        /// <summary>
        /// 用户添加
        /// </summary>
        /// <returns></returns>
        public ActionResult UserAdd()
        {
            ViewData["Roles"] = new SelectList(SystemService.GetList<RoleInfo>(), "ID", "RoleName");
            return View(new UserInfo());
        }
        /// <summary>
        /// 用户添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserAdd(UserInfo info, IFormCollection form)
        {
            info.IsAdmin = form["IsAdmin"].ToString().ToLower() == "on" ? true : false;

            info.ID = StringUtils.GenerateUniqueID();
            info.Password = RSADEncrypt.Encrypt(info.Password);
            info.CreateBy = LoginUserInfo.ID;
            info.CreateTime = DateTime.Now;

            if (SystemService.Insert(info))
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
        /// 用户添加
        /// </summary>
        /// <returns></returns>
        public ActionResult UserEdit(string id)
        {
            ViewData["Roles"] = new SelectList(SystemService.GetList<RoleInfo>(), "ID", "RoleName");
            UserInfo info = SystemService.GetModel<UserInfo>("ID", id);
            return View(info);
        }
        /// <summary>
        /// 用户添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserEdit(UserInfo info, IFormCollection form)
        {
            info.IsAdmin = form["IsAdmin"].ToString().ToLower() == "on" ? true : false;
            if (SystemService.Update(info))
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
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult UserDelete(string id)
        {
            if (SystemService.Delete<UserInfo>("ID", id))
            {
                Result.IsOk = true;
                Result.Msg = "删除成功";
            }
            else
            {
                Result.IsOk = false;
                Result.Msg = "删除失败";
            }
            return Json(Result);
        }
        /// <summary>
        /// 用户修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult UserPwd(string id)
        {
            UserInfo info = SystemService.GetModel<UserInfo>("ID", id);
            return View(info ?? new UserInfo());
        }
        /// <summary>
        /// 用户修改密码
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserPwd(UserInfo info)
        {
            UserInfo user = SystemService.GetModel<UserInfo>("ID", info.ID);
            user.Password= RSADEncrypt.Encrypt(info.Password);
            if (SystemService.Update<UserInfo>(user))
            {
                Result.IsOk = true;
                Result.Msg = "修改成功";
            }
            else
            {
                Result.IsOk = false;
                Result.Msg = "修改失败";
            }
            return Json(Result);
        }
        #endregion

        #region 角色管理
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult RoleList(GetRoleListRequest request)
        {
            Pager<GetRoleListResponse> list = SystemService.GetRoleList(request);
            return View(list);
        }
        /// <summary>
        /// 角色添加
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleAdd()
        {
            RoleInfo info = new RoleInfo();
            //获取所有权限
            List<AuthInfo> allPermission = SystemService.GetList<AuthInfo>();
            //获取权限
            var ztree = from p in allPermission
                        select new ZTreeData
                        {
                            id = p.ID,
                            pId = p.ParentID,
                            name = p.AuthName,
                            value = p.ID,
                            Checked = false
                        };
            //序列化为ztree所需要的json串
            ViewBag.PermissionList = JsonConvert.SerializeObject(ztree);
            //构造隐藏域的值
            ViewBag.PermissionIDs = "";
            return View(info ?? new RoleInfo());
        }
        /// <summary>
        /// 角色添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RoleAdd(RoleInfo info, IFormCollection form)
        {
            info.ID = StringUtils.GenerateUniqueID();
            info.CreateBy = LoginUserInfo.ID;
            info.CreateTime = DateTime.Now;
            if (SystemService.Insert(info))
            {
                string permissionIDs = form["PermissionIDs"];
                SystemService.Insert(GetRolePermissionList(info.ID, permissionIDs));
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
        /// 角色添加
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleEdit(string id)
        {
            RoleInfo info = SystemService.GetModel<RoleInfo>("ID", id);
            //获取所有权限
            List<AuthInfo> allPermission = SystemService.GetList<AuthInfo>();
            //获取角色对应的权限
            var rolePermission = SystemService.GetList<RoleAuth>("RoleID", id);
            var ztree = from p in allPermission
                        select new ZTreeData
                        {
                            id = p.ID,
                            pId = p.ParentID,
                            name = p.AuthName,
                            value = p.ID,
                            Checked = rolePermission.Exists(a => a.AuthID == p.ID)
                        };
            //序列化为ztree所需要的json串
            ViewBag.PermissionList = JsonConvert.SerializeObject(ztree);
            //构造隐藏域的值
            ViewBag.PermissionIDs = GetRolePermissionIDs(rolePermission);
            return View(info ?? new RoleInfo());
        }
        /// <summary>
        /// 角色添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RoleEdit(RoleInfo info, IFormCollection form)
        {
            if (SystemService.Update(info))
            {
                string permissionIDs = form["PermissionIDs"];
                SystemService.Delete<RoleAuth>("RoleID", info.ID);
                SystemService.Insert(GetRolePermissionList(info.ID, permissionIDs));
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
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult RoleDelete(string id)
        {
            if (SystemService.Delete<RoleInfo>("ID", id))
            {
                Result.IsOk = true;
                Result.Msg = "删除成功";
            }
            else
            {
                Result.IsOk = false;
                Result.Msg = "删除失败";
            }
            return Json(Result);
        }
        /// <summary>
        /// 获取角色权限List
        /// </summary>
        /// <returns></returns>
        public List<RoleAuth> GetRolePermissionList(string roleID, string permissionIDs)
        {
            List<RoleAuth> list = new List<RoleAuth>();
            foreach (string id in permissionIDs.Split(",".ToArray()))
            {
                if (string.IsNullOrEmpty(id))
                    continue;
                list.Add(new RoleAuth { RoleID = roleID, AuthID = id });
            }
            return list;
        }
        /// <summary>
        /// 构造角色权限ID集
        /// </summary>
        /// <returns></returns>
        public string GetRolePermissionIDs(List<RoleAuth> list)
        {
            string permissinIDs = "";
            foreach (RoleAuth info in list)
            {
                if (permissinIDs != "")
                    permissinIDs += ",";
                permissinIDs += info.AuthID;
            }
            return permissinIDs;
        }

        #endregion

        #region 权限管理
        /// <summary>
        /// 权限列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult AuthList()
        {
            return View(GetAuthForSelect());
        }
        /// <summary>
        /// 权限添加
        /// </summary>
        /// <returns></returns>
        public ActionResult AuthAdd()
        {
            AuthInfo info = new AuthInfo();
            if (info.AuthType == 0)
                info.AuthType = (int)AuthType.Module;
            ViewData["AuthTypeList"] = GetAuthTypeForSelect();
            ViewData["AuthList"] = new SelectList(from p in GetAuthForSelect() where p.AuthType != (int)AuthType.Action select p, "ID", "AuthName");
            return View(info);
        }
        /// <summary>
        /// 权限添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AuthAdd(AuthInfo info)
        {
            info.ParentID = (string.IsNullOrEmpty(info.ParentID) ? "0" : info.ParentID);
            AuthInfo hasInfo = SystemService.GetModel<AuthInfo>("AuthCode", info.AuthCode);
            if (hasInfo != null && hasInfo.ID != "")
            {
                Result.IsOk = false;
                Result.Msg = "权限编码已存在";
            }
            else
            {
                info.ID = StringUtils.GenerateUniqueID();
                if (SystemService.Insert<AuthInfo>(info))
                {
                    Result.IsOk = true;
                    Result.Msg = "添加成功";
                }
                else
                {
                    Result.IsOk = false;
                    Result.Msg = "添加失败";
                }
            }
            return Json(Result);
        }
        /// <summary>
        /// 权限编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult AuthEdit(string id)
        {
            AuthInfo info = SystemService.GetModel<AuthInfo>("ID", id);
            ViewData["AuthTypeList"] = GetAuthTypeForSelect();
            ViewData["AuthList"] = new SelectList(from p in GetAuthForSelect() where p.AuthType != (int)AuthType.Action select p, "ID", "AuthName");
            return View(info);
        }
        /// <summary>
        /// 权限编辑
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AuthEdit(AuthInfo info)
        {
            info.ParentID = (string.IsNullOrEmpty(info.ParentID) ? "0" : info.ParentID);
            if (SystemService.Update<AuthInfo>(info))
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
        /// <summary>
        /// 权限角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult AuthDelete(string id)
        {
            if (SystemService.Delete<AuthInfo>("ID", id))
            {
                Result.IsOk = true;
                Result.Msg = "删除成功";
            }
            else
            {
                Result.IsOk = false;
                Result.Msg = "删除失败";
            }
            return Json(Result);
        }
        #endregion


        /// <summary>
        /// 构造权限下拉框数据
        /// </summary>
        /// <returns></returns>
        protected List<AuthInfo> GetAuthForSelect()
        {
            List<AuthInfo> allAuth = SystemService.GetList<AuthInfo>();
            List<AuthInfo> authTemp = new List<AuthInfo>();

            var listP1 = from p in allAuth
                         where p.AuthType == 1
                         orderby p.Sort
                         select p;
            foreach (var item1 in listP1)
            {
                authTemp.Add(item1);
                var listP2 = from p in allAuth
                             where p.AuthType == 2 && p.ParentID == item1.ID
                             orderby p.Sort
                             select p;
                foreach (var item2 in listP2)
                {
                    item2.AuthName = "|----" + item2.AuthName;
                    authTemp.Add(item2);
                    var listP3 = from p in allAuth
                                 where p.AuthType == 3 && p.ParentID == item2.ID
                                 orderby p.Sort
                                 select p;
                    foreach (var item3 in listP3)
                    {
                        item3.AuthName = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|----" + item3.AuthName;
                        authTemp.Add(item3);
                    }
                }
            }
            return authTemp;
        }
    }
}