using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace EventManagementSystem.API.Filters
{
    /// <summary>
    /// Allows access to the action only between 09:00 and 21:00.
    /// </summary>
    public class TimeAccessFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var now = DateTime.Now.TimeOfDay;
            var start = new TimeSpan(9, 0, 0);  // 09:00
            var end = new TimeSpan(21, 0, 0);   // 21:00

            if (now < start || now > end)
            {
                context.Result = new ContentResult
                {
                    Content = "🚫 You can only access this endpoint between 09:00 and 21:00.",
                    StatusCode = 403
                };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Optional: You can add logging here if needed
        }
    }
}
