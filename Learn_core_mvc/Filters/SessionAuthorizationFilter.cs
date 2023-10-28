
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Filters
{
    public class SessionAuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            //bool hasAllowAnonymous = context.ActionDescriptor.EndpointMetadata
            //                     .Any(em => em.GetType() == typeof(AllowAnonymousAttribute)); 

            var actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            var hasAllowAnonymous = actionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
                .Any(a => a.GetType() == typeof(AllowAnonymousAttribute));

            if (hasAllowAnonymous)
            {
                // Don't check for authorization as AllowAnonymous filter is applied to the action or controller  
                return;
            }

            // Check for authorization  
            if (context.HttpContext.Session.GetString("UserName") == null)
            {
                var redirectResult = new RedirectToActionResult("Index", "Session",null);
                context.Result = redirectResult;
            }

            if (context.HttpContext.Session.GetString("UserRole") != Roles)
            {
                var redirectResult = new RedirectToActionResult("Dashboard", "Session", null);
                context.Result = redirectResult;
            }
        }
    }
}
