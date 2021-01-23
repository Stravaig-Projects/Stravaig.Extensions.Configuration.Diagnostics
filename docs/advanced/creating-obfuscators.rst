.. _refCreatingObfuscators:

Creating obfuscators
====================

Obfuscators are classes or functions that replace a secret with some sort of unintelligible output. To create one you just need to create a class that implements the ``ISecretObfuscator`` interface.

ISecretObfuscator
-----------------

You can create a obfuscator class by implementing the interface ``ISecretObfuscator``.

::

    public interface ISecretObfuscator
    {
        string Obfuscate(string secret);
    }

The interface has one method, ``Obfuscate``, to implement. It takes a string (the secret) and returns a string (the obfuscated secret)

Example Class Implementation
~~~~~~~~~~~~~~~~~~~~~~~~~~~~

::

    public class MyCustomObfuscator : ISecretObfuscator
    {
        public string Obfuscate(string secret)
        {
            if (string.IsNullOrEmpty(secret))
                return string.Empty;
            
            return new string('*', secret.Length);
        }
    }

Adding your custom obfuscator to the ConfigurationDiagnosticsOptions
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

When configuring the obfuscators, the ``When(...)``` method takes an ``IObfuscator``

::

    ConfigurationDiagnosticsOptions.SetupBy
        .Obfuscating.With(new MyCustomObfuscator())
        ...

Func<string, string>
--------------------

You can create an obfuscator function that takes a string (the secret) and returns a string (the obfuscated secret).

::

    Func<string, string>

Example Function Implementation
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

::

    Func<string, string> obfuscator = s => new string('*', s?.Length ?? 0)

Adding your obfuscator function to the ConfigurationDiagnosticsOptions
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

When configuring the obfuscators, the ``When(...)``` method takes the ``Func<string, string>``

::

    ConfigurationDiagnosticsOptions.SetupBy
        .Obfuscating.With(s => new string('*', s?.Length ?? 0))
        ...
