namespace Stravaig.Configuration.Diagnostics.Obfuscators
{
    /// <summary>
    /// Initialises an obfuscator that replaces the secret with a string of asterisks the same length as the secret.
    /// </summary>
    public class MatchedLengthAsteriskObfuscator : ISecretObfuscator
    {
        /// <inheritdoc />
        public string Obfuscate(string secret)
        {
            if (string.IsNullOrEmpty(secret))
                return string.Empty;
            
            return new string('*', secret.Length);
        }
    }
}