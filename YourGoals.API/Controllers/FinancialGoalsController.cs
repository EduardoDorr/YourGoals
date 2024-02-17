using MediatR;
using Microsoft.AspNetCore.Mvc;

using YourGoals.Core.Results;
using YourGoals.API.Extensions;
using YourGoals.Application.FinancialGoals.GetFinancialGoal;
using YourGoals.Application.FinancialGoals.GetFinancialGoals;
using YourGoals.Application.FinancialGoals.CreateFinancialGoal;
using YourGoals.Application.FinancialGoals.UpdateFinancialGoal;
using YourGoals.Application.FinancialGoals.DeleteFinancialGoal;

namespace YourGoals.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class FinancialGoalsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FinancialGoalsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetFinancialGoalsQuery query)
    {
        var result = await _mediator.Send(query);

        return result.Match(
        onSuccess: Ok,
        onFailure: this.GetResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetFinancialGoalQuery(id);

        var result = await _mediator.Send(query);

        return result.Match(
        onSuccess: Ok,
        onFailure: this.GetResult);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFinancialGoalCommand command)
    {
        var result = await _mediator.Send(command);

        return result.Match(
        onSuccess: value => CreatedAtAction(nameof(GetById), new { id = value }, command),
        onFailure: this.GetResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFinancialGoalInputModel inputModel)
    {
        var command =
            new UpdateFinancialGoalCommand(
                id,
                inputModel.Name,
                inputModel.GoalAmount,
                inputModel.InitialAmount,
                inputModel.InterestRate,
                inputModel.Deadline);

        var result = await _mediator.Send(command);

        return result.Match(
        onSuccess: NoContent,
        onFailure: this.GetResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteFinancialGoalCommand(id);

        var result = await _mediator.Send(command);

        return result.Match(
        onSuccess: NoContent,
        onFailure: this.GetResult);
    }
}