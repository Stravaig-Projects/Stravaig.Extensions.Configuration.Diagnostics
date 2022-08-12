using System.Runtime.CompilerServices;
using Stravaig.Configuration.Diagnostics.FluentOptions;
using Stravaig.Configuration.Diagnostics.Matchers;
using Stravaig.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Configuration.Diagnostics;

/// <summary>
/// A builder for configuring the options used to render diagnostic information.
/// </summary>
public class ConfigurationDiagnosticsOptionsSetupBuilder
{
    internal bool ApplyGlobally { get; private set; }
    private readonly IKeyMatchBuilder _configKeyMatches = new KeyMatchBuilder();
    private readonly IKeyMatchBuilder _connectionKeyMatches = new KeyMatchBuilder();
    private ISecretObfuscator _obfuscator = PlainTextObfuscator.Instance; 

    /// <summary>
    /// Indicates whether the completed configuration should be applied globally.
    /// </summary>
    /// <param name="applyGlobally">Whether to apply the completed configuration
    /// globally, default is true.</param>
    public void MakeGlobal(bool applyGlobally = true) => ApplyGlobally = applyGlobally;
    
    /// <summary>
    /// Builds the key matchers for the configuration elements.
    /// </summary>
    public IKeyMatchBuilder ObfuscateConfig => _configKeyMatches;
    
    /// <summary>
    /// Builds the key matchers for the connection strings.
    /// </summary>
    public IKeyMatchBuilder ObfuscateConnectionString => _connectionKeyMatches;

    /// <summary>
    /// Sets the obfuscator to use when rendering secrets.
    /// </summary>
    /// <param name="obfuscator">The obfuscator</param>
    public void SetObfuscator(ISecretObfuscator obfuscator) => _obfuscator = obfuscator;


    internal void ApplyTo(ConfigurationDiagnosticsOptions opts)
    {
        opts.ConfigurationKeyMatcher = _configKeyMatches.Build();
        opts.ConnectionStringElementMatcher = _connectionKeyMatches.Build();
        opts.Obfuscator = _obfuscator;
    }
}