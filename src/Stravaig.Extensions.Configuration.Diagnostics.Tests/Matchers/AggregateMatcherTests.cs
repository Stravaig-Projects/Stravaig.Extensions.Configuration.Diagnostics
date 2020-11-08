using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using Stravaig.Extensions.Configuration.Diagnostics.Matchers;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__helpers;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.Matchers
{
    [TestFixture]
    public class AggregateMatcherTests
    {
        [Test]
        public void CtorThrowsArgumentNullExceptionWithNullParameter()
        {
            Should.Throw<ArgumentNullException>(
                () => new AggregateMatcher(null));
        }

        [Test]
        public void DefaultCtorInitialisesMatcher()
        {
            _ = new AggregateMatcher();
            
            // Nothing to assert, just make sure it doesn't break on init.
        }

        [Test]
        public void CtorWithMatchersInitialisedTheMatcher()
        {
            _ = new AggregateMatcher(Array.Empty<IMatcher>());
            
            // Nothing to assert, just make sure it doesn't break on init.
        }

        [Test]
        public void AddNullMatcherThrowsException()
        {
            var aggregateMatcher = new AggregateMatcher();

            Should.Throw<ArgumentNullException>(() =>
                aggregateMatcher.Add((IMatcher)null));
        }
        
        [Test]
        public void AddNullMatchersThrowsException()
        {
            var aggregateMatcher = new AggregateMatcher();

            Should.Throw<ArgumentNullException>(() =>
                aggregateMatcher.Add((IEnumerable<IMatcher>)null));
        }

        [Test]
        public void AddSelfThrowsException()
        {
            var aggregateMatcher = new AggregateMatcher();

            Should.Throw<ArgumentException>(() =>
                    aggregateMatcher.Add(aggregateMatcher))
                .Message.ShouldContain("circular");
        }
        
        [Test]
        public void AddNullMatcherSkipsAdd()
        {
            var aggregateMatcher = new AggregateMatcher();

            aggregateMatcher.Add(NullMatcher.Instance);

            dynamic jailBrokenMatcher = new Jailbreak<AggregateMatcher>(aggregateMatcher);
            var count = (int)jailBrokenMatcher._matchers.Count;
            count.ShouldBe(0);
        }

        [Test]
        public void AddGeneralMatcherAddsTheMatcher()
        {
            var aggregateMatcher = new AggregateMatcher();

            aggregateMatcher.Add(new ContainsMatcher("abc"));

            dynamic jailBrokenMatcher = new Jailbreak<AggregateMatcher>(aggregateMatcher);
            var count = (int)jailBrokenMatcher._matchers.Count;
            count.ShouldBe(1);
        }
        
        [Test]
        public void AddMultipleMatcherAddsAllTheMatchers()
        {
            var aggregateMatcher = new AggregateMatcher();

            aggregateMatcher.Add(new IMatcher[]
            {
                new ContainsMatcher("abc"),
                new RegexMatcher("def"),
            });

            dynamic jailBrokenMatcher = new Jailbreak<AggregateMatcher>(aggregateMatcher);
            var count = (int)jailBrokenMatcher._matchers.Count;
            count.ShouldBe(2);
        }

        [TestCase("Nobody can be uncheered with a balloon.", ExpectedResult = true)]
        [TestCase("Love is a better teacher than duty.", ExpectedResult = true)]
        [TestCase("The road to success is always under construction.", ExpectedResult = false)]
        public bool AggregateMatcherMatchesAnyValidMatch(string key)
        {
            var matcher = new AggregateMatcher(new IMatcher[]
            {
                new ContainsMatcher("uncheered"),
                new ContainsMatcher("teacher"),
            });

            return matcher.IsMatch(key);
        }
    }
}