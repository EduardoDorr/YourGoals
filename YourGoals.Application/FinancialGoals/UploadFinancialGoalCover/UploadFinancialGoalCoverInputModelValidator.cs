using FluentValidation;

namespace YourGoals.Application.FinancialGoals.UploadFinancialGoalCover;

public sealed class UploadFinancialGoalCoverInputModelValidator : AbstractValidator<UploadFinancialGoalCoverInputModel>
{
    private readonly int MAX_SIZE_IN_MB = 1 * 1024 * 1024;

    public UploadFinancialGoalCoverInputModelValidator()
    {
        RuleFor(r => r.CoverImage)
            .Length(1, MAX_SIZE_IN_MB)
            .WithMessage("The image size exceeds the maximum upload size for this site (1 MB)");
    }
}