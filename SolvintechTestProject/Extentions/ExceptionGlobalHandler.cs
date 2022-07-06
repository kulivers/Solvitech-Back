using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using PasswordManager.Entities.Exceptions;

namespace SolvintechTestProject.Extentions
{
    public static class ExceptionGlobalHandler
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = GetResponseStatusCode(contextFeature); 
                        var errorModels = GetErrorModels(contextFeature, context);
                        var model = new ErrorResultModel(errorModels);
                        await context.Response.WriteAsync(model.ToString());
                    }
                });
            });
        }

        private static IEnumerable<ErrorModel> GetErrorModels(IExceptionHandlerFeature contextFeature,
            HttpContext context)
        {
            return contextFeature.Error switch
            {
                MultipleErrorsException errors => errors.Messages.Select(message =>
                    new ErrorModel(context.Response.StatusCode, message)),
                _ => new ErrorModel[]
                {
                    new()
                    {
                        Code = context.Response.StatusCode,
                        Description = contextFeature.Error.Message,
                    }
                }
            };
        }

        private static int GetResponseStatusCode(IExceptionHandlerFeature contextFeature)
        {
            return contextFeature.Error switch
            {
                RegistrationNotSuccess => StatusCodes.Status409Conflict,
                LoginNotSuccess => StatusCodes.Status401Unauthorized,
                MultipleErrorsException => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };
        }
    }
}