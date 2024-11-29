using HTMLSimpleTokeniser.Models;
using HTMLSimpleTokeniser.Services;

namespace HTMLSimpleTokeniser.HTMLSimpleTokeniserTests.Integration;

public class IntegrationTests : IDisposable
{
    private readonly string testFilePath;
    private readonly Logger logger;

    public IntegrationTests()
    {
        logger = Logger.Instance;

        testFilePath = Path.Combine(Path.GetTempPath(), "integration_test.html");

        // ensure file exists
        if (!File.Exists(testFilePath))
        {
            string htmlContent = @"
                <div>
                    <p>Sample Text</p>
                </div>";

            File.WriteAllText(testFilePath, htmlContent);
        }
    }

    [Fact]
    public void TestFilePath_ShouldExist()
    {
        Assert.True(File.Exists(testFilePath));
    }

    [Fact]
    public void HtmlTokeniser_ShouldProcessHtmlFileCorrectly()
    {

        var tokeniser = new HtmlTokeniser(testFilePath);
        IEnumerable<Token> tokens = tokeniser.Tokenise();

        Assert.NotNull(tokens);

        // verify that the tokens match the expected sequence: a start tag token (e.g., <p>), a text content token (e.g., Sample Text), an end tag token (e.g., </p>)
        Assert.Collection(tokens,
            token => Assert.Equal(TokeniserConfig.StartTag, token.Type),
            token => Assert.Equal(TokeniserConfig.StartTag, token.Type),
            token => Assert.Equal(TokeniserConfig.TextContent, token.Type),
            token => Assert.Equal(TokeniserConfig.EndTag, token.Type),
            token => Assert.Equal(TokeniserConfig.EndTag, token.Type)
        );

        logger.Log("INTEGRATION TESTING: HtmlTokeniser_ShouldProcessHtmlFileCorrectly passed");
    }

    public void Dispose()
    {
        // cleanup
        if (File.Exists(testFilePath))
        {
            File.Delete(testFilePath);
        }
    }
}
