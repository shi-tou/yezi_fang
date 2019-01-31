﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YeZiFang.Application.Models;
using YeZiFang.Application.Extensions;
using YeZiFang.Application.Model;
using System.Diagnostics;

namespace YeZiFang.Application.Filter
{
    /// <summary>
    /// 全局异常处理过滤器
    /// </summary>
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        private readonly ILogger _logger;

        public GlobalExceptionFilterAttribute(
            IHostingEnvironment hostingEnvironment,
            IModelMetadataProvider modelMetadataProvider,
            ILogger<GlobalExceptionFilterAttribute> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            //var request = context.HttpContext.Request;
            //_logger.LogError(context.Exception, string.Format("ErrorCdoe:{0},host:{1},path:{2}\n",
            //    Activity.Current?.Id ?? context.HttpContext.TraceIdentifier,
            //    request.Host, request.Path));
            //base.OnException(context);
            if (!_hostingEnvironment.IsDevelopment())
            {
                return;
            }
#if DEBUG
            AjaxResult result = null;
            var request = context.HttpContext.Request;
            if (!request.IsAjax())
            {
                _logger.LogError(context.Exception, string.Format("host:{0},path:{1}", request.Host, request.Path));
                result = new AjaxResult { IsOk = false, Msg = context.Exception.Message, Data = "" };
            }
            else
            {
                _logger.LogError(context.Exception, string.Format("host:{0},path:{1}", request.Host, request.Path));
                result = new AjaxResult { IsOk = false, Msg = context.Exception.Message, Data = "" };
            } 
#endif
#if RELEASE

            var request = context.HttpContext.Request;
            _logger.LogError(context.Exception, string.Format("host:{0},path:{1}", request.Host, request.Path));
            var result = new AjaxResult { IsOk = false, Msg = "服务开小差了,请联系管理员", Data = "" };
#endif
            context.Result = new JsonResult(result);
        }
    }
}
