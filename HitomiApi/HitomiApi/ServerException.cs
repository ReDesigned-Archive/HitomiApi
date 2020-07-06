using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EmbedIO;
using Swan.Logging;

namespace HitomiApi
{
    public class ServerException
    {
        private static string LogSource = "ExceptionHandler";

        public static async Task Handle(IHttpContext ctx, IHttpException exception)
        {
            $"HTTP Error Caught: {ctx.RemoteEndPoint.Address} ({exception.StatusCode}) {exception.Message} {ctx.RequestedPath}".Warn(LogSource);
            await ctx.SendDataAsync(new {Message = ((HttpStatusCode)exception.StatusCode).ToString()});
        }
        public static async Task HandleU(IHttpContext ctx, Exception exception)
        {
            $"UnHandled Error Caught: {ctx.RemoteEndPoint.Address} {exception.Message} {ctx.RequestedPath}".Warn(LogSource);
            await ctx.SendDataAsync(new { Message = "InternalServerError" });
        }
    }
}
