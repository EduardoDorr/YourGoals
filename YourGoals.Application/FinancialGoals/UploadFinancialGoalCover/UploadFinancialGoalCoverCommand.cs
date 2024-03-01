using MediatR;

using YourGoals.Core.Results;

namespace YourGoals.Application.FinancialGoals.UploadFinancialGoalCover;

public sealed record UploadFinancialGoalCoverCommand(Guid FinancialGoalId, string? CoverImage) : IRequest<Result>;