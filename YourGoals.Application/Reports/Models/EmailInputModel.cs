namespace YourGoals.Application.Reports.Models;

public sealed record EmailInputModel
(
    string Destiny,
    string Subject,
    string Attachment
);