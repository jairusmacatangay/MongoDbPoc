using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MongoDbPoc.Models
{
    public class ApiResponse()
    {
        [JsonIgnore]
        public int StatusCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public IEnumerable<string>? Errors { get; set; }
    }

    public class ApiResponse<T>() : ApiResponse 
    {
        public T? Data { get; set; }

    }
}
