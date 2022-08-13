namespace Stravaig.Configuration.Diagnostics.Obfuscators;

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

/// <summary>
/// Helper extensions for setting up a fixed asterisk obfuscator
/// </summary>
public static class SetupFixedStringObfuscatorExtensions
{
    /// <summary>
    /// Sets up a fixed string obfuscator with the given string.
    /// </summary>
    /// <param name="builder">The setup builder</param>
    /// <param name="replacementText">The text with which to replace the secret.</param>
    public static void SetFixedStringObfuscator(this ConfigurationDiagnosticsOptionsSetupBuilder builder, string replacementText)
    {
        builder.SetObfuscator(new FixedStringObfuscator(replacementText));
    }
}