using System;

namespace API.Erros;

public class ApiException(int StatusCode, string Message, string? Detail = null)
{

    public int StatusCode { get; set; } = StatusCode;   
    public string Message { get; set; } = Message;
    public string? Detail { get; set; } = Detail;

}
