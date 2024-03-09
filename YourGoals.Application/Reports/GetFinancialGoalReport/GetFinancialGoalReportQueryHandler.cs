using System.Net;

using MediatR;

using YourGoals.Core.Results;
using YourGoals.Domain.FinancialGoals.Errors;
using YourGoals.Domain.FinancialGoals.Interfaces;
using YourGoals.Application.Abstractions.Errors;
using YourGoals.Application.Abstractions.EmailApi;
using YourGoals.Application.Reports.Services;

namespace YourGoals.Application.Reports.GetFinancialGoalReport;

public sealed class GetFinancialGoalReportQueryHandler : IRequestHandler<GetFinancialGoalReportQuery, Result>
{
    private readonly IFinancialGoalRepository _financialGoalRepository;
    private readonly IReportService _reportService;
    private readonly IEmailApi _mailApi;

    public GetFinancialGoalReportQueryHandler(
        IFinancialGoalRepository financialGoalRepository,
        IReportService reportService,
        IEmailApi mailApi)
    {
        _financialGoalRepository = financialGoalRepository;
        _reportService = reportService;
        _mailApi = mailApi;
    }

    public async Task<Result> Handle(GetFinancialGoalReportQuery request, CancellationToken cancellationToken)
    {
        var financialGoal = await _financialGoalRepository.GetByIdAsync(request.FinancialGoalId, cancellationToken);

        if (financialGoal is null)
            return Result.Fail(new HttpStatusCodeError(FinancialGoalErrors.NotFound, HttpStatusCode.NotFound));

        var reportResult = _reportService.GenerateFinancialGoalReport(financialGoal);

        if (!reportResult.Success)
            return Result.Fail(new HttpStatusCodeError("colocar algo aqui", "colocar algo aqui"));

        var emailInputModel =
            new EmailInputModel(request.Email, $"{financialGoal.Name} - Relatório de Transações", reportResult.Value);

        var mailResult = await _mailApi.SendEmail(emailInputModel);

        if (!mailResult.Success)
            return Result.Fail(new HttpStatusCodeError("colocar algo aqui", "colocar algo aqui"));

        return Result.Ok();
    }
}
