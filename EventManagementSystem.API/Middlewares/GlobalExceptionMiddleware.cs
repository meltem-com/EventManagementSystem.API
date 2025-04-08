using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventManagementSystem.API.Middlewares
{
    // This middleware catches unhandled exceptions in the request pipeline
    // and returns a custom JSON error response to the client
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        // Constructor receives the next middleware delegate
        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // Main method that intercepts HTTP requests
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Call the next middleware in the pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                // Set the response content type and status code
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // Create a custom JSON response object
                var response = new
                {
                    status = 500,
                    message = "🚨 A system error occurred. Please try again later.",
                    detail = ex.Message // In production, hide this detail and log it instead
                };

                // Write the response as JSON
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
