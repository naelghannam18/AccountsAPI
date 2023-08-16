using Domain.Exceptions;
using System.Net;
using System.Text.Json;

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
        }catch (Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = ex switch
            {
                AccountDoesNotExistException or AccountInsufficientFundsException or CustomerDoesNotExistException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError,
            };
            var result = JsonSerializer.Serialize(new { message = ex?.Message });
            await response.WriteAsync(result);
        }
    }
}
