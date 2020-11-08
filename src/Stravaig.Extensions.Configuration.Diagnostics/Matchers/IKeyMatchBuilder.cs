using System.Collections.Generic;

namespace Stravaig.Extensions.Configuration.Diagnostics.Matchers
{
    public interface IKeyMatchBuilder
    {
        /// <summary>
        /// Adds the given <see cref="IMatcher"/> to the builder.
        /// </summary>
        /// <param name="matcher">A matcher</param>
        /// <returns>Self, so that adding matcher calls can be chained.</returns>
        IKeyMatchBuilder Add(IMatcher matcher);

        /// <summary>
        /// Adds the given <see cref="IMatcher"/> instances to the builder.
        /// </summary>
        /// <param name="matchers">A sequence of matcher</param>
        /// <returns>Self, so that adding matcher calls can be chained.</returns>
        IKeyMatchBuilder Add(IEnumerable<IMatcher> matchers);

        /// <summary>
        /// Builds an overarching matcher than contains each of the listed matchers.
        /// </summary>
        /// <returns>A matcher that encompasses all the given rules.</returns>
        IMatcher Build();
    }
}