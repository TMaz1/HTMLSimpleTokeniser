using HTMLSimpleTokeniser.Services;
using HTMLSimpleTokeniser.Helpers;

namespace HTMLSimpleTokeniser;

class Program
{
    static void Main(string[] args)
    {
        var logger = Logger.Instance;
        string htmlFilePath = ConfigurationHelper.GetHtmlFilePath("Configuration/appsettings.json", "HtmlFilePath", "Data/default.html");

        logger.Log($"Application started. Processing file: {htmlFilePath}");

        try
        {
            var tokeniser = new HtmlTokeniser(htmlFilePath);
            var tokens = tokeniser.Tokenise();

            logger.Log("Tokens generated successfully.");
            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }
        }
        catch (Exception e)
        {
            logger.Log($"Error: {e.Message}");
        }

        logger.Log("Application finished execution.");
    }
}
