using HTMLSimpleTokeniser.Services;

namespace HTMLSimpleTokeniser.HTMLSimpleTokeniserTests.Unit;

public class LoggerTests
{
    [Fact]
    public void Log_ShouldWriteMessageToLogFile()
    {
        var logger = Logger.Instance;
        logger.ClearLog();

        logger.Log("Test message");

        var logs = logger.ReadLogs();
        Assert.Contains("Test message", logs);
    }

    [Fact]
    public void ClearLog_ShouldClearLogFile()
    {
        var logger = Logger.Instance;

        logger.Log("Another test message");
        logger.ClearLog();

        var logs = logger.ReadLogs();
        Assert.Equal(string.Empty, logs);
    }
}
