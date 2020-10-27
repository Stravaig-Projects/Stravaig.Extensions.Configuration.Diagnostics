using System.Collections.Generic;

namespace Stravaig.Extensions.Configuration.Diagnostics.Matchers
{
    public interface IMatchBuilder
    {
        IMatchBuilder Add(IMatcher matcher);
        IMatchBuilder Add(IEnumerable<IMatcher> matchers);
    }
}