# LothiumLgger [![NuGet Version](https://img.shields.io/nuget/v/LothiumLogger.svg?style=flat)](https://www.nuget.org/packages/LothiumLogger/) [![NuGet Downloads](https://img.shields.io/nuget/v/LothiumLogger.svg?style=flat)](https://www.nuget.org/packages/LothiumLogger/)

### Getting Started

LothiumLogger is a simple easy-to-use Logger Library for .Net applications written entirely in C# for fun, it offers a basic and easy syntax and give flexybility to the final user.
The idea behind this project is to create a simple interactive way to catch events from your application in the simpliest and easiest way possible just by instance a variable and use it!
You can install LothiumLogger directly from NuGet using the NuGet Manager inside Visual Studio or with this command:

```
dotnet add package LothiumLogger
```

This library provide a simple configuration steps, the first thing to do is create a new LoggerSettings and configure all the settings that the logging process will use.
One way is to create a new LoggerSettings class, configure it and pass it to the constructor, the other way is to do it directly inside the constructor with a delegate action.

```csharp
var logger = new Logger(settings => {
    // Add settings here
});
```

The library provides a built-in Sink, one for logging to the OS's console and one for logging to one or more file.
To configure this two sink you need a specific dedicated SinkOptions configuration to provide them, the process to do that is the same of the LoggerSettings class.
Every sink will use different property from the SinkOptions class so it's not required to populate all of them, this depends on what sink you are currently configuring.

```csharp
var logger = new Logger(settings => {
    // Console Sink
    settings.AddConsoleSink(options => {
        // Add options here 
    });

    // Default File Sink For Log Events
    settings.AddFileSink(options => {
        // Add options here
    });
});
```

### Console Sinker

The console's sink offer the ability to write logs directly to the IDE's debug console / OS's console to track events while running your projects.
Like mentioned before, this sink required a set of options passed through a dedicated object to properly working.
This options class have different type of property, the minimum required are the following:

```csharp
var logger = new Logger(settings => {
    settings.AddConsoleSink(options => {
        options.Enabled = true;                                             // Required => Defines if the sink is enabled for writing the events
        options.DateFormat = LogDateFormatEnum.Standard;                    // Required => Defines the format for the date inside the log events
        options.LoggingRule = new(LogLevelEnum.Debug, false);               // Required => Defines the rule for writing the events
    });
});
```

### File Sinker

The file's sink offer the ability to write logs to one or more files/folders, like mentioned before, this sink required a set of options passed through a dedicated object to properly working.
This options class have different type of property, the minimum required are the following:

The 'AddFileSink()' have this property to be inizialized:
    - name: indicate the name of for the final created file
    - path: indicate the path where the final file will be created
    - minimumLogLevel: indicate the minimum accepted logging level for the sink.
    - restrictedToLogLevel: indicate the only accepted loggin level for the sink, this value overwrite the minimum one.
    - typeOfGeneratedFile: indicate the type of the final created file (for now there is only two types: GenericLog & LothiumLog)

```csharp
var logger = new Logger(settings => {
    settings.AddFileSink(options => {
        options.Enabled = true;                                                             // Required => Defines if the sink is enabled for writing the events
        options.DateFormat = LogDateFormatEnum.Standard;                                    // Required => Defines the format for the date inside the log events
        options.LoggingRule = new(LogLevelEnum.Normal, false);                              // Required => Defines the rule for writing the events
        options.FileRule = new(                                                             // Required => Defines the type, the path and the name of the file
            LogFileTypeEnum.GenericLog, 
            Path.Combine("var", "logs"),
            "LogName"
        );                                                                                  
    });
});
```

### Custom Sinker

The library provides the ability to use custom sink in addition to the built-in ones.
To define a custom sink you need to extends the GenericSink class and override the Write() method
The configuration object offer the ability to create and add a customized sinker, the only thing to do is create a new class and extender the ISinkers interface:

```csharp
using LothiumLogger.Interfaces;

public class CustomSink : GenericSink
{
    public CustomSink(SinkOptions setting) : base(setting) { }

    public CustomSink(Action<SinkOptions> settings) : base(settings) { }

    public override void Write(LogEvent logEvent)       // Override required if you want to add custom logic during the writing of the log events
    {
        base.Write(logEvent)        // Do this if you need the default writing before your custom logic //

        // Put here your custom logic //
    }
}
```

### Logger Methods

After the creation of the logger instance, there are 6 different types of logging level: Normal, Debug, Information, Warning, Error & Fatal.
Each one have their separated methods for writing new logging events:

```csharp
logger.Write("Log Message");                            // Defualt normal message
logger.Debug("Debug Log Message");                      // Debug message when testing code
logger.Information("Info Log Message");                 // Info message
logger.Warning("Warn Log Message");                     // Warning message
logger.Error("Err Log Message");                        // Error message
logger.Fatal("Fatal Log Message");                      // Fatal message
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

