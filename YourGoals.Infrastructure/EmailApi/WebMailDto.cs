using System.Text.Json.Serialization;

namespace YourGoals.Infrastructure.EmailApi;

public record WebMailDto(
    [property: JsonPropertyName("origin")] string Origin,
    [property: JsonPropertyName("destiny")] string Destiny,
    [property: JsonPropertyName("subject")] string Subject,
    [property: JsonPropertyName("body")] string Body,
    [property: JsonPropertyName("attachment")] string? Attachment
);