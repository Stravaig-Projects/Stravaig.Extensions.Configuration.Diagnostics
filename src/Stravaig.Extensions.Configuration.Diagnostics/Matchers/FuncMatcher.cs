using System;

namespace Stravaig.Extensions.Configuration.Diagnostics.Matchers
{
    /// <summary>
    /// A matcher that uses a given function to perform the match.
    /// </summary>
    public class FuncMatcher : IMatcher
    {
        private readonly Func<string, bool> _matchingFunction;

        /// <summary>
        /// Initialises the object with the given matching function.
        /// </summary>
        /// <param name="matchingFunction"></param>
        public FuncMatcher(Func<string, bool> matchingFunction)
        {
            _matchingFunction = matchingFunction
                                ?? throw new ArgumentNullException(nameof(matchingFunction));
        }

        /// <inheritdoc />
        public bool IsMatch(string key)
        {
            return _matchingFunction(key);
        }
    }
}