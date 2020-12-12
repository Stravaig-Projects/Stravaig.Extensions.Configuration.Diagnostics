using Microsoft.Extensions.Configuration;

namespace Stravaig.Extensions.Configuration.Diagnostics.Renderers
{
    public interface IAllConnectionStringsRenderer
    {
        MessageEntry Render(IConfiguration config, ConfigurationDiagnosticsOptions options);
    }
}