Configuration Provider Information
==================================

Configuration Providers are the sources of configuration
information. These are things such as ``appsetting.json``
files or Environment Variables.

Log Provider Type Names
-----------------------

This simply logs the provider types in sequence.

::

    logger.LogProviderNames(IConfigurationRoot configRoot, LogEventLevel level)

Where:

* ``logger`` is the ``ILogger`` where you want the information logged.
* ``configRoot`` is the ``IConfigurationRoot`` that contains the provider information.
* ``level`` is the ``LogEventLevel`` at which you wish the information to be logged.


Example output
~~~~~~~~~~~~~~

::

    info: Stravaig.Extensions.Configuration.Diagnostics.Tests.MultipleProviderNameTests[0]
        The following configuration providers were registered:
        Microsoft.Extensions.Configuration.Memory.MemoryConfigurationProvider
        Microsoft.Extensions.Configuration.Json.JsonConfigurationProvider
        Microsoft.Extensions.Configuration.EnvironmentVariables.EnvironmentVariablesConfigurationProvider
        Microsoft.Extensions.Configuration.CommandLine.CommandLineConfigurationProvider

Alternative calls
~~~~~~~~~~~~~~~~~

::

    logger.LogProviderNamesAsInformation(IConfigurationRoot configRoot)
    logger.LogProviderNamesAsDebug(IConfigurationRoot configRoot)
    logger.LogProviderNamesAsVerbose(IConfigurationRoot configRoot)

These operate the same as before but the log level is indicated in the method name.

Log Providers
-------------

This logs the provider details. This can be more helpful than logging the provider type names as some providers supply additional context, such as which ``appsettings.<env>.json`` has been used.

::

    logger.LogProviders(IConfigurationRoot configRoot, LogEventLevel logEventLevel)

Where:

* ``logger`` is the ``ILogger`` where you want the information logged.
* ``configRoot`` is the ``IConfigurationRoot`` that contains the provider information.
* ``level`` is the ``LogEventLevel`` at which you wish the information to be logged.

Example output
~~~~~~~~~~~~~~

::

    info: Stravaig.Extensions.Configuration.Diagnostics.Tests.MultipleProvidersTests[0]
        The following configuration providers were registered:
        MemoryConfigurationProvider
        JsonConfigurationProvider for 'appsettings.json' (Optional)
        JsonConfigurationProvider for 'appsettings.test.json' (Optional)
        EnvironmentVariablesConfigurationProvider
        CommandLineConfigurationProvider

Alternative calls
~~~~~~~~~~~~~~~~~

::

    logger.LogProvidersAsInformation(IConfigurationRoot configRoot)
    logger.LogProvidersAsDebug(IConfigurationRoot configRoot)
    logger.LogProvidersAsVerbose(IConfigurationRoot configRoot)

These operate the same as before but the log level is indicated in the method name.
