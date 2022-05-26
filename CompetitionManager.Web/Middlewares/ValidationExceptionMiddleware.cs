using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CompetitionManager.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace CompetitionManager.Web.Middlewares
{
    public class ValidationExceptionMiddleware
    {
        public ValidationExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public readonly RequestDelegate next;

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (ValidationException exception)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(new { exception.Message });
            }
            catch (ObjectNotFoundException exception)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(new { exception.Message });
            }
            //catch (FluentValidation.ValidationException exception)
            //{
            //    var errors = exception.Errors.Select(s => $"{s.PropertyName} {s.ErrorMessage}");
            //    var errorMessage = string.Join(Environment.NewLine, errors);

            //    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            //    await httpContext.Response.WriteAsJsonAsync(new { Error = errorMessage });
            //}
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(new { Message = ex.Message });
            }
        }
    }
}
