using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Stravaig.Configuration.Diagnostics.Renderers
{
    /// <summary>
    /// A structured renderer that renders the providers of a configuration.
    /// </summary>
    public class StructuredConfigurationProviderRenderer : Renderer, IConfigurationProviderRenderer
    {
        /// <summary>
        /// The single instance of this class.
        /// </summary>
        public static readonly StructuredConfigurationProviderRenderer Instance = new StructuredConfigurationProviderRenderer();

        /// <inheritdoc />
        public MessageEntry Render(IConfigurationRoot configuration)
        {
            List<object> args = new List<object>();
            StringBuilder messageTemplate = new StringBuilder("The following configuration providers were registered:" + Environment.NewLine);

            var indexedProviders = configuration.Providers.Select((p, i) => (i, p));

            foreach (var (index, provider) in indexedProviders)
            {
                messageTemplate.AppendLine(Placeholder("Provider", index.ToString(CultureInfo.InvariantCulture)));
                args.Add(provider.ToString());
            }
            
            var objArgs = args.ToArray();
            return new MessageEntry(messageTemplate.ToString(), objArgs);
        }
    }
}