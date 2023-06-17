using System.Net;

namespace Domain.Models;

public class Response<T>
{
    public HttpStatusCode Status { get; set; }

    public string Message { get; set; } 

    public T Data { get; set; }
}
