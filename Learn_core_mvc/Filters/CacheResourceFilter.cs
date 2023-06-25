using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Filters
{
    public class CacheResourceFilter : IResourceFilter
    {
        private static readonly Dictionary<string, object> _cache
                = new Dictionary<string, object>();
        private string _cacheKey;

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _cacheKey = context.HttpContext.Request.Path.ToString();
            if (_cache.ContainsKey(_cacheKey))
            {
                var cachedValue = _cache[_cacheKey];
                if (cachedValue != null)
                {
                    var redirectResult = new RedirectToActionResult("GetCachedData", "FiltersSample", new { data= cachedValue });
                    context.Result = redirectResult;
                }
            }
        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            if (!String.IsNullOrEmpty(_cacheKey) && !_cache.ContainsKey(_cacheKey))
            {
                var result = context.Result as JsonResult;
                if (result != null)
                {
                    var value = result.Value as dynamic;
                    var data = value.data;
                    _cache.Add(_cacheKey, data);
                }
            }
        }
    }
}
