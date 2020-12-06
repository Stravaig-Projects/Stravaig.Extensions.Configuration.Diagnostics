using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using Stravaig.Extensions.Configuration.Diagnostics.Matchers;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__helpers;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.Matchers
{
    [TestFixture]
    public class KeyMatchBuilderTests
    {
        [Test]
        public void AddNullMatcherThrowsArgumentNullException()
        {
            Should.Throw<ArgumentNullException>( 
                () => new KeyMatchBuilder().Add((IMatcher)null));
        }
        
        [Test]
        public void AddNullEnumerableMatcherThrowsArgumentNullException()
        {
            Should.Throw<ArgumentNullException>( 
                () => new KeyMatchBuilder().Add((IEnumerable<IMatcher>)null));
        }

        [Test]
        public void EmptyBuilderBuildsNullMatcher()
        {
            var matcher = new KeyMatchBuilder()
                .Build();
            matcher.ShouldNotBeNull();
            matcher.ShouldBeOfType<NullMatcher>();
        }

        [Test]
        public void BuilderWithSingleMatcherShouldReturnUnderlyingMatcher()
        {
            var singleMatcher = new ContainsMatcher("abc");
            var matcher = new KeyMatchBuilder()
                .Add(singleMatcher)
                .Build();
            matcher.ShouldNotBeNull();
            matcher.ShouldBeOfType<ContainsMatcher>();
            matcher.ShouldBe(singleMatcher);
        }

        [Test]
        public void BuilderWithMultipleMatchersShouldReturnAggregateMatcher()
        {
            var matcherOne = new ContainsMatcher("abc");
            var matcherTwo = new RegexMatcher("def");
            var matcher = new KeyMatchBuilder()
                .Add(new IMatcher[]
                {
                    matcherOne,
                    matcherTwo,
                })
                .Build();
            matcher.ShouldNotBeNull();
            matcher.ShouldBeOfType<AggregateMatcher>();
            
            dynamic aggregateMatcher = new Jailbreak<AggregateMatcher>((AggregateMatcher)matcher);
            List<IMatcher> matchers = aggregateMatcher._matchers;
            matchers.Count.ShouldBe(2);
            matchers.ShouldContain(m => m.GetType() == typeof(ContainsMatcher));
            matchers.ShouldContain(m => m.GetType() == typeof(RegexMatcher));
        }
    }
}