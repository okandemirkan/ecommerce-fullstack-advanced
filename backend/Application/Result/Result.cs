using System.Text.Json.Serialization;

namespace Application.Result
{
    public class Result<T>
    {
        public string? Message { get; private set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] //data nullsa json'a yazma.
        public T? Response { get; private set; }
        private Result(string? message, T? data)
        {
            Response = data;
            Message = message;
        }
        public static Result<T> Success(string message, T data)
            => new(message,data);
        public static Result<T> Success(string message)
          => new(message,default);
        public static Result<T> Failure(string message)
            => new(message,default);
    }
}
