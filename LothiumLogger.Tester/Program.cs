// LothiumLogger Class
using LothiumLogger;
using LothiumLogger.Enumerations;
using LothiumLogger.Tester.TestModels;

// Starting Point For The Tests
Console.WriteLine("{--- Starting Point ---}");

// Create a new logger instance
var logger = new LoggerConfiguration()
    .AddConsoleSinker(theme: ConsoleThemeEnum.LothiumDark)
    .AddFileSinker(minimumLogLevel: LogLevelEnum.Normal)
    .AddFileSinker(name: "DebugLogs", restrictedToLogLevel: LogLevelEnum.Debug, typeOfGeneratedFile: LogFileTypeEnum.LothiumLog)
    .AddFileSinker(name: "ErrorLogs", restrictedToLogLevel: LogLevelEnum.Err, typeOfGeneratedFile: LogFileTypeEnum.LothiumLog)
    .Build();

// Write a sample log messages
logger.Write("Normal Message");
logger.Debug("Debug Message");
logger.Information("Info Message");
logger.Warning("Warn Message");
logger.Error("Err Message");
logger.Fatal("Fatal Message");

// Write an advanced log messages
Person person = new Person() 
{
    Name = "Andrea",
    Surname = "Santinato",
    Age = 25
};
logger.Information("Created Person: {@Person}", person);
logger.Information("Person Name: {@Person.Name} {@Person.Surname}", person);
logger.Information("Person Age: {@Person.Age}", person);

// Serialize an independant variable
string example = "Test Variable";
logger.Debug("Variable Value: {@example}", example);

// Write an advanced log from an exception
logger.Error("Test Exception", new Exception("This is a first test exception"));
logger.Fatal("Fatal Test Exception", new Exception("This is a second test exception"));

// Ending Point For The Tests
Console.WriteLine("{--- Ending Point ---}");