using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YeZiFang.App.Models;
using YeZiFang.App.Service;

namespace YeZiFang.App.Controllers
{
    public class HomeController : Controller
    {
        private IMongoService _mongoService { get; set; }
        public HomeController(IMongoService mongoService)
        {
            _mongoService = mongoService;
        }

        public IActionResult Index()
        {
            PagingList<ProjectModel> list = _mongoService.PageList<ProjectModel>(1, 20);
            return View(list);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
