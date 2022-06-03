using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OneByte.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                HandleException(ex,context);
            }
        }

        private void HandleException(Exception ex, HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            var result = JsonSerializer.Serialize(new { error = ex.Message });
            context.Response.WriteAsync(result);
        }
    }
}