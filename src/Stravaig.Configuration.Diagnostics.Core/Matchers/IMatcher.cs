namespace Stravaig.Configuration.Diagnostics.Matchers
{
    /// <summary>
    /// A matcher that determines if a key matches a set of conditions.
    /// </summary>
    public interface IMatcher
    {
        /// <summary>
        /// Determines if the given key matches the the conditions for the matcher.
        /// </summary>
        /// <param name="key">The key to test</param>
        /// <returns>true if the key is a match; false otherwise.</returns>
        bool IsMatch(string key);
    }
}