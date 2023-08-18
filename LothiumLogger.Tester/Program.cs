using LothiumLogger;
using LothiumLogger.Enumerations;
using LothiumLogger.Tester.TestModels;

Console.WriteLine("{--- Starting Point ---}");

// Create a new logger istance
var logger = new LoggerConfiguration()
    .WriteToConsole()
    .WriteToFile(fileName: "Log", minimumLevel: LogLevel.Normal)
    .WriteToFile(fileName: "Err_Log", restrictedToLevel: LogLevel.Err)
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
logger.Information("Person Name: {@Name} {@Surname}", person);
logger.Information("Person Age: {@Age}", person);

// Write an advanced log from an exception
logger.Error("Test Exception", new Exception("This is a first test exception"));
logger.Fatal("Fatal Test Exception", new Exception("This is a second test exception"));

Console.WriteLine("{--- Ending Point ---}");