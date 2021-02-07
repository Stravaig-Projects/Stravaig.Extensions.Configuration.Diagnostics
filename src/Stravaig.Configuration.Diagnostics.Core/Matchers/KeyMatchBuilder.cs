using System;
using System.Collections.Generic;

namespace Stravaig.Extensions.Configuration.Diagnostics.Matchers
{
    /// <summary>
    /// A builder for key matchers
    /// </summary>
    public class KeyMatchBuilder : IKeyMatchBuilder
    {
        private List<IMatcher> _matchers;
        
        /// <summary>
        /// Initialises the key match builder.
        /// </summary>
        public KeyMatchBuilder()
        {
            _matchers = new List<IMatcher>();
        }

        /// <inheritdoc />
        public IKeyMatchBuilder Add(IMatcher matcher)
        {
            if (matcher == null)
                throw new ArgumentNullException(nameof(matcher));

            _matchers.Add(matcher);
            return this;
        }

        /// <inheritdoc />
        public IKeyMatchBuilder Add(IEnumerable<IMatcher> matchers)
        {
            if (matchers == null)
                throw new ArgumentNullException(nameof(matchers));

            foreach (var matcher in matchers)
                Add(matcher);

            return this;
        }

        /// <inheritdoc />
        public IMatcher Build()
        {
            switch (_matchers.Count)
            {
                case 0:
                    return NullMatcher.Instance;
                case 1:
                    return _matchers[0];
                default:
                    return new AggregateMatcher(_matchers);
            }
        }
    }
}