namespace Stravaig.Configuration.Diagnostics.Obfuscators
{
    /// <summary>
    /// An interface for strategies to obfuscate secrets.
    /// </summary>
    public interface ISecretObfuscator
    {
        /// <summary>
        /// Obfuscate the given secret.
        /// </summary>
        /// <param name="secret">The secret to obfuscate.</param>
        /// <returns>An obfuscated secret.</returns>
        string Obfuscate(string secret);
    }
}