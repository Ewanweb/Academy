namespace Academy.Api.DTOs;

public class ApiResponse<T>
{
    public T? Data { get; set; }
    public string MessageType { get; set; } = "success";
    public string Message { get; set; } = string.Empty;
    public Dictionary<string, string[]>? Errors { get; set; }

    public static ApiResponse<T> Success(T? data, string message = "عملیات با موفقیت انجام شد") =>
        new() { Data = data, Message = message, MessageType = "success" };

    public static ApiResponse<T> Fail(string message, Dictionary<string, string[]>? errors = null) =>
        new() { Data = default, Message = message, MessageType = "error", Errors = errors };
}
