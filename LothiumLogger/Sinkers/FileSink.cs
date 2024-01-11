// System Classes
using System.IO;
using System.Text.RegularExpressions;
// Custom Classes
using LothiumLogger.Configurations;
using LothiumLogger.Enumerations;

// Sinkers Namesapce
namespace LothiumLogger.Sinkers;

/// <summary>
/// Define a FileSink instance
/// Provides a set of methods to write log events inside one or more files/directories
/// </summary>
public class FileSink : GenericSink
{
    #region Class Constructors

    /// <summary>
    /// Defines a new FileSink instance
    /// </summary>
    /// <param name="setting">Contains the settings for the sink</param>
    public FileSink(SinkOptions setting) : base(setting) { }

    /// <summary>
    /// Defines a new FileSink instance
    /// </summary>
    /// <param name="setting">Contains the settings for the sink</param>
    public FileSink(Action<SinkOptions> settings) : base(settings) { }

    /// <summary>
    /// Destructor to ensure that Dispose is called if the user forgets
    /// </summary>
    ~FileSink() => base.DisposeResource(false);

    #endregion

    #region Class Methods

    /// <summary>
    /// Format an aggregate message for the log events
    /// </summary>
    /// <param name="logEvent">Contains the log event to aggregate</param>
    /// <param name="previouslyWrittenContent">Contains a previously generated aggregate log file's content</param>
    /// <returns></returns>
    protected string FormatAggregateMessage(LogEvent logEvent, string previouslyWrittenContent = "")
    {
        var innerContent = string.Empty;
        var content = string.Empty;

        //
        // 1) Retrive all the aggregate log events
        // 2) Add the new log event inside the log's events list
        // 3) Format the final aggregate content for the log's file
        // 4) Return the generated result
        //

        // Populate the log events list from a previously content
        var _events = new List<string>();
        foreach (Match match in Regex.Matches(previouslyWrittenContent, "{[^}]+}"))
        {
            var matchItem = match.Value
                .Replace("{", string.Empty)
                .Replace("}", string.Empty)
                .Replace("\t", string.Empty)
                .Replace(Environment.NewLine, string.Empty)
                .Trim();
            _events.Add(matchItem);
        }

        // Add the new log event to the final list
        _events.Add(FormatMessage(logEvent));

        // Generate the inner aggregate content
        _events.ForEach(e => innerContent = string.Concat(innerContent, $"\t {e}", Environment.NewLine));

        // Generate the final content
        content = string.Concat(
            $"EventsOfDate ({GenerateLogDate(logEvent.Date, LogDateFormatEnum.Minimal)}) =>",
            Environment.NewLine,
            "{",
            Environment.NewLine,
            innerContent,
            "};"
        );

        return content;
    }

    /// <summary>
    /// Read the content of a log file
    /// </summary>
    /// <param name="path">Contains the path of the choosen file to read</param>
    /// <returns>A string with the content of the readed file</returns>
    public string Read(string path)
    {
        try
        {
            // Verify if the file exist
            if (!File.Exists(path)) throw new FileNotFoundException("No path specified!!");

            // Read and process the file's content
            var content = File.ReadAllText(path);
            if (string.IsNullOrEmpty(content)) throw new Exception("No content inside the file!!");

            return content.Trim();
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// Write a new log event into an output file
    /// </summary>
    /// <param name="logEvent">Contains the generated log event</param>
    public override void Write(LogEvent logEvent)
    {
        //
        // 1) Set all the variables
        // 2) Generate the writing output path for the file
        // 3) Generate the file content with all the log events based on the choosen file's type
        // 4) Write the content inside the specific output path of the file
        //

        var extension = Options!.FileRule!.Type switch
        {
            LogFileTypeEnum.GenericLog => "log",
            LogFileTypeEnum.LothiumLog => "ltlog",
            _ => throw new NotImplementedException()
        };
        var name = string.Format(
            "{0}_{1}.{2}",
            string.IsNullOrEmpty(Options!.FileRule!.Name) ? "Log" : Options!.FileRule!.Name,
            DateTime.Now.ToString("yyMMdd"),
            extension
        );
        var path = !string.IsNullOrEmpty(Options!.FileRule!.Path)
            ? Path.Combine(Options!.FileRule.Path)
            : Path.Combine(Directory.GetCurrentDirectory(), "Logs");
        var outputPath = Path.Combine(path, name);
        var prevContent = Read(outputPath);
        var content = Options!.FileRule.Type switch
        {
            LogFileTypeEnum.GenericLog => FormatMessage(logEvent, prevContent),
            LogFileTypeEnum.LothiumLog => FormatAggregateMessage(logEvent, prevContent),
            _ => throw new NotImplementedException()
        };

        // Create the output directory and log's file if they doesn't exists and write the final formated content
        // If the log's file doesn't exist it will create it
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);        
        File.WriteAllText(outputPath, content);
    }

    #endregion
}
