using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Attributes
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.ActionArguments.SingleOrDefault();
            if (param.Value == null)
            {
                context.Result = new BadRequestObjectResult("Model is null");
                return;
            }
            if (!context.ModelState.IsValid)
            {
                return;
                //context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
