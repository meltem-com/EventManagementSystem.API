using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using EventManagementSystem.API.Middlewares;

namespace EventManagementSystem.API.Middlewares
{
    /// <summary>
    /// Middleware that logs incoming HTTP requests with method, path, and timestamp.
    /// </summary>
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor that initializes the next middleware in the pipeline.
        /// </summary>
        /// <param name="next">The next middleware component to invoke.</param>
        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Logs the HTTP method and request path before passing to the next middleware.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"🟢 Incoming Request: {context.Request.Method} {context.Request.Path} - {DateTime.Now}");
            await _next(context); // Call the next middleware in the pipeline
        }
    }
}