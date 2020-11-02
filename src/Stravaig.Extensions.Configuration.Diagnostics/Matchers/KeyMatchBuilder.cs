using System;
using System.Collections.Generic;

namespace Stravaig.Extensions.Configuration.Diagnostics.Matchers
{
    /// <summary>
    /// 
    /// </summary>
    public class KeyMatchBuilder : IKeyMatchBuilder
    {
        private List<IMatcher> _matchers;

        public KeyMatchBuilder()
        {
            _matchers = new List<IMatcher>();
        }

        public IKeyMatchBuilder Add(IMatcher matcher)
        {
            if (matcher == null)
                throw new ArgumentNullException(nameof(matcher));

            _matchers.Add(matcher);
            return this;
        }

        public IKeyMatchBuilder Add(IEnumerable<IMatcher> matchers)
        {
            if (matchers == null)
                throw new ArgumentNullException(nameof(matchers));

            foreach (var matcher in matchers)
                Add(matcher);

            return this;
        }

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