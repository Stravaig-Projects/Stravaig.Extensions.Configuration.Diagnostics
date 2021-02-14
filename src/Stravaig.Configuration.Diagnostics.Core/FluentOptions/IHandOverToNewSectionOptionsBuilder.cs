namespace Stravaig.Configuration.Diagnostics.FluentOptions
{
    /// <summary>
    /// The part of the fluent interface that hands over to a new section.
    /// </summary>
    public interface IHandOverToNewSectionOptionsBuilder
    {
        /// <summary>
        /// Indicates that the configuration options are still being built.
        /// </summary>
        ConfigurationDiagnosticsOptionsBuilder And { get; }
        
        /// <summary>
        /// Indicates that the configuration options are about to be built in some way.
        /// </summary>
        IFinishBuildingOptions AndFinally { get; }
    }
    
    public partial class ConfigurationDiagnosticsOptionsBuilder
        : IHandOverToNewSectionOptionsBuilder
    {
        ConfigurationDiagnosticsOptionsBuilder IHandOverToNewSectionOptionsBuilder.And => this;

        IFinishBuildingOptions IHandOverToNewSectionOptionsBuilder.AndFinally => this;
    }
}