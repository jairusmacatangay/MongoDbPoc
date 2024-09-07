using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MongoDbPoc.Models
{
    public class ApiResponse(bool isSuccess, int statusCode, string message, IEnumerable<string>? errors = null)
    {
        public bool Success { get; set; } = isSuccess;

        public int StatusCode { get; set; } = statusCode;

        public string Message { get; set; } = message;

        public IEnumerable<string>? Errors { get; set; } = errors ?? [];
    }

    public class ApiResponse<T>(bool isSuccess, T data, int statusCode, string message, IEnumerable<string>? errors = null) 
        : ApiResponse(isSuccess, statusCode, message, errors)
    {
        public T Data { get; set; } = data;

    }
}
