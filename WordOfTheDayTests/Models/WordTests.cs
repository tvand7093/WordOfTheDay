using System;
using NUnit.Framework;
using WordOfTheDay.Models;
using FluentAssertions;
using WordOfTheDayTests.Helpers;

namespace WordOfTheDayTests.Models
{
	[TestFixture]
	public class WordTests
	{
		[Test]
		public void UrlFormatsCorrectly ()
		{
			const string expected 
				= "http://www.transparent.com/word-of-the-day/today/italian.html?date=07-23-2015";
			var word = new Word () {
				Date = new DateTime(2015, 7, 23)
			};
			word.Url.Should ().Be (expected);
		}

		[Test]
		public void Compares_EqualsSame() 
		{
			var word = Generate.CachedWord ();
			var word2 = Generate.CachedWord ();
			word.CompareTo (word2).Should ().Be (0);
		}

		[Test]
		public void Compares_LessThanDate() 
		{
			var word = Generate.CachedWord ();
			var word2 = Generate.CachedWord ();
			word2.Date = word2.Date.AddDays (1);
			word.CompareTo (word2).Should ().Be (-1);
		}

		[Test]
		public void Compares_GreaterThanDate() 
		{
			var word = Generate.CachedWord ();
			var word2 = Generate.CachedWord ();
			word.Date = word.Date.AddDays (1);
			word.CompareTo (word2).Should ().Be (1);
		}

		[Test]
		public void Compares_LessThanTodaysWord() 
		{
			var word = Generate.CachedWord ();
			var word2 = Generate.CachedWord ();
			word2.TodaysWord += "a";
			word.CompareTo (word2).Should ().Be (-1);
		}

		[Test]
		public void Compares_GreaterThanTodaysWord() 
		{
			var word = Generate.CachedWord ();
			var word2 = Generate.CachedWord ();
			word.TodaysWord	+= "a";
			word.CompareTo (word2).Should ().Be (1);
		}

		[Test]
		public void Compares_LessThanTodaysExample() 
		{
			var word = Generate.CachedWord ();
			var word2 = Generate.CachedWord ();
			word2.TodaysExample += "a";

			word.CompareTo (word2).Should ().Be (-1);
		}

		[Test]
		public void Compares_GreaterThanTodaysExample() 
		{
			var word = Generate.CachedWord ();
			var word2 = Generate.CachedWord ();
			word.TodaysExample += "a";
			word.CompareTo (word2).Should ().Be (1);
		}

		[Test]
		public void Compares_LessThanEnglishWord() 
		{
			var word = Generate.CachedWord ();
			var word2 = Generate.CachedWord ();
			word2.EnglishWord += "a";

			word.CompareTo (word2).Should ().Be (-1);
		}

		[Test]
		public void Compares_GreaterThanEnglishWord() 
		{
			var word = Generate.CachedWord ();
			var word2 = Generate.CachedWord ();
			word.EnglishWord += "a";
			word.CompareTo (word2).Should ().Be (1);
		}

		[Test]
		public void Compares_LessThanEnglishExample() 
		{
			var word = Generate.CachedWord ();
			var word2 = Generate.CachedWord ();
			word2.EnglishExample += "a";

			word.CompareTo (word2).Should ().Be (-1);
		}

		[Test]
		public void Compares_GreaterThanEnglishExample() 
		{
			var word = Generate.CachedWord ();
			var word2 = Generate.CachedWord ();
			word.EnglishExample += "a";
			word.CompareTo (word2).Should ().Be (1);
		}

		[Test]
		public void Compares_LessThanPartOfSpeach() 
		{
			var word = Generate.CachedWord ();
			var word2 = Generate.CachedWord ();
			word2.PartOfSpeech += "a";

			word.CompareTo (word2).Should ().Be (-1);
		}

		[Test]
		public void Compares_GreaterThanPartOfSpeach() 
		{
			var word = Generate.CachedWord ();
			var word2 = Generate.CachedWord ();
			word.PartOfSpeech += "a";
			word.CompareTo (word2).Should ().Be (1);
		}
	}
}

