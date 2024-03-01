using FluentValidation;

namespace YourGoals.Application.Transactions.CreateTransaction;

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(r => r.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be valid");

        RuleFor(r => r.TransactionDate)
            .GreaterThan(DateTime.Now)
            .WithMessage("transaction date must be valid");

        RuleFor(r => r.Type)
            .IsInEnum()
            .WithMessage("Type must be valid");
    }
}