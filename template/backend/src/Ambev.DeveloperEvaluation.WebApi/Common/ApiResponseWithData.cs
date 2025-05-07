using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

public class ApiResponseWithData<T> : ApiResponse
{
    public T? Data { get; set; }

    public static ApiResponseWithData<T> Success(T data, string message = "Success") =>
        new()
        {
            //Success = true,
            Message = message,
            Data = data
        };

    public static ApiResponseWithData<T> Fail(string message, IEnumerable<ValidationErrorDetail>? errors = null) =>
        new()
        {
            //Success = false,
            Message = message,
            Errors = errors ?? new List<ValidationErrorDetail>()
        };
}

