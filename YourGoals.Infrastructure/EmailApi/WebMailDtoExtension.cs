using YourGoals.Application.Abstractions.EmailApi;

namespace YourGoals.Infrastructure.EmailApi;

public static class WebMailDtoExtension
{
    private const string ORIGIN = "YourGoals";

    public static WebMailDto FromModel(this EmailInputModel model)
    {
        var body = "<h2><em><strong><span style=\"font-family:Verdana,Geneva,sans-serif\">Segue anexo o seu relat&oacute;rio de transa&ccedil;&otilde;es para a sua meta financeira!!</span></strong></em></h2>";

        return new WebMailDto(
            ORIGIN,
            model.Destiny,
            model.Subject,
            body,
            model.Attachment
            );
    }
}