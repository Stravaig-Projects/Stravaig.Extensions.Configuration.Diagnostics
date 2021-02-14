namespace Stravaig.Configuration.Diagnostics.Obfuscators
{
    /// <summary>
    /// Obfuscates a secret by replacing it with a fixed string.
    /// </summary>
    public class FixedStringObfuscator : ISecretObfuscator
    {
        private readonly string _fixedString;

        /// <summary>
        /// Initialises the obfuscator with the fixed string with which to replace secrets.
        /// </summary>
        /// <param name="fixedString"></param>
        public FixedStringObfuscator(string fixedString)
        {
            _fixedString = fixedString;
        }

        /// <inheritdoc />
        public string Obfuscate(string secret)
        {
            if (string.IsNullOrWhiteSpace(secret))
                return string.Empty;

            return _fixedString;
        }
    }
}