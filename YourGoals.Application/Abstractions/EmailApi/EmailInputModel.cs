namespace YourGoals.Application.Abstractions.EmailApi;

public sealed record EmailInputModel
(
    string Destiny,
    string Subject,
    string? Attachment = default
);