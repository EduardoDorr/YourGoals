using YourGoals.Application.Reports.Models;
using YourGoals.Core.Results;

namespace YourGoals.Application.Reports.Service;

public interface IMailApi
{
    Task<Result> SendEmail(EmailInputModel email);
}