
using Shitou.Framework.ORM;
using Microsoft.Extensions.Logging;

using YeZiFang.Service;
using YeZiFang.DataContract.Response;
using YeZiFang.DataContract.Request;
using YeZiFang.Dao;

namespace YeZiFang.Service
{
    /// <summary>
    /// 业务基础层
    /// </summary>
    public class ProjectService : BaseService, IProjectService
    {
        public IProjectDao ProjectDao { get; set;}
        public ILogger Logger { get; set; }
        public ProjectService(IProjectDao projectDao, IAdoTemplate adoTemplate, ILogger<SystemService> logger)
            : base(adoTemplate, logger)
        {
            ProjectDao = projectDao;
            Logger = logger;
        }

        #region 楼盘管理

        /// <summary>
        /// 商品列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public Pager<GetProjectListResponse> GetProjectList(GetProjectListRequest request)
        {
            return ProjectDao.GetProjectList(request);
        }

        #endregion
    }
}
