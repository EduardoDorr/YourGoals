using System.Net;

using YourGoals.Core.Results.Errors;

namespace YourGoals.Application.Errors;

public record HttpStatusCodeError(string Code, string Message, HttpStatusCode StatusCode) : IError
{
    public HttpStatusCodeError(Error error, HttpStatusCode StatusCode)
        : this(error.Code, error.Message, StatusCode) { }
}