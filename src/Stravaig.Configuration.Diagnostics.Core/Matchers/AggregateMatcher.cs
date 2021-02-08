using System;
using System.Collections.Generic;
using System.Linq;

namespace Stravaig.Configuration.Diagnostics.Matchers
{
    /// <summary>
    /// A matcher that contains other matchers.
    /// </summary>
    public class AggregateMatcher : IMatcher
    {
        private readonly List<IMatcher> _matchers;

        /// <summary>
        /// Initialises a default empty <see cref="AggregateMatcher"/>.
        /// </summary>
        public AggregateMatcher()
        {
            _matchers = new List<IMatcher>();
        }

        /// <summary>
        /// Initialises an <see cref="AggregateMatcher"/> with the given matchers.
        /// </summary>
        /// <param name="matchers">The matchers to initialise this <see cref="AggregateMatcher"/> with.</param>
        public AggregateMatcher(IEnumerable<IMatcher> matchers)
        {
            if (matchers == null) 
                throw new ArgumentNullException(nameof(matchers));
            _matchers = new List<IMatcher>(matchers);
        }

        /// <summary>
        /// Adds a matcher to this aggregate matcher
        /// </summary>
        /// <param name="matcher">A matcher to add.</param>
        /// <returns>Self, so that methods can be chained.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="matcher"/> is null</exception>
        /// <exception cref="ArgumentException"><paramref name="matcher"/> would result in a circular reference.</exception>
        public AggregateMatcher Add(IMatcher matcher)
        {
            if (matcher == null)
                throw new ArgumentNullException(nameof(matcher));
            
            if (matcher == this)
                throw new ArgumentException(
                    $"Circular reference detected. Cannot add an {nameof(AggregateMatcher)} to itself.",
                    nameof(matcher));

            if (matcher is NullMatcher)
                return this;
            
            // TODO: Deeper circular reference detection.
            
            _matchers.Add(matcher);

            return this;
        }

        /// <summary>
        /// Adds many matchers to this aggregate matcher.
        /// </summary>
        /// <param name="matchers">The matchers to add.</param>
        /// <returns>self, so that methods can be chained.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="matchers"/> is null</exception>
        /// <exception cref="ArgumentException"><paramref name="matchers"/> would result in a circular reference.</exception>
        public AggregateMatcher Add(IEnumerable<IMatcher> matchers)
        {
            if (matchers == null) 
                throw new ArgumentNullException(nameof(matchers));

            foreach (var matcher in matchers)
                Add(matcher);

            return this;
        }
        
        /// <inheritdoc />
        public bool IsMatch(string key)
        {
            return _matchers.Any(matcher => matcher.IsMatch(key));
        }
    }
}