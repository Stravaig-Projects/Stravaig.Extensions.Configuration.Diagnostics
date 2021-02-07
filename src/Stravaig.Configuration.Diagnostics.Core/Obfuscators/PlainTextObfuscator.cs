namespace Stravaig.Extensions.Configuration.Diagnostics.Obfuscators
{
    /// <summary>
    /// An obfuscator that just returns the plain text representation back.
    /// </summary>
    public class PlainTextObfuscator : ISecretObfuscator
    {
        /// <summary>
        /// The default instance of the <see cref="T:Stravaig.Extensions.Configuration.Diagnostics.Obfuscators.PlainTextObfuscator"/>.
        /// </summary>
        public static PlainTextObfuscator Instance { get; } = new PlainTextObfuscator();
        
        /// <inheritdoc />
        public string Obfuscate(string secret)
        {
            return secret;
        }
    }
}