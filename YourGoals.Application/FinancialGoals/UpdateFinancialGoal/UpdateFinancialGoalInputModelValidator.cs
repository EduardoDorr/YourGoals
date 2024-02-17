using FluentValidation;

namespace YourGoals.Application.FinancialGoals.UpdateFinancialGoal;

public class UpdateFinancialGoalInputModelValidator : AbstractValidator<UpdateFinancialGoalInputModel>
{
    public UpdateFinancialGoalInputModelValidator()
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
            .When(value => value is not null)
            .WithMessage("InterestRate must be valid");

        RuleFor(r => r.Deadline)
            .GreaterThan(DateTime.Now)
            .When(value => value is not null)
            .WithMessage("Deadline must be valid");
    }
}