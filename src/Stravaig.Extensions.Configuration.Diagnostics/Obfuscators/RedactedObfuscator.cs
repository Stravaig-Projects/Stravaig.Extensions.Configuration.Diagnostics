namespace Stravaig.Extensions.Configuration.Diagnostics.Obfuscators
{
    public class RedactedObfuscator : ISecretObfuscator
    {
        private const string Redacted = "REDACTED";

        private string _obfuscatedValue;

        public RedactedObfuscator()
        {
            _obfuscatedValue = Redacted;
        }

        public RedactedObfuscator(string symmetricalAccoutrement)
        {
            _obfuscatedValue = $"{symmetricalAccoutrement}{Redacted}{symmetricalAccoutrement}";
        }

        public string Obfuscate(string secret)
        {
            return _obfuscatedValue;
        }
    }
}