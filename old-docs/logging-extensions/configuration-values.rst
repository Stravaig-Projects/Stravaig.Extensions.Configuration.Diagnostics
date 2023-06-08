Configuration Values
====================

This allows you to dump a complete set of configuration values to the log.

::

    logger.LogConfigurationValues(IConfiguration config, LogLevel level, ConfigurationDiagnosticsOptions options = null)

Where:

* ``logger`` is the ``ILogger`` where you want the information logged.
* ``config`` is the ``IConfiguration`` that contains the configuration information.
* ``level`` is the ``LogLevel`` at which you wish the information to be logged.
* ``options`` are the options to use for logging. If not supplied then the ``ConfigurationDiagnosticsOptions.GlobalOptions`` are used.

Example output
--------------

::

    info: Example.Startup[0]
        The following values are available:
        AllowedHosts : *
        ALLUSERSPROFILE : C:\ProgramData
        APPDATA : C:\Users\colin\AppData\Roaming
        applicationName : Example
        ASPNETCORE_ENVIRONMENT : Development
        ASPNETCORE_URLS : https://localhost:5001;http://localhost:5000
        ENVIRONMENT : Development
        HOMEDRIVE : C:
        Logging :
        Logging:LogLevel :
        Logging:LogLevel:Default : Information
        Logging:LogLevel:Microsoft : Warning
        Logging:LogLevel:Microsoft.Hosting.Lifetime : Information
    ...

Alternative calls
-----------------

::

    logger.LogConfigurationValuesAsInformation(IConfiguration config, ConfigurationDiagnosticsOptions options = null)
    logger.LogConfigurationValuesAsDebug(IConfiguration config, ConfigurationDiagnosticsOptions options = null)
    logger.LogConfigurationValuesAsTrace(IConfiguration config, ConfigurationDiagnosticsOptions options = null)
