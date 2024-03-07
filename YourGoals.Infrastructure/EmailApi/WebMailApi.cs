using System.Text;
using System.Net.Mime;
using System.Text.Json;

using YourGoals.Core.Results;
using YourGoals.Core.Results.Errors;
using YourGoals.Application.Abstractions.EmailApi;

namespace YourGoals.Infrastructure.EmailApi;

public sealed class WebMailApi : IEmailApi
{
    private readonly IHttpClientFactory _httpClientFactory;

    public WebMailApi(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Result> SendEmail(EmailInputModel email)
    {
        var webMailDto = email.FromModel();

        using var httpClient = _httpClientFactory.CreateClient("WebMailApi");

        var json =
            new StringContent(
                JsonSerializer.Serialize(webMailDto),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

        using HttpResponseMessage httpResponse =
            await httpClient.PostAsync("email", json);

        if (httpResponse.IsSuccessStatusCode)
            return Result.Ok();

        return Result.Fail(new Error("WebMailApi.Error", await httpResponse.Content.ReadAsStringAsync()));
    }
}