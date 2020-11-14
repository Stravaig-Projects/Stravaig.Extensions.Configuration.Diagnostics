namespace Stravaig.Extensions.Configuration.Diagnostics.Obfuscators
{
    public class FixedStringObfuscator : ISecretObfuscator
    {
        private readonly string _fixedString;

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