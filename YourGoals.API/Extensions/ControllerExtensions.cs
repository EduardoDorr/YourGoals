using System.Net;

using Microsoft.AspNetCore.Mvc;

using YourGoals.Core.Results.Errors;
using YourGoals.Application.Abstractions.Errors;

namespace YourGoals.API.Extensions;

public static class ControllerExtensions
{
    public static IActionResult GetResult(this ControllerBase controller, IEnumerable<IError> errors)
    {
        var error = errors.FirstOrDefault();

        return error switch
        {
            HttpStatusCodeError err => controller.FromResult(err),
            _ => controller.BadRequest(errors),
        };
    }

    private static IActionResult FromResult(this ControllerBase controller, HttpStatusCodeError error)
    {
        return error.StatusCode switch
        {
            HttpStatusCode.NotFound => controller.NotFound(error),
            HttpStatusCode.BadRequest => controller.BadRequest(error),
            HttpStatusCode.Unauthorized => controller.Unauthorized(),
            _ => throw new Exception(error.Message),
        };
    }
}