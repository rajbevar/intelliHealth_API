using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PatientEngTranscription.Api.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CustomExceptionMiddleware> _logger;
        private readonly JsonSerializerSettings _jsonSettings;
        private readonly IHostingEnvironment _environment;


        public CustomExceptionMiddleware(RequestDelegate next, IServiceProvider serviceProvider, IHostingEnvironment environment)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _environment = environment;

            _serviceProvider = serviceProvider;
            _logger = serviceProvider.GetRequiredService<ILogger<CustomExceptionMiddleware>>();

            _jsonSettings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Operation409ConflictException ex409)
            {
                _logger.LogError($"Something went wrong: {ex409}");
                await HandleExceptionAsync(httpContext, ex409, HttpStatusCode.Conflict);
            }
            catch (Operation400BadRequestException ex400)
            {
                _logger.LogError($"Something went wrong: {ex400}");
                await HandleExceptionAsync(httpContext, ex400, HttpStatusCode.BadRequest);
            }
            catch (Operation401UnauthorizedException ex401)
            {
                _logger.LogError($"Something went wrong: {ex401}");
                await HandleExceptionAsync(httpContext, ex401, HttpStatusCode.Unauthorized);
            }
            catch (Operation403ForbiddenException ex403)
            {
                _logger.LogError($"Something went wrong: {ex403}");
                await HandleExceptionAsync(httpContext, ex403, HttpStatusCode.Forbidden);
            }
            catch (Operation404NotFoundException ex404)
            {
                _logger.LogError($"Something went wrong: {ex404}");
                await HandleExceptionAsync(httpContext, ex404, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode httpStatusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;

            var errorObj = new ApiError(exception.Message)
            {
                 Details= exception.ToString(),
                  ExceptionMessage= exception.Message,
                   ReferenceErrorCode= context.Response.StatusCode.ToString()
            };

            //var errorObj = new ApiException()
            //{
            //    Code = context.Response.StatusCode,
            //    Message = exception.Message,
            //    DetailedError = _environment.IsDevelopment() ? exception.ToString() : null,
            //    success = false,
            //};

            //return context.Response.WriteAsync(errorResponse).ToString();

            //var modelState = new ModelStateDictionary();
            //exception.AddModelStateErrors(modelState);
            //var json = JsonConvert.SerializeObject(modelState.Errors(true), _jsonSettings);

            var json = JsonConvert.SerializeObject(errorObj);
            await context.Response.WriteAsync(json);
        }



    }
}
