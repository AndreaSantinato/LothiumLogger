# LothiumLgger [![NuGet Version](https://img.shields.io/nuget/v/LothiumLogger.svg?style=flat)](https://www.nuget.org/packages/LothiumLogger/) [![NuGet Downloads](https://img.shields.io/nuget/v/LothiumLogger.svg?style=flat)](https://www.nuget.org/packages/LothiumLogger/)

### Getting Started

LothiumLogger is a simple easy-to-use Logger Library for .Net applications written entirely in C# for fun, it offers a basic and easy syntax and give flexybility to the final user.
The idea behind this project is to create a simple interactive way to catch events from your application in the simpliest and easiest way possible just by instance a variable and use it!
You can install LothiumLogger directly from NuGet using the NuGet Manager inside Visual Studio or with this command:

```
dotnet add package LothiumLogger
```

To start using this library is essential to create a new Logger instance using the dedicated LoggerConfiguration class like this example:

```csharp
var logger = new LoggerConfiguration()
    .AddConsoleSinker()
    .Build();
```

There library is designed to have multiple type of sinkers, in this version there are two: the ConsoleSinker and the FileSinker, each of them have their unique functionalities.
For using more of one sinker just add them with the configuration object:

```csharp
var logger = new LoggerConfiguration()
    .AddConsoleSinker()
    .AddFileSinker()
    .Build();
```

### Console Sinker

The console's sink offer the ability to write logs directly to the IDE's debug console to track events while running your projects.
The 'AddConsoleSink' have this property to be inizialied:
    - minimumLogLevel: indicate the minimum accepted logging level for the sink.
    - restrictedToLogLevel: indicate the only accepted loggin level for the sink, this value overwrite the minimum one.
    - theme: indicate the console's theme that will be used

### File Sinker

The file's sink offer the ability to write logs in one or more files on one or more folders.
The 'AddFileSink' have this property to be inizialized:
    - name: indicate the name of for the final created file
    - path: indicate the path where the final file will be created
    - minimumLogLevel: indicate the minimum accepted logging level for the sink.
    - restrictedToLogLevel: indicate the only accepted loggin level for the sink, this value overwrite the minimum one.
    - typeOfGeneratedFile: indicate the type of the final created file (for now there is only two types: GenericLog & LothiumLog)

### Custom Sinker

The configuration object offer the ability to create and add a customized sinker, the only thing to do is create a new class and extender the ISinkers interface:

```csharp
using LothiumLogger.Sinkers;

class CustomSinker() : ISinkers 
{
    // Interface's Property And Methods Implementation
}

class Main() 
{
    var logger = new LoggerConfiguration(new CustomSinker())
        .AddCustomSinker()
        .Build();
}
```

### Logger Methods

After the created a new logger instance, there are 6 different type of logging level: Normal, Debug, Information, Warning, Error & Fatal.
Each one have their separated methods for writing new logging events:

```csharp
// Defualt normal message
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

### Object Serialization

The library gave the user the ability to seraize an object inside the log message or a one or more property to keep track of real time changes.
For serialize an object or a Property Name is required the Type Name likes this example:

```csharp
Person person = new Person() 
{
    Name = "Andrea",
    Surname = "Santinato",
    Age = 25
};

logger.Information("Created Person: {@Person}", person);
logger.Information("Person Name: {@Person.Name} {@Person.Surname}", person);
logger.Information("Person Age: {@Person.Age}", person);
```

For this methods to work the parameter's name must be equal to the object class's type, in this case the class name is 'Person'
When you need to serialize one or more object property, the library need a combination of the object's type name and the property name
In the end the library can serialize an external independent variable passed to the methods:

```csharp
string example = "Test Variable";
logger.Debug("Variable Value: {@example}", example);
```

If an error occured while executing the code and need to keep track of exception, the library offer the ability to serialize an exception object directly to the log message:
```csharp
try 
{
    // Some Code Here...
    throw new Exception("Test exception example!!");
}
catch (Exception exception)
{
    // Log the exception
    logger.Error(exception);
}
```

### Bug Reports

If you want to contribute to this project, you can send a pull request to the GitHub Repository and i'll be happy to add it to the project.
In The feature i'll separate the Master Branch from the Develop Branch

I welcome any type of bug reports and suggestions through my GitHub [Issue Tracker](https://github.com/AndreaSantinato/LothiumLogger/issues).

### Latest Release Changes

Redesign all the library structure to manage more functionality and customization in the future release:
- Reorganize library's folders
- Reorganize library's classes
- Changes  to the Logger Class Core's Methods
- Changes to the Console's Sinker Methods
- Changes to the File's Sinker Methods
- Included the new AddCustomSinker() method inside the LoggerConfiguration Class to make able to do custom logging when using the library

### Copyright

_LothiumLogger is copyright &copy; 2023 - Provided under the [GNU General Public License v3.0](https://github.com/AndreaSantinato/LothiumLogger/blob/main/LICENSE)._

