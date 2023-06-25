using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Filters
{
    public class ActionEncodeFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var actionArgument in context.ActionArguments)
            {
                var parameter = actionArgument.Value;
                foreach (var prop in parameter.GetType().GetProperties())
                {
                    if (prop.PropertyType.IsClass)
                    {
                        string value = prop.GetValue(parameter).ToString().Trim();
                        var htmlEncodedValue = System.Web.HttpUtility.HtmlEncode(value);
                        prop.SetValue(parameter, htmlEncodedValue);
                    }
                }
            }
        }
    }
}
