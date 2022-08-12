using Stravaig.Configuration.Diagnostics.Matchers;

namespace Stravaig.Configuration.Diagnostics;

/// <summary>
/// A builder for configuring the options used to render diagnostic information.
/// </summary>
public class ConfigurationDiagnosticsOptionsSetupBuilder
{
    internal bool ApplyGlobally { get; private set; }
    private readonly IKeyMatchBuilder _configKeyMatches = new KeyMatchBuilder();
    private readonly IKeyMatchBuilder _connectionKeyMatches = new KeyMatchBuilder();

    /// <summary>
    /// Indicates whether the completed configuration should be applied globally.
    /// </summary>
    /// <param name="applyGlobally">Whether to apply the completed configuration
    /// globally, default is true.</param>
    public void MakeGlobal(bool applyGlobally = true) => ApplyGlobally = applyGlobally;
    public IKeyMatchBuilder ObfuscateConfig => _configKeyMatches;
    public IKeyMatchBuilder ObfuscateConnectionString => _connectionKeyMatches;

    internal void ApplyTo(ConfigurationDiagnosticsOptions opts)
    {
        opts.ConfigurationKeyMatcher = _configKeyMatches.Build();
        opts.ConnectionStringElementMatcher = _connectionKeyMatches.Build();
    }
}