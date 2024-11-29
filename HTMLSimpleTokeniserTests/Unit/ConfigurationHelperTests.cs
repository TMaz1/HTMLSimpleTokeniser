using System;
using System.IO;
using System.Text.Json;
using Xunit;
using HTMLSimpleTokeniser.Helpers;
using HTMLSimpleTokeniser.Services;


namespace HTMLSimpleTokeniserTests.Helpers;

public class ConfigurationHelperTests : IDisposable
{
    private readonly string testConfigPath;
    private const string ConfigKey = "HtmlFilePath";
    private readonly string fallbackFilePath;
    private readonly Logger logger;

    public class JsonKeyPair
    {
        public string? HtmlFilePath { get; set; }
    }

    public ConfigurationHelperTests()
    {
        logger = Logger.Instance;
        testConfigPath = Path.Combine(Path.GetTempPath(), "appsettings.tests.json");
        fallbackFilePath = Path.Combine(Path.GetTempPath(), "fallback.html");

        // ensure file exists
        if (!File.Exists(testConfigPath))
        {
            JsonKeyPair appTestSettings = new JsonKeyPair
            {
                HtmlFilePath = "Data/sample.html"
            };

            string? appTestSettingsJson = JsonSerializer.Serialize(appTestSettings);
            File.WriteAllText(testConfigPath, appTestSettingsJson);
        }
    }

    [Fact]
    public void GetHtmlFilePath_ReturnsPathFromConfig()
    {
        var expectedPath = "Data/sample.html";
        Directory.CreateDirectory("Data");
        File.WriteAllText(expectedPath, "<p>HTML Content</p>");

        var result = ConfigurationHelper.GetHtmlFilePath(testConfigPath, ConfigKey, fallbackFilePath);

        Assert.Equal(expectedPath, result);
        Assert.True(File.Exists(expectedPath));
        logger.Log("UNIT TESTING: GetHtmlFilePath_ReturnsPathFromConfig passed");
    }

    [Fact]
    public void GetHtmlFilePath_CreateFallbackFileWhenConfigKeyNotFound()
    {
        var result = ConfigurationHelper.GetHtmlFilePath(testConfigPath, "wrongKey", fallbackFilePath);

        Assert.Equal(fallbackFilePath, result);
        Assert.True(File.Exists(fallbackFilePath));
        Assert.Contains("<p>Hello World</p>", File.ReadAllText(fallbackFilePath));
        logger.Log("UNIT TESTING: GetHtmlFilePath_CreateFallbackFileWhenFileNotFound passed");
    }

    [Fact]
    public void GetHtmlFilePath_ThrowsExceptionWhenConfigFileNotFound()
    {
        var invalidConfigPath = "Invalid/appsettings.json";
        Assert.Throws<FileNotFoundException>(() => ConfigurationHelper.GetHtmlFilePath(invalidConfigPath, ConfigKey, fallbackFilePath));
        logger.Log("UNIT TESTING: GetHtmlFilePath_ThrowsExceptionWhenConfigFileNotFound passed");
    }

    public void Dispose()
    {
        // cleanup
        if (File.Exists(testConfigPath))
        {
            File.Delete(testConfigPath);
        }

        if (File.Exists(fallbackFilePath))
        {
            File.Delete(fallbackFilePath);
        }
    }
}