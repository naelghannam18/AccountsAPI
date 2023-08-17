using System.Net;
using System.Text.Json;
using Domain.Exceptions;
using FluentValidation;

namespace AccountsApi.ExceptionHandler;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate Next;

    public GlobalExceptionHandler(RequestDelegate next)
    {
        Next = next;
    }

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

    private static string GetExceptionType(Exception ex) => ex.GetType().ToString();

    private class Response
    {
        public string Message { get; set; }

        public int StatusCode { get; set; }

        public string Details { get; set; }

    }
}
