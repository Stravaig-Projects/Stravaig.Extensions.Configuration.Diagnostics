namespace Stravaig.Extensions.Configuration.Diagnostics.Obfuscators
{
    /// <summary>
    /// An obfuscator that replaces secrets with a fixed number of asterisks.
    /// </summary>
    public class FixedAsteriskObfuscator : ISecretObuscator
    {
        private readonly string _asterisks;

        /// <summary>
        /// Initialises the obfuscator with 4 asterisks.
        /// </summary>
        public FixedAsteriskObfuscator()
        {
            _asterisks = "****";
        }

        /// <summary>
        /// Initialises the obfuscator with the given number of asterisks.
        /// </summary>
        /// <param name="numAsterisks">The number of asterisks to replace a secret with.</param>
        public FixedAsteriskObfuscator(int numAsterisks)
        {
            _asterisks = new string('*', numAsterisks);
        }

        /// <inheritdoc />
        public string Obfuscate(string secret)
        {
            return _asterisks;
        }
    }
}