namespace Stravaig.Extensions.Configuration.Diagnostics.Obfuscators
{
    public class RedactedObfuscator : ISecretObfuscator
    {
        private const string Redacted = "REDACTED";

        private readonly string _obfuscatedValue;

        public RedactedObfuscator()
        {
            _obfuscatedValue = Redacted;
        }

        public RedactedObfuscator(string symmetricalAccoutrement)
        {
            _obfuscatedValue = $"{symmetricalAccoutrement}{Redacted}{symmetricalAccoutrement}";
        }

        public RedactedObfuscator(string leftAccoutrement, string rightAccoutrement)
        {
            _obfuscatedValue = $"{leftAccoutrement}{Redacted}{rightAccoutrement}";
        }

        public string Obfuscate(string secret)
        {
            return _obfuscatedValue;
        }
    }
}