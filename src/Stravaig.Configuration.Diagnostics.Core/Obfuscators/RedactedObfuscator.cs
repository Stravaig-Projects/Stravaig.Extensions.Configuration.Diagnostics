namespace Stravaig.Configuration.Diagnostics.Obfuscators
{
    /// <summary>
    /// Initialises an obfuscator that replaces the secret with REDACTED.
    /// </summary>
    public class RedactedObfuscator : FixedStringObfuscator
    {
        private const string Redacted = "REDACTED";
        
        /// <summary>
        /// Initialises the obfuscator with REDACTED.
        /// </summary>
        public RedactedObfuscator()
            : base(Redacted)
        {
        }

        /// <summary>
        /// Initialises the obfuscator with REDACTED surrounded by the accoutrement.
        /// </summary>
        /// <param name="symmetricalAccoutrement">The string to place before and after the word REDACTED.</param>
        public RedactedObfuscator(string symmetricalAccoutrement)
            : base($"{symmetricalAccoutrement}{Redacted}{symmetricalAccoutrement}")
        {
        }

        /// <summary>
        /// Initialises the obfuscator with REDACTED surrounded by the accoutrements.
        /// </summary>
        /// <param name="leftAccoutrement">The string to place before the word REDACTED.</param>
        /// <param name="rightAccoutrement">The string to place after the word REDACTED.</param>
        public RedactedObfuscator(string leftAccoutrement, string rightAccoutrement)
            : base($"{leftAccoutrement}{Redacted}{rightAccoutrement}")
        {
        }
    }
}