using System.Collections.Generic;

namespace Stravaig.Configuration.Diagnostics.Matchers
{
    /// <summary>
    /// A builder for combining many key matchers together.
    /// </summary>
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