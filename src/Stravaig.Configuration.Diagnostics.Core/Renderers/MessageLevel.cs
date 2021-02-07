namespace Stravaig.Extensions.Configuration.Diagnostics.Renderers
{
    /// <summary>
    /// The virtual log level of a message.
    /// </summary>
    public enum MessageLevel
    {
        Trace = 0,
        Debug = 1,
        Information = 2,
        Warning = 3,
        Error = 4,
        Critical = 5,
        
        /// <summary>
        /// Will not be logged.
        /// </summary>
        None = 6,
    }
}