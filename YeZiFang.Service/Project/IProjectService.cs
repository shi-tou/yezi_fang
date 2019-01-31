
using Shitou.Framework.ORM;
using System.Collections.Generic;
using YeZiFang.DataContract.Request;
using YeZiFang.DataContract.Response;
using YeZiFang.Service;

namespace YeZiFang.Service
{
    /// <summary>
    /// 业务基础层
    /// </summary>
    public interface IProjectService : IBaseService
    {
        #region 楼盘管理
        /// <summary>
        /// 商品列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        Pager<GetProjectListResponse> GetProjectList(GetProjectListRequest request);
        #endregion

    }
}
