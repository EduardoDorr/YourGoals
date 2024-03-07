using System.Net;
using YourGoals.Core.Results.Errors;

namespace YourGoals.Application.Abstractions.Errors;

public record HttpStatusCodeError(string Code, string Message, HttpStatusCode StatusCode = HttpStatusCode.InternalServerError) : IError
{
    public HttpStatusCodeError(IError error, HttpStatusCode StatusCode = HttpStatusCode.InternalServerError)
        : this(error.Code, error.Message, StatusCode) { }
}