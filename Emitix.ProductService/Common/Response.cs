using System.Text.Json.Serialization;

namespace Emitix.ProductService.Common;

public record Response<T>
{
    public T? Data { get; init; }
    public string Message { get; init; } = string.Empty;
    public int Code { get; init; }
    
    [JsonIgnore]
    public bool IsSuccess => Code is >= 200 and <= 299; 

    private Response(){}
    private Response(T? data, string message, int code)
    {
        Data = data;
        Message = message;
        Code = code;
    }

    public static Response<T> Success(T? data, string message = "", int code = 200)
        => new(data, message, 200);
    
    public static Response<T> Error(T? data, string message, int code)
        => new(data, message, code);
}