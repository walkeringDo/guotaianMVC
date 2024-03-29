﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc
{
    public class MyController : Controller
    {
        /// <summary>
        /// Action执行前判断
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string msg;
            if (!this.CheckLogin(out msg))
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = Content("{\"loginstatus\":-1}");
                }
                else
                {
                    filterContext.Result = Content(string.Concat("<script>",
                        msg.IsNullOrEmpty() ? "" : string.Format("alert('{0}');", msg),
                        string.Compare(filterContext.Controller.ToString(), "WebMvc.Controllers.HomeController", true) == 0 ? "top.location='" + Url.Content("~/Login") + "'" : "top.login();", "</script>"), "text/html");
                }
            }

            base.OnActionExecuting(filterContext);
        }
        
        /// <summary>
        /// 验证登录
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected virtual bool CheckLogin(out string msg)
        {
            return WebMvc.Common.Tools.CheckLogin(out msg);
        }

    }
}