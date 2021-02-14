using Stravaig.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Configuration.Diagnostics.FluentOptions
{
    /// <summary>
    /// The part of the fluent interface that adds the obfuscator to the configuration options.
    /// </summary>
    public interface IObfuscatorOptionsBuilder
    {
        /// <summary>
        /// Sets the obfuscator.
        /// </summary>
        /// <param name="obfuscator">The obfuscator to use to mask out secrets.</param>
        /// <returns>The part of the fluent interface that transitions to a new section.</returns>
        IHandOverToNewSectionOptionsBuilder With(ISecretObfuscator obfuscator);
    }

    public partial class ConfigurationDiagnosticsOptionsBuilder
        : IObfuscatorOptionsBuilder
    {
        IHandOverToNewSectionOptionsBuilder IObfuscatorOptionsBuilder.With(ISecretObfuscator obfuscator)
        {
            _obfuscator = obfuscator;
            return this;
        }
    }
}