using YeZiFang.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YeZiFang.DataContract.Response
{
    /// <summary>
    /// 用户列表
    /// </summary>
    public class GetRoleListResponse : RoleInfo
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUser { get; set; }
    }
}
