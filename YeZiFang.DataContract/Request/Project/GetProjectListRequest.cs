using System;
using System.Collections.Generic;
using System.Text;
using YeZiFang.DataContract;

namespace YeZiFang.DataContract.Request
{
    public class GetProjectListRequest : PageRequest
    {
        public string ProjectName { get; set; }
    }
}
