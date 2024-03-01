using MediatR;

using YourGoals.Core.Results;

namespace YourGoals.Application.FinancialGoals.UploadFinancialGoalCover;

public sealed class UploadFinancialGoalCoverCommandHandler : IRequestHandler<UploadFinancialGoalCoverCommand, Result>
{
    public Task<Result> Handle(UploadFinancialGoalCoverCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}