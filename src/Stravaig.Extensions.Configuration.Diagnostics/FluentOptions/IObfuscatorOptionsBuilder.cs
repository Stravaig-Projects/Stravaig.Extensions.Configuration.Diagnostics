using Stravaig.Extensions.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Extensions.Configuration.Diagnostics.FluentOptions
{
    public interface IObfuscatorOptionsBuilder
    {
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