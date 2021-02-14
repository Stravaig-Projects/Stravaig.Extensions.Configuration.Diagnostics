namespace Stravaig.Configuration.Diagnostics.Renderers
{
    /// <summary>
    /// The virtual log level of a message. That will be translated for any final logging framework.
    /// </summary>
    public enum MessageLevel
    {
        /// <summary>
        /// Trace level logs, the most verbose level.
        /// </summary>
        Trace = 0,
        
        /// <summary>
        /// Debug level logs.
        /// </summary>
        Debug = 1,
        
        /// <summary>
        /// Information level logs that describe the general flow of the application.
        /// </summary>
        Information = 2,
        
        /// <summary>
        /// Warning level logs. Often indicates something that has gone wrong, but recoverable.
        /// </summary>
        Warning = 3,
        
        /// <summary>
        /// Error level logs. Often indicates that something has gone wrong and the current action has stopped, but is not an application wide issue.
        /// </summary>
        Error = 4,
        
        /// <summary>
        /// Critical level logs. Indicate that something has gone wrong that affects the entire application, which may have to come to a halt.
        /// </summary>
        Critical = 5,
        
        /// <summary>
        /// Will not be logged.
        /// </summary>
        None = 6,
    }
}