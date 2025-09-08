using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using MediatR;
using Refit;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace MicroserviceDemo.Shared
{
    public interface IRequestByServiceResult<T> : IRequest<ServiceResult<T>>;
    public interface IRequestByServiceResult : IRequest<ServiceResult>;


    public class ServiceResult
    {
        [JsonIgnore] public HttpStatusCode Status { get; set; }

        public ProblemDetails? Fail { get; set; }
        [JsonIgnore] public bool IsSuccess => Fail is null;
        [JsonIgnore] public bool IsFail => !IsSuccess;


        public static ServiceResult SuccessAsNoContent() => new() { Status = HttpStatusCode.NoContent };

        public static ServiceResult ErrorAsNotFound() => new()
        {
            Status = HttpStatusCode.NotFound,
            Fail = new ProblemDetails
            {
                Title = "Not Found", Detail = "The requested resource was not found",
                Status = (int)HttpStatusCode.NotFound
            }
        };

        public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode status) => new() { Status = status, Fail = problemDetails };
        public static ServiceResult Error(string title, string detail, HttpStatusCode status) => new() { Status = status, Fail = new ProblemDetails { Title = title, Detail = detail, Status = (int)status } };
        public static ServiceResult Error(string title, HttpStatusCode status) => new() { Status = status, Fail = new ProblemDetails { Title = title, Status = (int)status } };
        public static ServiceResult ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult()
                {
                    Fail = new ProblemDetails
                    {
                        Title = exception.Message,
                        Status = (int)exception.StatusCode
                    },
                    Status = exception.StatusCode
                };
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });


            return new ServiceResult()
            {
                Fail = problemDetails ?? new ProblemDetails
                {
                    Title = exception.Message,
                    Status = (int)exception.StatusCode
                },
                Status = exception.StatusCode
            };
        }
        public static ServiceResult ErrorFromValidator(IDictionary<string, object> errors) => new()
        {
            Status = HttpStatusCode.BadRequest,
            Fail = new ProblemDetails
            {
                Title = "Validation Error",
                Detail = "One or more validation errors occurred.",
                Status = (int)HttpStatusCode.BadRequest,
                Extensions =
                {
                    { "errors", errors }
                }
            }

        };


    }


    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }
        [JsonIgnore] public string? UrlAsCreated { get; set; }

        public static ServiceResult<T> SuccessAsOk(T data) => new() { Status = HttpStatusCode.OK, Data = data };
        public static ServiceResult<T> SuccessAsCreated(T data, string url) => new() { Status = HttpStatusCode.Created, UrlAsCreated = url, Data = data };

        public new static ServiceResult<T> Error(ProblemDetails problemDetails, HttpStatusCode status) => new() { Status = status, Fail = problemDetails };
        public new static ServiceResult<T> Error(string title, string detail, HttpStatusCode status) => new() { Status = status, Fail = new ProblemDetails { Title = title, Detail = detail, Status = (int)status } };
        public new static ServiceResult<T> Error(string title, HttpStatusCode status) => new() { Status = status, Fail = new ProblemDetails { Title = title, Status = (int)status } };
        public new static ServiceResult<T> ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult<T>()
                {
                    Fail = new ProblemDetails
                    {
                        Title = exception.Message,
                        Status = (int)exception.StatusCode
                    },
                    Status = exception.StatusCode
                };
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });


            return new ServiceResult<T>()
            {
                Fail = problemDetails ?? new ProblemDetails
                {
                    Title = exception.Message,
                    Status = (int)exception.StatusCode
                },
                Status = exception.StatusCode
            };
        }
        public new static ServiceResult<T> ErrorFromValidator(IDictionary<string, object> errors) => new()
        {
            Status = HttpStatusCode.BadRequest,
            Fail = new ProblemDetails
            {
                Title = "Validation Error",
                Detail = "One or more validation errors occurred.",
                Status = (int)HttpStatusCode.BadRequest,
                Extensions =
                {
                    { "errors", errors }
                }
            }

        };
    }
}
