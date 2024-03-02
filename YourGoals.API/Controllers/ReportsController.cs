using Microsoft.AspNetCore.Mvc;

using MediatR;

using YourGoals.Core.Results;
using YourGoals.API.Extensions;
using YourGoals.Application.Reports.GetFinancialGoalReport;

namespace YourGoals.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ReportsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetFinancialGoalReportQuery query)
    {
        var result = await _mediator.Send(query);

        return result.Match(
        onSuccess: Accepted,
        onFailure: this.GetResult);
    }
}