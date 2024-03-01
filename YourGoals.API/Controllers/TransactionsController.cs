using Microsoft.AspNetCore.Mvc;

using MediatR;

using YourGoals.Core.Results;
using YourGoals.API.Extensions;
using YourGoals.Application.Transactions.GetTransaction;
using YourGoals.Application.Transactions.GetTransactions;
using YourGoals.Application.Transactions.CreateTransaction;
using YourGoals.Application.Transactions.DeleteTransaction;

namespace YourGoals.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class TransactionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransactionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetTransactionsQuery query)
    {
        var result = await _mediator.Send(query);

        return result.Match(
        onSuccess: Ok,
        onFailure: this.GetResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetTransactionQuery(id);

        var result = await _mediator.Send(query);

        return result.Match(
        onSuccess: Ok,
        onFailure: this.GetResult);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionCommand command)
    {
        var result = await _mediator.Send(command);

        return result.Match(
        onSuccess: value => CreatedAtAction(nameof(GetById), new { id = value }, command),
        onFailure: this.GetResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteTransactionCommand(id);

        var result = await _mediator.Send(command);

        return result.Match(
        onSuccess: NoContent,
        onFailure: this.GetResult);
    }
}