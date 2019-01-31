
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
    public class CommonService : BaseService, ICommonService
    {
        public ICommonDao CommonDao { get; set;}
        public ILogger Logger { get; set; }
        public CommonService(ICommonDao commonDao, IAdoTemplate adoTemplate, ILogger<SystemService> logger)
            : base(adoTemplate, logger)
        {
            CommonDao = commonDao;
            Logger = logger;
        }
    }
}
