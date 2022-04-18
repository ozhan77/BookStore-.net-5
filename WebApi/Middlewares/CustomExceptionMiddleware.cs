using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Builder;
using System.Diagnostics;

namespace WebApi.Middlewares
{
    public class CustomExceptionMiddware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionMiddware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            string message = "[Request] HTTP" + context.Request.Method + " - " + context.Request.Path;
            Console.WriteLine(message);
            await _next(context);
            watch.Stop();
            message = "[Response] HTTP"+ context.Request.Method+ " - "+ context.Request.Path+" responded"+ context.Response.StatusCode+"in"+watch.Elapsed.TotalMilliseconds+"ms";
        }
    }

    public static class CustomExceptionMiddwareExtention
    {
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddware>();
        }
    }
}