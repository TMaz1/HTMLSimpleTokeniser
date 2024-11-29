using HTMLSimpleTokeniser.Models;
using HTMLSimpleTokeniser.Services;

namespace HTMLSimpleTokeniser.HTMLSimpleTokeniserTests.Unit;

public class HtmlTokeniserTests : IDisposable
{
    private readonly string testFilePath;
    private readonly Logger logger;

    public HtmlTokeniserTests()
    {
        logger = Logger.Instance;

        testFilePath = Path.Combine(Path.GetTempPath(), "unit_test.html");

        // ensure file exists
        if (!File.Exists(testFilePath))
        {
            string htmlContent = @"
                <html>
                    <body>
                        <h1>Hello World</h1>
                    </body>
                </html>";

            File.WriteAllText(testFilePath, htmlContent);
        }
    }

    [Fact]
    public void UnitTestFilePath_ShouldExist()
    {
        Assert.True(File.Exists(testFilePath));
    }

    [Fact]
    public void HtmlTokeniser_ShouldParseCorrectly()
    {
        var tokeniser = new HtmlTokeniser(testFilePath);
        var tokens = new List<Token>(tokeniser.Tokenise());

        Assert.Equal(7, tokens.Count);
        Assert.Equal(TokeniserConfig.StartTag, tokens[0].Type);
        Assert.Equal("html", tokens[0].Value);

        Assert.Equal(TokeniserConfig.StartTag, tokens[1].Type);
        Assert.Equal("body", tokens[1].Value);

        Assert.Equal(TokeniserConfig.StartTag, tokens[2].Type);
        Assert.Equal("h1", tokens[2].Value);

        Assert.Equal(TokeniserConfig.TextContent, tokens[3].Type);
        Assert.Equal("Hello World", tokens[3].Value);

        Assert.Equal(TokeniserConfig.EndTag, tokens[4].Type);
        Assert.Equal("h1", tokens[4].Value);

        Assert.Equal(TokeniserConfig.EndTag, tokens[5].Type);
        Assert.Equal("body", tokens[5].Value);

        Assert.Equal(TokeniserConfig.EndTag, tokens[6].Type);
        Assert.Equal("html", tokens[6].Value);

        logger.Log("UNIT TESTING: HtmlTokeniser_ShouldParseCorrectly passed");
    }

    [Fact]
    public void HtmlTokeniser_ShouldThrowFileNotFoundException()
    {
        Assert.Throws<FileNotFoundException>(() => new HtmlTokeniser("not_real.html"));
        logger.Log("UNIT TESTING: HtmlTokeniser_ShouldThrowFileNotFoundException passed");
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
