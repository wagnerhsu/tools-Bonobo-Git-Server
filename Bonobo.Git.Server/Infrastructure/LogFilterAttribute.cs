using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bonobo.Git.Server.Infrastructure
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        static ILogger Logger = LogManager.GetCurrentClassLogger();
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception!=null)
            {
                Logger.Error(filterContext.Exception.ToString());
            }
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string userName = filterContext.HttpContext.User.Identity.Name;
            var controller = filterContext.Controller.ToString();
            var requestPath = filterContext.HttpContext.Request.CurrentExecutionFilePath;
            Logger.Debug($"{userName} access {controller}|{filterContext.ActionDescriptor.ActionName}|{requestPath}");
        }
    }
}