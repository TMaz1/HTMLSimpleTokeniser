namespace HTMLSimpleTokeniser.Services;

public class Logger
{
    private static readonly Lazy<Logger> _instance = new(() => new Logger());
    private readonly string _logFilePath;
    private readonly object _lock = new();

    private Logger()
    {
        _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.log");
        if (!File.Exists(_logFilePath))
        {
            File.Create(_logFilePath).Dispose();
        }
    }

    public static Logger Instance => _instance.Value;

    public void Log(string message)
    {
        var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
        lock (_lock)
        {
            File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
        }
        Console.WriteLine(logEntry);
    }

    public void ClearLog()
    {
        lock (_lock)
        {
            File.WriteAllText(_logFilePath, string.Empty);
            Console.WriteLine("Log file cleared.");
        }
    }

    public string ReadLogs()
    {
        // read the contents of the log file
        lock (_lock)
        {
            return File.ReadAllText(_logFilePath);
        }
    }
}
