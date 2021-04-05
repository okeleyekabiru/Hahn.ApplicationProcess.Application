using System;
using System.Net;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.February2021.Domain.Exceptions;
using Hahn.ApplicationProcess.February2021.Domain.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


namespace Hahn.ApplicationProcess.February2021.Domain.Middlewares
{
    public class HttpStatusCodeExceptionMiddleware
    {
        private readonly ILogger<HttpStatusCodeExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public HttpStatusCodeExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory?.CreateLogger<HttpStatusCodeExceptionMiddleware>() ??
                      throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpStatusCodeException ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning(
                        "The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                await HandleExceptionAsync(context, ex);

            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            string result = String.Empty;

            var innermostException = exception.GetBaseException();

            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };


            if (innermostException is HttpStatusCodeException)
            {
                var actualException = innermostException as HttpStatusCodeException;
                code = (HttpStatusCode)actualException.StatusCode;
                result = JsonConvert.SerializeObject(new BaseResponse<string>
                {
                    Data = null,
                    Code = (int)code,
                    Message = actualException.Message
                }, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver,
                    Formatting = Formatting.Indented
                }); ;
            }
            else
            {
                result = JsonConvert.SerializeObject(new BaseResponse<string>
                {
                    Code = (int)code,
                    Message = "An unexpected error occurred"
                }) ;
                _logger.LogError(exception, $"Error occurred while completing operation for path {context.Request.Path}");
            }



            // if (string.IsNullOrEmpty(result))
            // {
            //     result = JsonConvert.SerializeObject(new ErrorResponse { Message = innermostException.Message });
            // }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            // _logger.LogCritical($@"Action - {}
            //     {Environment.NewLine}
            //     result - {result}
            //     {Environment.NewLine}
            // Stacktrace - {innermostException.StackTrace}");
            await context.Response.WriteAsync(result);
        }

    }
}