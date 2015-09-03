using System;
using FluentAssertions;
using NUnit.Framework;
using WordOfTheDay.Structures;

namespace WordOfTheDayTests.Structures
{
	[TestFixture]
	public class Configuration_Unit_Tests
	{
		[Test]
		public void InsightsKeyLoaded ()
		{
			Configuration.InsightsApiKey.Should ().NotBeNullOrEmpty ();
		}
	}
}

