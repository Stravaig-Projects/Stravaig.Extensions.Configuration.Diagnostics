using System;

namespace Stravaig.Extensions.Configuration.Diagnostics.Renderers
{
    /// <summary>
    /// A message entry that will be send to the logger.
    /// </summary>
    public class MessageEntry
    {
        /// <summary>
        /// The message (or template, for structured loggers)
        /// </summary>
        public string MessageTemplate { get; private set; }

        /// <summary>
        /// The properties to be applied to the template. (For non-structured loggers this will be ignored)
        /// </summary>
        public object[] Properties { get; }

        /// <summary>
        /// An exception that occurred when attempting to render the message.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Initialises the message with a message(template) and a set of properties.
        /// </summary>
        /// <param name="messageTemplate">The message(template).</param>
        /// <param name="properties">The optional properties.</param>
        public MessageEntry(string messageTemplate, params object[] properties)
        {
            MessageTemplate = messageTemplate;
            Properties = properties;
            Exception = null;
        }
        
        /// <summary>
        /// Initialises the message with an exception, a message(template) and optional properties.
        /// </summary>
        /// <param name="exception">The exception that occurred during the rendering process.</param>
        /// <param name="messageTemplate">The message(template).</param>
        /// <param name="properties">The optional properties to be applied to the template.</param>
        public MessageEntry(Exception exception, string messageTemplate, params object[] properties)
        {
            MessageTemplate = messageTemplate;
            Properties = properties;
            Exception = exception;
        }

        /// <summary>
        /// Gets the log level with which to render this message.
        /// </summary>
        /// <param name="requestedLevel">The user requested level.</param>
        /// <returns>The log level, which may be elevated in the event that an exception exists.</returns>
        public MessageLevel GetMessageLevel(MessageLevel requestedLevel)
        {
            if (HasException)
                return (MessageLevel) Math.Max((int) requestedLevel, (int) MessageLevel.Warning);
            return requestedLevel;
        }

        /// <summary>
        /// Indicated if an exception occurred during the rendering process.
        /// </summary>
        public bool HasException => Exception != null;
    }
}