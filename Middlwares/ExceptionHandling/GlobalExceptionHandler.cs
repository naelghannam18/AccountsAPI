using System.Net;
using System.Text.Json;
using Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Middlewares.ExceptionHandling;

public class GlobalExceptionHandler
{
    #region Private Readonly Fields
    private readonly RequestDelegate Next;
    #endregion

    #region Constructor
    public GlobalExceptionHandler(RequestDelegate next)
    {
        Next = next;
    }
    #endregion

    #region Public Methods

    #region Invoke
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await Next(context);
        }
        catch (Exception ex)
        {
            var response = new Response()
            {
                Message = GetExceptionType(ex),
                Details = ex.Message
            };

            var statusCode = GetStatusCode(ex);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(result);
        }
    }
    #endregion

    #endregion

    #region Private Methods

    #region Get Status Code
    private static int GetStatusCode(Exception ex)
    {
        return ex switch
        {
            AccountDoesNotExistException or
            AccountInsufficientFundsException or
            CustomerDoesNotExistException
                => (int)HttpStatusCode.BadRequest,
            ValidationException
                => (int)HttpStatusCode.UnprocessableEntity,
            _ => (int)HttpStatusCode.InternalServerError
        };
    }
    #endregion

    #region Get Exception Type
    private static string GetExceptionType(Exception ex) => ex.GetType().ToString();
    #endregion

    #endregion

    #region Response Class
    private class Response
    {
        public string Message { get; set; }

        public int StatusCode { get; set; }

        public string Details { get; set; }

    } 
    #endregion
}
