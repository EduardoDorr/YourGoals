using MediatR;

using YourGoals.Core.Results;
using YourGoals.Core.Interfaces;
using YourGoals.Domain.FinancialGoals.Entities;
using YourGoals.Domain.FinancialGoals.Interfaces;
using YourGoals.Domain.FinancialGoals.DomainErrors;

namespace YourGoals.Application.FinancialGoals.CreateFinancialGoal;

internal sealed class CreateFinancialGoalCommandHandler : IRequestHandler<CreateFinancialGoalCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFinancialGoalRepository _financialGoalRepository;

    public CreateFinancialGoalCommandHandler(IUnitOfWork unitOfWork, IFinancialGoalRepository financialGoalRepository)
    {
        _unitOfWork = unitOfWork;
        _financialGoalRepository = financialGoalRepository;
    }

    public async Task<Result<Guid>> Handle(CreateFinancialGoalCommand request, CancellationToken cancellationToken)
    {
        var financialGoal = FinancialGoal
            .CreateBuilder(request.Name, request.GoalAmount)
            .WithInitialAmount(request.InitialAmount)
            .WithInterestRate(request.InterestRate)
            .WithDeadline(request.Deadline)
            .Build();

        _financialGoalRepository.Create(financialGoal);

        var created = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!created)
            return Result.Fail<Guid>(FinancialGoalErrors.CannotBeCreated);

        return Result.Ok(financialGoal.Id);
    }
}