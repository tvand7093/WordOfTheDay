using System;
using NUnit.Framework;
using WordOfTheDay.Models;
using FluentAssertions;
using WordOfTheDayTests.Helpers;

namespace WordOfTheDayTests.Models
{
	[TestFixture]
	public class LanguageInfoTests
	{
		[Test]
		public void SetsLanguageCorrectly ()
		{
			var li = new LanguageInfo (Language.Italian);
			li.Language.Should ().Be (Language.Italian);
		}

		[Test]
		public void ApiUrlFormatsCorrectlySingleWord ()
		{
			const string expected 
			= "http://www.transparent.com/word-of-the-day/today/italian.html";
			var li = new LanguageInfo (Language.Italian);
			li.ApiUrl.Should ().Be (expected);
		}

		[Test]
		public void ApiUrlFormatsCorrectlyMultipleWord ()
		{
			const string expected 
			= "http://www.transparent.com/word-of-the-day/today/english-portuguese.html";
			var li = new LanguageInfo (Language.EnglishForPortugueseSpeakers);
			li.ApiUrl.Should ().Be (expected);
		}
		[Test]
		public void RSSUrlFormatsCorrectlySingleWord ()
		{
			const string expected 
			= "http://feeds.feedblitz.com/italian-word-of-the-day&x=1";
			var li = new LanguageInfo (Language.Italian);
			li.RSSUrl.Should ().Be (expected);
		}

		[Test]
		public void RSSUrlFormatsCorrectlyMultipleWord ()
		{
			const string expected 
			= "http://feeds.feedblitz.com/english-word-of-the-day-for-portuguese&x=1";
			var li = new LanguageInfo (Language.EnglishForPortugueseSpeakers);
			li.RSSUrl.Should ().Be (expected);
		}

		[Test]
		public void ComparesGreaterThan ()
		{
			var li = new LanguageInfo (Language.Urdu);
			var li2 = new LanguageInfo (Language.Arabic);
			li.Should ().BeGreaterThan (li2);
		}

		[Test]
		public void ComparesLessThan ()
		{
			var li = new LanguageInfo (Language.Arabic);
			var li2 = new LanguageInfo (Language.Urdu);
			li.Should ().BeLessThan (li2);
		}

		[Test]
		public void ComparesEqual ()
		{
			var li = new LanguageInfo (Language.Swedish);
			var li2 = new LanguageInfo (Language.Swedish);
			li.Should ().Be (li2);
		}
	}
}

