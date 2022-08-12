namespace Stravaig.Configuration.Diagnostics.Obfuscators;

/// <summary>
/// An obfuscator that replaces secrets with a fixed number of asterisks.
/// </summary>
public class FixedAsteriskObfuscator : FixedStringObfuscator
{
    /// <summary>
    /// Initialises the obfuscator with 4 asterisks.
    /// </summary>
    public FixedAsteriskObfuscator()
        : base("****")
    {
    }

    /// <summary>
    /// Initialises the obfuscator with the given number of asterisks.
    /// </summary>
    /// <param name="numAsterisks">The number of asterisks to replace a secret with.</param>
    public FixedAsteriskObfuscator(int numAsterisks)
        : base(new string('*', numAsterisks))
    {
    }
}

/// <summary>
/// Helper extensions for setting up a fixed asterisk obfuscator
/// </summary>
public static class SetupFixedAsteriskObfuscatorExtensions
{
    /// <summary>
    /// Sets up a fixed asterisk obfuscator with the default number of asterisks.
    /// </summary>
    /// <param name="builder">The setup builder</param>
    public static void SetFixedAsteriskObfuscator(this ConfigurationDiagnosticsOptionsSetupBuilder builder)
    {
        builder.SetObfuscator(new FixedAsteriskObfuscator());
    }

    /// <summary>
    /// Sets up a fixed asterisk obfuscator with the default number of asterisks.
    /// </summary>
    /// <param name="builder">The setup builder</param>
    /// <param name="numberOfAsterisks">The number of asterisks with which to replace the secret.</param>
    public static void SetFixedAsteriskObfuscator(this ConfigurationDiagnosticsOptionsSetupBuilder builder, int numberOfAsterisks)
    {
        builder.SetObfuscator(new FixedAsteriskObfuscator(numberOfAsterisks));
    }
}