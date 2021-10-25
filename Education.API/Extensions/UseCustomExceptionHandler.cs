using Education.Core.DTOs;
using Education.Core.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education.API.Extensions
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    if (error != null)
                    {
                        var ex = error.Error;
                        List<ErrorDto> errorDto = new List<ErrorDto>();
                        errorDto.Add(new ErrorDto { Error = ex.Message });

                        ErrorListResponse resp = new ErrorListResponse(errorDto);

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(resp));
                    }
                });
            });

        }
    }
}
