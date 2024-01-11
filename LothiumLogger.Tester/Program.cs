// LothiumLogger Class
using LothiumLogger;
using LothiumLogger.Enumerations;
using LothiumLogger.Records;
using LothiumLogger.Tester.TestModels;

// Starting Point For The Tests
Console.WriteLine("{--- Starting Point ---}");

// Create a new logger instance
var logger = new Logger(settings => {
    // Default Console Sink For Log Events
    settings.AddConsoleSink(options => {
        options.Enabled = true;
        options.DateFormat = LogDateFormatEnum.Standard;
        options.LoggingRule = new LoggingRule(LogLevelEnum.Debug, false);
        options.ConsoleTheme = ConsoleThemeEnum.Default;
    });
    
    var logFileDir = Path.Combine(Directory.GetCurrentDirectory(), "Logs");

    // Default File Sink For Log Events
    settings.AddFileSink(options => {
        options.Enabled = true;
        options.DateFormat = LogDateFormatEnum.Standard;
        options.LoggingRule = new LoggingRule(LogLevelEnum.Normal, false);
        options.FileRule = new FileRule(LogFileTypeEnum.GenericLog, logFileDir);
    });

    // Specific File Sink For Debug Log Events
    settings.AddFileSink(options => {
        options.Enabled = true;
        options.DateFormat = LogDateFormatEnum.Standard;
        options.LoggingRule = new LoggingRule(LogLevelEnum.Debug, true);
        options.FileRule = new FileRule(LogFileTypeEnum.LothiumLog, logFileDir, "DebugLogs");
    });

    // Specific File Sink For Error Log Events
    settings.AddFileSink(options => {
        options.Enabled = true;
        options.DateFormat = LogDateFormatEnum.Standard;
        options.LoggingRule = new LoggingRule(LogLevelEnum.Err, true);
        options.FileRule = new FileRule(LogFileTypeEnum.LothiumLog, logFileDir, "ErrorLogs");
    });
});

// Write a sample log messages
logger.Write("Normal Message");
logger.Debug("Debug Message");
logger.Information("Info Message");
logger.Warning("Warn Message");
logger.Error("Err Message");
logger.Fatal("Fatal Message");

// Write an advanced log messages
var person = new Person() 
{
    Name = "Andrea",
    Surname = "Santinato",
    Age = 25
};
logger.Information("Created Person: {@Person}", person);
logger.Information("Person Name: {@Person.Name} {@Person.Surname}", person);
logger.Information("Person Age: {@Person.Age}", person);

// Serialize an independant variable
var example = "Test Variable";
logger.Debug("Variable Value: {@example}", example);

// Write an advanced log from an exception
logger.Error("Test Exception", new Exception("This is a first test exception"));
logger.Fatal("Fatal Test Exception", new Exception("This is a second test exception"));

// Ending Point For The Tests
Console.WriteLine("{--- Ending Point ---}");