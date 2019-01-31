using System.Text;
using Dapper;
using Shitou.Framework.ORM;
using YeZiFang.DataContract.Request;
using YeZiFang.DataContract.Response;

namespace YeZiFang.Dao
{
    public class ProjectDao : IProjectDao
    {
        public IAdoTemplate AdoTemplate { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbConnection">数据库链接</param>
        /// <param name="sqlGenerator"> sq语句构造器</param>
        public ProjectDao(IAdoTemplate adoTemplate)
        {
            AdoTemplate = adoTemplate;
        }

        #region 商品管理
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Pager<GetProjectListResponse> GetProjectList(GetProjectListRequest request)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(@"select * from T_Project a where 1=1 ");
            var param = new DynamicParameters();
            if (!string.IsNullOrEmpty(request.ProjectName))
            {
                sbSql.Append(" and a.ProjectName like ?ProjectName");
                param.Add("ProjectName", "%" + request.ProjectName + "%");
            }
            if (request.CityId > 0)
            {
                sbSql.Append(" and a.CityId = ?CityId");
                param.Add("CityId", request.CityId);
            }
            request.OrderBy = "a.CreateTime desc";
            return AdoTemplate.GetPagedList<GetProjectListResponse>(sbSql.ToString(), param, request.PageIndex, request.PageSize, request.OrderBy);
        }

        #endregion

    }
}
