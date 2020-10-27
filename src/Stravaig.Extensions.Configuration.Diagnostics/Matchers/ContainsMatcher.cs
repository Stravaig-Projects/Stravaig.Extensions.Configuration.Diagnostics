namespace Stravaig.Extensions.Configuration.Diagnostics.Matchers
{
    public class ContainsMatcher : IMatcher
    {
        private readonly string _contains;

        /// <summary>
        /// Initialises a matcher where the condition is that the given value is contained within the value being matched.
        /// </summary>
        /// <param name="contains"></param>
        public ContainsMatcher(string contains)
        {
            _contains = contains;
        }

        /// <inheritdoc />
        public bool IsMatch(string key)
        {
            return key.Contains(_contains);
        }
    }
}