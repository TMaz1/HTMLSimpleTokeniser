using System.Text;
using HTMLSimpleTokeniser.Models;

namespace HTMLSimpleTokeniser.Services;

public class HtmlTokeniser
{
    private readonly string htmlContent;
    private int position;

    /// <summary>
    /// Initialises the tokeniser by loading HTML content from a file
    /// </summary>
    /// <param name="filePath">The path to the HTML file to be tokenised</param>
    /// <exception cref="FileNotFoundException">Thrown if specified file doesn't exist</exception>
    public HtmlTokeniser(string filePath)
    {
        var logger = Logger.Instance;
        if (!File.Exists(filePath))
        {
            string errorMsg = $"File not found: {filePath}";
            logger.Log(errorMsg);
            throw new FileNotFoundException(errorMsg);
        }

        logger.Log($"Reading file: {Path.GetFileName(filePath)}");
        htmlContent = File.ReadAllText(filePath);
        position = 0;
    }

    /// <summary>
    /// Tokenises the HTML content into a collection of tokens
    /// </summary>
    /// <returns>A list of tokens representing HTML elements and text</returns>
    public IEnumerable<Token> Tokenise()
    {
        var logger = Logger.Instance;
        logger.Log("Tokenisation started.");
        List<Token> tokens = [];

        while (position < htmlContent.Length)
        {
            if (htmlContent[position] == '<')
            {
                if (htmlContent[position + 1] == '/')
                {
                    tokens.Add(ReadEndTag());
                }
                else
                {
                    tokens.Add(ReadStartTag());
                }
            }
            else if (!char.IsWhiteSpace(htmlContent[position]))
            {

                tokens.Add(ReadTextContent());
            }
            else
            {
                position++;
            }
        }

        logger.Log($"Tokenisation completed. Total tokens: {tokens.Count}");
        return tokens;
    }


    /// <summary>
    /// Reads a HTML start tag and creates a token
    /// </summary>
    /// <returns>A token (type as start tag, and its value) representing the start tag</returns>
    private Token ReadStartTag()
    {
        position++; // skips '<'
        StringBuilder tagName = ReadCharsUntilStoppingChar('>');
        position++; // skips '>'
        return new Token(TokeniserConfig.StartTag, tagName.ToString().Trim());
    }

    /// <summary>
    /// Reads a HTML end tag and creates a token
    /// </summary>
    /// <returns>A token (type as end tag, and its value) representing the end tag</returns>
    private Token ReadEndTag()
    {
        position += 2; // skips '</'
        StringBuilder tagName = ReadCharsUntilStoppingChar('>');
        position++; // skips '>'
        return new Token(TokeniserConfig.EndTag, tagName.ToString().Trim());
    }

    /// <summary>
    /// Reads text content between HTML tags and creates a token
    /// </summary>
    /// <returns>A token representing the text content</returns>
    private Token ReadTextContent()
    {
        StringBuilder textContent = ReadCharsUntilStoppingChar('<');
        return new Token(TokeniserConfig.TextContent, textContent.ToString().Trim());
    }

    /// <summary>
    /// Reads characters from the current position until a specified stopping character is encountered
    /// </summary>
    /// <param name="stoppingChar">The character at which to stop reading.</param>
    /// <returns>String containing the characters read; from start character to stopping character</returns>
    private StringBuilder ReadCharsUntilStoppingChar(char stoppingChar)
    {
        var tokenContent = new StringBuilder();
        while (position < htmlContent.Length && htmlContent[position] != stoppingChar)
        {
            tokenContent.Append(htmlContent[position]);
            position++;
        }
        return tokenContent;
    }

}