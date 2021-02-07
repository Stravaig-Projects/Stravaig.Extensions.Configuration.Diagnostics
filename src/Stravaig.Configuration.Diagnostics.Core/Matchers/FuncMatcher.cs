using System;

namespace Stravaig.Extensions.Configuration.Diagnostics.Matchers
{
    /// <summary>
    /// A matcher that uses a given function to perform the match.
    /// </summary>
    public class FuncMatcher : IMatcher
    {
        private readonly Func<string, bool> _predicate;

        /// <summary>
        /// Initialises the object with the given matching predicate function.
        /// </summary>
        /// <param name="predicate">The function predicate that matches the key.</param>
        public FuncMatcher(Func<string, bool> predicate)
        {
            _predicate = predicate
                                ?? throw new ArgumentNullException(nameof(predicate));
        }

        /// <inheritdoc />
        public bool IsMatch(string key)
        {
            return _predicate(key);
        }
    }
}