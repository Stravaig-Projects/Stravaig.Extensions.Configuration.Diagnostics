namespace Stravaig.Configuration.Diagnostics.Obfuscators;

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

/// <summary>
/// Helper extensions for setting up a matched length asterisk obfuscator
/// </summary>
public static class SetupMatchedLengthAsteriskObfuscatorExtensions
{
    /// <summary>
    /// Sets up an obfuscator that converts the secret to a asterisk string of equivalent length.
    /// </summary>
    /// <param name="builder">The setup builder</param>
    public static void SetMatchedLengthObfuscator(this ConfigurationDiagnosticsOptionsSetupBuilder builder)
    {
        builder.SetObfuscator(new MatchedLengthAsteriskObfuscator());
    }
}