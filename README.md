﻿# LothiumLgger [![NuGet Version](https://img.shields.io/nuget/v/LothiumLogger.svg?style=flat)](https://www.nuget.org/packages/LothiumLogger/) [![NuGet Downloads](https://img.shields.io/nuget/v/LothiumLogger.svg?style=flat)](https://www.nuget.org/packages/LothiumLogger/)

LothiumLogger is a simple easy Logger Library for .Net applications written entirely in C# for fun, it offers a basic and easy syntax and give flexybility to the final user.

For create a new Logger istance you will need to use the LoggerConfiguration class:

```csharp
var logger = new LoggerConfiguration()
    .WriteToConsole()
    .WriteToFile(fileName: "Log", minimumLevel: LogLevel.Normal)
    .WriteToFile(fileName: "Err_Log", restrictedToLevel: LogLevel.Err)
    .Build();
```

After the creation of the new logger istance, there is 6 different methods to write a new log message:

```csharp
// Defualt base message
logger.Write("Log Message");

// Debug message when testing code
logger.Debug("Debug Log Message");

// Info message
logger.Information("Info Log Message");

// Warning message
logger.Warning("Warn Log Message");

// Error message
logger.Error("Err Log Message");

// Fatal message
logger.Fatal("Fatal Log Message");
```

The library gave the user the ability to seraize an object inside the log message to keep track of real time changes:
```csharp
Person person = new Person() 
{
    Name = "Andrea",
    Surname = "Santinato",
    Age = 25
};

logger.Information("Created New Person: {@Person}", person);
```
For this methods to work the parameter's name must be equal to the object class's type, in this case the class name is 'Person'

If an error occured while executing the code and need to keep track of exception, the library offer the ability to serialize an exception object directly to the log message:
```csharp
try 
{
    // Some Code Here...
}
catch (Exception exception)
{
    // Log the exception
    logger.Error(exception);
}
Exception exception = new Exception("This is a test exception");
```

### Getting started

You can install LothiumLogger directly from NuGet using the NuGet Manager inside Visual Studio or with this command:

```
dotnet add package LothiumLogger
```

### Bug Reports

If you want to contribute to this project, you can send a pull request to the GitHub Repository and i'll be happy to add it to the project.
In The feature i'll separate the Master Branch from the Develop Branch

I welcome any type of bug reports and suggestions through my GitHub [Issue Tracker](https://github.com/AndreaSantinato/LothiumLogger/issues).

_LothiumLogger is copyright &copy; 2023 - Provided under the [GNU General Public License v3.0](https://github.com/AndreaSantinato/LothiumLogger/blob/main/LICENSE)._
