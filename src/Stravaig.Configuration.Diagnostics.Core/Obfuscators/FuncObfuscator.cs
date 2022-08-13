using System;

namespace Stravaig.Configuration.Diagnostics.Obfuscators;

/// <summary>
/// Wraps a Func&lt;string, string&gt; as an obfuscator.
/// </summary>
public class FuncObfuscator : ISecretObfuscator
{
    private readonly Func<string, string> _obfuscateFunction;

    /// <summary>
    /// Initialises a <see cref="FuncObfuscator"/> with a function that runs when <see cref="Obfuscate"/> is called.
    /// </summary>
    /// <param name="obfuscateFunction">The function that obfuscates the secret.</param>
    public FuncObfuscator(Func<string, string> obfuscateFunction)
    {
        _obfuscateFunction = obfuscateFunction 
                             ?? throw new ArgumentNullException(nameof(obfuscateFunction));
    }
        
    /// <inheritdoc />
    public string Obfuscate(string secret)
    {
        return _obfuscateFunction(secret);
    }
}

/// <summary>
/// Helper extensions for setting up a func based obfuscator
/// </summary>
public static class SetupFuncObfuscatorExtensions
{
    /// <summary>
    /// Sets up an obfuscator that applies a func to the text.
    /// </summary>
    /// <param name="builder">The setup builder</param>
    /// <param name="obfuscate">The func that applies the obfuscation.</param>
    public static void SetFuncObfuscator(this ConfigurationDiagnosticsOptionsSetupBuilder builder, Func<string, string> obfuscate)
    {
        builder.SetObfuscator(new FuncObfuscator(obfuscate));
    }
}