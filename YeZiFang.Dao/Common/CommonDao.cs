using System.Text;
using Dapper;
using Shitou.Framework.ORM;
using YeZiFang.DataContract.Request;
using YeZiFang.DataContract.Response;

namespace YeZiFang.Dao
{
    public class CommonDao : ICommonDao
    {
        public IAdoTemplate AdoTemplate { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbConnection">数据库链接</param>
        /// <param name="sqlGenerator"> sq语句构造器</param>
        public CommonDao(IAdoTemplate adoTemplate)
        {
            AdoTemplate = adoTemplate;
        }

    }
}
