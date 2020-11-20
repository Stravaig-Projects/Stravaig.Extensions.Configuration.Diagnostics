using Stravaig.Extensions.Configuration.Diagnostics.Matchers;
using Stravaig.Extensions.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Extensions.Configuration.Diagnostics.FluentOptions
{
    public partial class ConfigurationDiagnosticsOptionsBuilder 
        : IConfigurationKeyMatcherOptionsBuilder,
            IConnectionStringKeyMatcherOptionsBuilder
    {
        private ISecretObfuscator _obfuscator; 
        private IMatcher _configKeyMatcher; 
        private IMatcher _connectionStringKeyMatcher;
        private State _state = State.None; 
        private IKeyMatchBuilder _currentKeyMatcherBuilder; 

        public IObfuscatorOptionsBuilder Obfuscating => StartNewSection(State.BuildingObfuscator);

        public IConfigurationKeyMatcherOptionsBuilder MatchingConfigurationKeys => StartNewSection(State.BuildingConfigurationKeyMatcher);

        public IConnectionStringKeyMatcherOptionsBuilder MatchingConnectionStringKeys => StartNewSection(State.BuildingConnectionStringKeyMatcher);

        private ConfigurationDiagnosticsOptionsBuilder StartNewSection(State newAction)
        {
            CloseOffOldState();
            _state = newAction; 
            _currentKeyMatcherBuilder = new KeyMatchBuilder();
            return this;
        }
        
        private void CloseOffOldState() 
        { 
            switch (_state) 
            { 
                case State.BuildingConfigurationKeyMatcher: 
                    _configKeyMatcher = _currentKeyMatcherBuilder.Build(); 
                    break; 
                case State.BuildingConnectionStringKeyMatcher: 
                    _connectionStringKeyMatcher = _currentKeyMatcherBuilder.Build(); 
                    break; 
                case State.BuildingObfuscator: 
                case State.None: 
                    return; 
            }
        } 
    }
}