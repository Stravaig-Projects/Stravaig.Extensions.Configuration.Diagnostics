using Stravaig.Extensions.Configuration.Diagnostics.Matchers;
using Stravaig.Extensions.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Extensions.Configuration.Diagnostics.FluentOptions
{
    public partial class ConfigurationDiagnosticsOptionsBuilder
    {
        private ISecretObfuscator _obfuscator; 
        private IKeyMatchBuilder _configKeyMatcher; 
        private IKeyMatchBuilder _connectionStringKeyMatcher;
        private State _state = State.None; 
        private IKeyMatchBuilder _currentKeyMatcherBuilder;

        public ConfigurationDiagnosticsOptionsBuilder()
        {
            _configKeyMatcher = new KeyMatchBuilder();
            _connectionStringKeyMatcher = new KeyMatchBuilder();
        }
        public IObfuscatorOptionsBuilder Obfuscating => StartNewSection(State.BuildingObfuscator);

        public IKeyMatcherOptionsBuilder MatchingConfigurationKeys => StartNewSection(State.BuildingConfigurationKeyMatcher);

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