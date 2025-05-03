
using System.Text.Json;

namespace ExpenseManagement.Base;

public class ApiResponse
{
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public Guid ReferenceNo { get; set; } = Guid.NewGuid();
    public DateTime ServerDate { get; set; } = DateTime.UtcNow; //response`n üretildiği zaman
    public bool Success { get; set; }
    public string Message { get; set; }

    public ApiResponse(string message = null)
    {
        if (string.IsNullOrWhiteSpace(message)) Success = true;
        else
        {
            Success = false;
            Message = message;
        }
    }
}

public class ApiResponse<T>
{
    public Guid ReferenceNo { get; set; } = Guid.NewGuid();
    public DateTime ServerDate { get; set; } = DateTime.UtcNow;
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Response { get; set; }

    public ApiResponse(bool isSuccess)
    {
        Success = isSuccess;
        Response = default;
        Message = isSuccess ? "Success" : "Error";
    }
    public ApiResponse(T data)
    {
        Success = true;
        Response = data;
        Message = "Success";
    }
    public ApiResponse(string message)
    {
        Success = false;
        Response = default;
        Message = message;
    }
}
