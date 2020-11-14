namespace Stravaig.Extensions.Configuration.Diagnostics.Obfuscators
{
    /// <summary>
    /// Initialises an obfuscator that replaces the secret with REDACTED.
    /// </summary>
    public class RedactedObfuscator : ISecretObfuscator
    {
        private const string Redacted = "REDACTED";

        private readonly string _obfuscatedValue;

        /// <summary>
        /// Initialises the obfuscator with REDACTED.
        /// </summary>
        public RedactedObfuscator()
        {
            _obfuscatedValue = Redacted;
        }

        /// <summary>
        /// Initialises the obfuscator with REDACTED surrounded by the accoutrement.
        /// </summary>
        /// <param name="symmetricalAccoutrement">The string to place before and after the word REDACTED.</param>
        public RedactedObfuscator(string symmetricalAccoutrement)
        {
            _obfuscatedValue = $"{symmetricalAccoutrement}{Redacted}{symmetricalAccoutrement}";
        }

        /// <summary>
        /// Initialises the obfuscator with REDACTED surrounded by the accoutrements.
        /// </summary>
        /// <param name="leftAccoutrement">The string to place before the word REDACTED.</param>
        /// <param name="rightAccoutrement">The string to place after the word REDACTED.</param>
        public RedactedObfuscator(string leftAccoutrement, string rightAccoutrement)
        {
            _obfuscatedValue = $"{leftAccoutrement}{Redacted}{rightAccoutrement}";
        }

        /// <inheritdoc />
        public string Obfuscate(string secret)
        {
            return _obfuscatedValue;
        }
    }
}