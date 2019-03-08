using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shitou.Framework.ORM;
using YeZiFang.DataContract.Request;
using YeZiFang.DataContract.Response;
using YeZiFang.Model;
using YeZiFang.Service;

namespace YeZiFang.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        public IProjectService ProjectService { get; set; }
        public ProjectController(IProjectService projectService)
        {
            ProjectService = projectService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public Pager<GetProjectListResponse> Get(GetProjectListRequest request)
        {
            request.CityId = 6;
            Pager<GetProjectListResponse> list = ProjectService.GetProjectList(request);
            return list;
        }
    }
}