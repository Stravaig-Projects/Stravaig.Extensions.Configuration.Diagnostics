namespace Stravaig.Extensions.Configuration.Diagnostics.Obfuscators
{
    public class RedactedObfuscator : ISecretObfuscator
    {
        public string Obfuscate(string secret)
        {
            return "REDACTED";
        }
    }
}