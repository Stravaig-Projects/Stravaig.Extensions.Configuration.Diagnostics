using Stravaig.Configuration.Diagnostics.Matchers;
using Stravaig.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Configuration.Diagnostics.FluentOptions
{
    /// <summary>
    /// The fluent configuration diagnostics options builder. 
    /// </summary>
    public partial class ConfigurationDiagnosticsOptionsBuilder
    {
        private readonly IKeyMatchBuilder _configKeyMatcher; 
        private readonly IKeyMatchBuilder _connectionStringKeyMatcher;

        private ISecretObfuscator _obfuscator; 
        private State _state = State.None; 
        private IKeyMatchBuilder _currentKeyMatcherBuilder;

        /// <summary>
        /// Initialises the configuration diagnostics options builder.
        /// </summary>
        public ConfigurationDiagnosticsOptionsBuilder()
        {
            _configKeyMatcher = new KeyMatchBuilder();
            _connectionStringKeyMatcher = new KeyMatchBuilder();
        }
        
        /// <summary>
        /// Starts the set up of the obfuscator.
        /// </summary>
        public IObfuscatorOptionsBuilder Obfuscating => StartNewSection(State.BuildingObfuscator);

        /// <summary>
        /// Starts the set up of the configuration key matcher.
        /// </summary>
        public IKeyMatcherOptionsBuilder MatchingConfigurationKeys => StartNewSection(State.BuildingConfigurationKeyMatcher);

        /// <summary>
        /// Starts the set up of the Connection String Keys
        /// </summary>
        public IKeyMatcherOptionsBuilder MatchingConnectionStringKeys => StartNewSection(State.BuildingConnectionStringKeyMatcher);
        
        private ConfigurationDiagnosticsOptionsBuilder StartNewSection(State newAction)
        {
            _state = newAction;
            switch (_state)
            {
                case State.BuildingConfigurationKeyMatcher:
                    _currentKeyMatcherBuilder = _configKeyMatcher;
                    break;
                case State.BuildingConnectionStringKeyMatcher:
                    _currentKeyMatcherBuilder = _connectionStringKeyMatcher;
                    break;
                default:
                    _currentKeyMatcherBuilder = null;
                    break;
            }

            return this;
        }
        
    }
}