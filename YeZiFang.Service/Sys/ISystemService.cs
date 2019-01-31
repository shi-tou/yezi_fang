﻿
using System.Collections.Generic;
using YeZiFang.DataContract.Request;
using YeZiFang.DataContract.Response;
using YeZiFang.Model;
using Shitou.Framework.ORM;

namespace YeZiFang.Service
{
    /// <summary>
    /// 业务基础层
    /// </summary>
    public interface ISystemService : IBaseService
    {
        #region 用户管理
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="request"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        UserLoginResponse UserLogin(UserLoginRequest request);
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        Pager<GetUserListResponse> GetUserList(GetUserListRequest request);
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        Pager<GetRoleListResponse> GetRoleList(GetRoleListRequest request);
        /// <summary>
        /// 获取用户的权限
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        List<AuthInfo> GetUserAuth(string userID);
        #endregion
    }
}
