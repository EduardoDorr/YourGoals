using FluentValidation;

namespace YourGoals.Application.FinancialGoals.CreateFinancialGoal;

public sealed class CreateFinancialGoalCommandValidator : AbstractValidator<CreateFinancialGoalCommand>
{
    public CreateFinancialGoalCommandValidator()
    {
        RuleFor(r => r.Name)
            .MinimumLength(3).WithMessage("Name must have a minimum of 3 characters")
            .MaximumLength(50).WithMessage("Name must have a maximum of 50 characters");

        RuleFor(r => r.GoalAmount)
            .GreaterThan(0)
            .WithMessage("GoalAmount must be valid");

        RuleFor(r => r.InitialAmount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("InitialAmount must be valid");

        RuleFor(r => r.InterestRate)
            .GreaterThan(0)
            .When(r => r.InterestRate is not null)
            .WithMessage("InterestRate must be valid");

        RuleFor(r => r.Deadline)
            .GreaterThan(DateTime.Now)
            .When(r => r.Deadline is not null)
            .WithMessage("Deadline must be valid");
    }
}