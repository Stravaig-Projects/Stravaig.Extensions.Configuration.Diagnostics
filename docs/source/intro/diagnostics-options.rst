Diagnostics options
===================

Out of the box the diagnostics logging will log everything. However, most configured systems will have some secrets such as database passwords, API keys and the like. In order that this can be used to help diagnose information in deployed systems, where the log is readable by many people, you can set up options to obfuscate secret values.

You can either set the options at a global level, accessible via ``ConfigurationDiagnosticsOptions.GlobalOptions`` or you can set up your own options and inject them into each call.

The options are set up using a fluent interface.

To start with use:

::

    ConfigurationDiagnosticsOptions.SetupBy

This will initiate the set up of options. From here you can decide whether you want to set up options to do with secret key matching on the configuration keys or connection string keys, or setup how you'd like secrets to be obfuscated.


Obfuscating
-----------

Obfuscating is making secrets unintelligible. There are a few built in obfuscators that you can use.

For example: 

::

    ConfigurationDiagnosticsOptions.SetupBy
        .Obfuscating.ByRedacting()

This will replace the secret with the word "REDACTED"


.Obfuscating.ByRedacting
~~~~~~~~~~~~~~~~~~~~~~~~

There are three variants:

* ``ByRedacting()`` replaces the secret with the word "REDACTED" in all caps.
* ``ByRedacting(string)`` add the given string before and after the word "REDACTED".
* ``ByRedacting(string, string)`` adds the given strings before and after the word "REDACTED".

e.g.

::

    // Secret == "REDACTED"
    ConfigurationDiagnosticsOptions.SetupBy
        .Obfuscating.ByRedacting()

    // Secret == "**REDACTED**"
    ConfigurationDiagnosticsOptions.SetupBy
        .Obfuscating.ByRedacting("**")

    // Secret == "[REDACTED]"
    ConfigurationDiagnosticsOptions.SetupBy
        .Obfuscating.ByRedacting("[", "]")

.Obfuscating.WithAsterisks
~~~~~~~~~~~~~~~~~~~~~~~~~~

Replaces the secret with asterisks of the same length.

::

    // "Secret" ==> "******"
    // "Super Secret" ==> "************" etc.
    ConfigurationDiagnosticsOptions.SetupBy
        .Obfuscating.WithAsterisks()

.Obfuscating.WithFixedAsterisks
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Replaces the secret with a fixed number of asterisks.

There are two variants:

* ``WithFixedAsterisks()`` replaces the secret with ``****``.
* ``WithFixedAsterisks(int)`` replaces the secret with the given number of asterisks.

e.g.

::

    // Secret == "****"
    ConfigurationDiagnosticsOptions.SetupBy
        .Obfuscating.WithFixedAsterisks()

    // Secret == "**"
    ConfigurationDiagnosticsOptions.SetupBy
        .Obfuscating.WithFixedAsterisks(2)

.Obfuscating.With
~~~~~~~~~~~~~~~~~

There are two variants: 

* ``With(ISecretObfuscator)`` Allows you to use your own obfuscator implementation.
* ``With(Func<string, string>)`` Allows you to supply a function that will obfuscate the secret.

See :ref:`Creating Obfuscators <refCreatingObfuscators>` for more.