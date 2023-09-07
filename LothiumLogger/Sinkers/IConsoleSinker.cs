// System Class
using System;
// Custom Class
using LothiumLogger.Enumerations;

namespace LothiumLogger.Sinkers
{
    /// <summary>
    /// Interface that define a ConsoleSink
    /// </summary>
    internal interface IConsoleSinker : ISinker
    {
        /// <summary>
        /// Retrive the theme from a passed name
        /// </summary>
        /// <param name="themeName">Contains the console theme's name</param>
        /// <returns>One specific console theme</returns>
        IConsoleSinkerThemes RetriveThemeFromName(ConsoleThemeEnum themeName);
    }
}
