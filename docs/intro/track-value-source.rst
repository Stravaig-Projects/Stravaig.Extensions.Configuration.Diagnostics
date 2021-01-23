Configuration Value Source
==========================

You can track where a value came from with the ``LogConfigurationKeySource`` based methods. It will output the providers that have the given key, the last of which will be the provider that was used for the final value that is picked up by your application.

::

    logger.LogConfigurationKeySource(LogLevel level, IConfigurationRoot configRoot, string key, bool compressed = false, ConfigurationDiagnosticsOptions options = null)

Where:

* ``logger`` is the ``ILogger`` where you want the information logged.
* ``configRoot`` is the ``IConfigurationRoot`` that contains the provider information.
* ``level`` is the ``LogLevel`` at which you wish the information to be logged.
* ``key`` is the full key name of the configuration element you want to track.
* ``compressed`` indicates whether you want to skip providers that don't provide a value for this item. The default is ``false`` so all providers are listed even if they do not provide a value for the given ``key``. When ``true``, any providers that don't provide a value for the given ``key`` won't be mentioned.
* ``options`` are the options to use for logging. If not supplied then the ``ConfigurationDiagnosticsOptions.GlobalOptions`` are used.

Example Output
~~~~~~~~~~~~~~

::

    info: Stravaig.Extensions.Configuration.Diagnostics.Tests.ConfigurationProviderTrackingExtensionsTests[0]
        Provider sources for value of SomeSection:SomeKey
        * MemoryConfigurationProvider ==> "SomeValue"
        * MemoryConfigurationProvider ==> null
        * JsonStreamConfigurationProvider ==> "SomeNewValue"

Alternative calls
~~~~~~~~~~~~~~~~~

There are a number of calls that imply the logging level in the method name instead of supplying it as a parameter. These are:

::

    logger.LogConfigurationKeySourceAsInformation(IConfigurationRoot configRoot, string key, bool compressed = false, ConfigurationDiagnosticsOptions options = null)
    logger.LogConfigurationKeySourceAsDebug(IConfigurationRoot configRoot, string key, bool compressed = false, ConfigurationDiagnosticsOptions options = null)
    logger.LogConfigurationKeySourceAsTrace(IConfigurationRoot configRoot, string key, bool compressed = false, ConfigurationDiagnosticsOptions options = null)

