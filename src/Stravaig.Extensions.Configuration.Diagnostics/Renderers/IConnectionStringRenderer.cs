namespace Stravaig.Extensions.Configuration.Diagnostics.Renderers
{
    public interface IConnectionStringRenderer
    {
        MessageEntry Render(string connectionString, string name, ConfigurationDiagnosticsOptions options);
    }
}