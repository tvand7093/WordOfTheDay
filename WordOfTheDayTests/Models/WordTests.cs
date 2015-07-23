using System;
using NUnit.Framework;
using WordOfTheDay.Models;
using FluentAssertions;

namespace WordOfTheDayTests.Models
{
	[TestFixture]
	public class WordTests
	{
		[Test]
		public void UrlFormatsCorrectly ()
		{
			var expected 
				= "http://www.transparent.com/word-of-the-day/today/italian.html?date=07-23-2015";
			var word = new Word () {
				Date = new DateTime(2015, 7, 23)
			};
			word.Url.Should ().Be (expected);
		}
	}
}

