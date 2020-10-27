namespace Stravaig.Extensions.Configuration.Diagnostics.Obfuscators
{
    public interface ISecretObuscator
    {
        /// <summary>
        /// Obfuscate the given secret.
        /// </summary>
        /// <param name="secret">The secret to obfuscate.</param>
        /// <returns>An obfuscated secret.</returns>
        string Obfuscate(string secret);
    }
}