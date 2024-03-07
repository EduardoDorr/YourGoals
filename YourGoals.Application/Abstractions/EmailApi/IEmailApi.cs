using YourGoals.Core.Results;

namespace YourGoals.Application.Abstractions.EmailApi;

public interface IEmailApi
{
    Task<Result> SendEmail(EmailInputModel email);
}