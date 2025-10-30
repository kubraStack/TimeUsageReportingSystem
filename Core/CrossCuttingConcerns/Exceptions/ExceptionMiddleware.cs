using Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                if (ex is BusinessException)
                {
                    ProblemDetails problemDetails = new ProblemDetails();
                    problemDetails.Title = "Business Exception";
                    problemDetails.Detail = ex.Message;
                    problemDetails.Type = "BusinessException";
                    await httpContext.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
                }
                else if(ex is ValidationException)
                {
                    ValidationException validationException = (ValidationException)ex;
                    ValidationProblemDetails validationProblemDetails = new ValidationProblemDetails(validationException.Errors.ToList());
                    await httpContext.Response.WriteAsync(JsonSerializer.Serialize(validationProblemDetails));
                }
                else
                {
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await httpContext.Response.WriteAsync("Bilinmedik Hata!");
                }
            }
        }
    }
}
