namespace Stravaig.Extensions.Configuration.Diagnostics.Matchers
{
    /// <summary>
    /// A matcher that never matches any key.
    /// </summary>
    public class NullMatcher : IMatcher
    {
        /// <summary>
        /// A fixed instance of the null matcher.
        /// </summary>
        public static NullMatcher Instance { get; } = new NullMatcher();
        
        /// <inheritdoc />
        public bool IsMatch(string key)
        {
            return false;
        }
    }
}