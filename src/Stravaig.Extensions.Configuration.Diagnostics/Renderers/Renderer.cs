using System.Text;

namespace Stravaig.Extensions.Configuration.Diagnostics.Renderers
{
    /// <summary>
    /// Renders an aspect of the configuration system.
    /// </summary>
    public abstract class Renderer
    {
        private const string PlaceholderPartJoin = "_";
        
        /// <summary>
        /// Creates a safely named placeholder for use in structured renderers.
        /// </summary>
        /// <param name="parts">The parts that make up the placeholder name.</param>
        /// <returns>A placeholder, including the braces.</returns>
        protected string Placeholder(params string[] parts)
        {
            StringBuilder placeholderBuilder = new StringBuilder();
            placeholderBuilder.Append("{");
            int partPos = 0;
            foreach (var part in parts)
            {
                if (string.IsNullOrWhiteSpace(part))
                    continue;
                
                if (partPos != 0)
                    placeholderBuilder.Append(PlaceholderPartJoin);
                
                int charPos = 0;
                foreach (char character in part)
                {
                    if (charPos == 0 && character >= '0' && character <= '9')
                        placeholderBuilder.Append("_");
                    if (char.IsLetterOrDigit(character) || character == '.')
                        placeholderBuilder.Append(character);
                    else
                        placeholderBuilder.Append("_");
                    charPos++;
                }

                partPos++;
            }

            placeholderBuilder.Append("}");
            return placeholderBuilder.ToString();
        }
    }
}