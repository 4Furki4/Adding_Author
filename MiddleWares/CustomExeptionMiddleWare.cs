using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using BookStore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BookStore.MiddleWares
{
    public class CustomExeptionMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILoggerService _loggerService;
        public CustomExeptionMiddleWare(RequestDelegate next, ILoggerService loggerService)
        {
            this.next = next;
            _loggerService = loggerService;
        }
        public async Task Invoke(HttpContext context)
        {
            var watch= Stopwatch.StartNew();
            try
            {
                string message=$"[Request]  HTTP {context.Request.Method}  -  {context.Request.Path}";
                _loggerService.Write(message);
                await next.Invoke(context); //next.(context) de aynı işi yapar.
                message=$"[Response] HTTP {context.Request.Method}  -  {context.Request.Path} responded {context.Response.StatusCode} in {watch.Elapsed.TotalMilliseconds} ms";
                _loggerService.Write(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await ExceptionHandle(context, ex, watch);
            }
        }

        private Task ExceptionHandle(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType="application/json";
            context.Response.StatusCode= (int)HttpStatusCode.InternalServerError;
            string message=$"[Error]    HTTP {context.Request.Method}  -  {context.Response.StatusCode} Error Message: {ex.Message} in {watch.Elapsed.TotalMilliseconds} ms";
            _loggerService.Write(message); //Konsola mesajı yazdırıyor.
            var result= JsonConvert.SerializeObject(new {error=ex.Message}, Formatting.None); //ex nesnesinin mesajını UI'a taşıyabilmek için veri taşıma yöntemini kullanıyoruz ve bunu json'a serialize etmemiz gerekiyor, yeni nesnede error değişkeniyle bunu sağlıyoruz.
            return context.Response.WriteAsync(result); //UI'da mesajı yazdırıyoruz.
        }
    }
    public static class CustomExeptionMiddleWareExtension
    {
        public static IApplicationBuilder UseCustomExeptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExeptionMiddleWare>();
        }
    }
}