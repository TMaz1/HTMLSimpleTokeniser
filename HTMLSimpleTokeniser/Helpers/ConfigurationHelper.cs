using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace HTMLSimpleTokeniser.Helpers;
public static class ConfigurationHelper
{
    public static string GetHtmlFilePath(string configFilePath, string configKey, string fallbackHtmlFilePath)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile(configFilePath, optional: false, reloadOnChange: true)
            .Build();

        string fallbackFilePath = fallbackHtmlFilePath ?? "Data/default.html";
        string htmlFilePath = configuration[configKey] ?? fallbackFilePath;

        if (!File.Exists(htmlFilePath))
        {
            var fallbackHtmlContent = @"
                <div>
                    <p>Hello World</p>
                </div>";

            Directory.CreateDirectory(Path.GetDirectoryName(htmlFilePath)!);
            File.WriteAllText(htmlFilePath, fallbackHtmlContent);
        }

        return htmlFilePath;
    }
}
