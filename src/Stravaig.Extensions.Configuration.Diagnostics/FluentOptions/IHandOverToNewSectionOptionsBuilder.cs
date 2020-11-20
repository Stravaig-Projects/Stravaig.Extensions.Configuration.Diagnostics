namespace Stravaig.Extensions.Configuration.Diagnostics.FluentOptions
{
    public interface IHandOverToNewSectionOptionsBuilder
    {
        ConfigurationDiagnosticsOptionsBuilder And { get; }
        IFinishBuildingOptions AndFinally { get; }
    }
    
    public partial class ConfigurationDiagnosticsOptionsBuilder
        : IHandOverToNewSectionOptionsBuilder
    {
        ConfigurationDiagnosticsOptionsBuilder IHandOverToNewSectionOptionsBuilder.And => this;

        IFinishBuildingOptions IHandOverToNewSectionOptionsBuilder.AndFinally => this;
    }
}