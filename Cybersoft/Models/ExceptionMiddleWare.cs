namespace Cybersoft.Models
{
        using System;
        using System.Net;
        using System.Threading.Tasks;
        using Microsoft.AspNetCore.Http;
        using Cybersoft.ApplicationCore.Exceptions;

        /// <summary>
        /// ExceptionMiddleware
        /// </summary>
        public class ExceptionMiddleware
        {
            private readonly RequestDelegate _next;

            public ExceptionMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task InvokeAsync(HttpContext httpContext)
            {
                if (httpContext.Request.Path.StartsWithSegments("/api", StringComparison.CurrentCulture))
                {
                    try
                    {
                        await _next(httpContext).ConfigureAwait(false);
                    }
                //    catch (Exception e) //EmployeeNotFoundException
                //{
                //        await HandleExceptionAsync(httpContext, HttpStatusCode.NotFound, e.Message)
                //            .ConfigureAwait(false);
                //    }
                    catch (Exception ex)
                    {
                    Console.WriteLine(ex.Message);
                    await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError,
                            "Internal Server Error").ConfigureAwait(false);
                    }
                }
                else
                {
                    await _next(httpContext).ConfigureAwait(false);
                }
            }

            private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)statusCode;
                context.Response.Headers.Add("X-Error", message);

                return context.Response.WriteAsync(new ErrorModel
                {
                    StatusCode = context.Response.StatusCode,
                    Message = message
                }.ToString());
            }
        }
    }


