using System;
using NUnit.Framework;
using WordOfTheDay.Models;
using FluentAssertions;
using WordOfTheDayTests.Helpers;
using System.Linq;
using WordOfTheDay.Helpers;

namespace WordOfTheDayTests.Models
{
	[TestFixture]
	public class LanguageInfoTests
	{

		[Test]
		public void ParseLanguageDefaultsToItalian()
		{
			LanguageInfo.ParseLanguage ("italian stuff").Should ().Be (Language.Italian);
		}

		[Test]
		public void ParseLanguageWordsOnSingleWord()
		{
			LanguageInfo.ParseLanguage ("german").Should ().Be (Language.German);
		}

		[Test]
		public void ParseLanguageWordsOnMultipleWords()
		{
			LanguageInfo.ParseLanguage ("english for portuguese")
				.Should ().Be (Language.EnglishForPortugueseSpeakers);
		}

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

		[Test]
		public void AllLanguageHasCorrectValues()
		{
			var languages = new String[] {
				"Arabic",
				"Balinese",
				"Balinese For Indonesian",
				"Chinese",
				"Dari",
				"Dutch",
				"English For Portuguese",
				"English For Spanish",
				"Esperanto",
				"French",
				"German",
				"Hebrew",
				"Hindi",
				"Indonesian",
				"Irish",
				"Italian",
				"Japanese",
				"Korean",
				"Latin",
				"Norwegian",
				"Pashto",
				"Polish",
				"Portuguese",
				"Russian",
				"Spanish",
				"Swedish",
				"Turkish",
				"Urdu"
			};
				
			foreach (var lang in LanguageInfo.AllLanguages) {
				languages.Contains (lang).Should().BeTrue();
			}
		}
	}
}

