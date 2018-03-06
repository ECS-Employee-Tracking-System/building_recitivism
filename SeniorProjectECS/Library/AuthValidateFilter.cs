using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace SeniorProjectECS.Library
{
    /// <summary>
    /// Global action filter to check authentication
    /// </summary>
    public class AuthValidateFilter : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            // Check if we are already on the home page or trying to login
            if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                var isDefined = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
                    .Any(a => a.GetType().Equals(typeof(DefaultAction)));

                // If we are a default action don't redirect
                if (isDefined)
                {
                    return;
                }
            }

            // Redirect to home page if we are not authenticated
            if (context.HttpContext.Session.GetInt32("AccessLevel") == null)
            {
                context.HttpContext.Response.Redirect("/Home/Index");
            }

            if (context.ActionDescriptor is ControllerActionDescriptor actionDescriptors)
            {
                if (actionDescriptors.MethodInfo.GetCustomAttributes(inherit: true).Any(a => a.GetType().Equals(typeof(AdminOnly))))
                {
                    if (context.HttpContext.Session.GetInt32("AccessLevel") > 1)
                    {
                        context.HttpContext.Response.Redirect("/Home/Index");
                    }
                }
            }
            if (context.ActionDescriptor is ControllerActionDescriptor actionDescriptor)
            {
                if (actionDescriptor.MethodInfo.GetCustomAttributes(inherit: true).Any(a => a.GetType().Equals(typeof(ViewOnly))))
                {
                    if (context.HttpContext.Session.GetInt32("AccessLevel") > 2)
                    {
                        context.HttpContext.Response.Redirect("/Home/Index");
                    }
                }
            }
        }
    }
}
