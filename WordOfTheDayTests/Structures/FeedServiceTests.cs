using System;
using FluentAssertions;
using NUnit.Framework;
using WordOfTheDay.Structures;
using WordOfTheDay.Models;
using PCLStorage;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using WordOfTheDayTests.Helpers;

namespace WordOfTheDayTests.Structures
{
	[TestFixture]
	public class FeedService_Unit_Tests
	{
		[SetUp]
		public void Setup()
		{
			Cleanup.CleanCache ();
		}

		[TearDown]
		public void TearDown() 
		{
			Cleanup.CleanCache ();
		}

		[Test]
		public async Task ReturnsNewWord ()
		{
			var word = await FeedService.GetWordAsync();
			word.Should ().NotBeNull ();
		}
	}

	[TestFixture]
	public class FeedService_Regression_Tests
	{
		Word expected;

		[SetUp]
		public void Setup()
		{
			Cleanup.CleanCache ();

			expected = Generate.HtmlWord ();

			Generate.ConfigureFeedService ();
		}

		[TearDown]
		public void TearDown() 
		{
			Cleanup.CleanCache ();
		}

		[Test]
		public async Task WordsAreDifferent ()
		{
			var word = await FeedService.GetWordAsync();
			word.EnglishWord.Should ().Be (expected.EnglishWord);
			word.TodaysWord.Should ().Be (expected.TodaysWord);
		}
	}

	[TestFixture]
	public class FeedService_Integration_Tests
	{
		Word cached;
		Word expected;

		[SetUp]
		public void Setup()
		{
			Cleanup.CleanCache ();
			cached = Generate.CachedWord ();
			expected = Generate.HtmlWord ();

			Generate.ConfigureFeedService ();
		}

		[TearDown]
		public void TearDown() 
		{
			FeedService.TestHTML = null;
			Cleanup.CleanCache ();
		}

		[Test]
		public async Task ReturnsAWord ()
		{
			FileService.SaveWordAsync (cached);
			var word = await FeedService.GetWordAsync();
			word.Should ().NotBeNull ();
		}

		[Test]
		public async Task ParsesNewWordCorrectly ()
		{
			var word = await FeedService.GetWordAsync();

			word.Date.Should ().Be (expected.Date);
			word.EnglishWord.Should ().Be (expected.EnglishWord);
			word.EnglishExample.Should ().Be (expected.EnglishExample);
			word.TodaysWord.Should ().Be (expected.TodaysWord);
			word.TodaysExample.Should ().Be (expected.TodaysExample);
		}

		[Test]
		public async Task ReturnsCorrectCachedWord ()
		{
			cached.Date = DateTime.Now.Date;
			FileService.SaveWordAsync (cached);

			var word = await FeedService.GetWordAsync();
			word.Date.Should ().Be (cached.Date);
			word.EnglishWord.Should ().Be (cached.EnglishWord);
			word.EnglishExample.Should ().Be (cached.EnglishExample);
			word.TodaysWord.Should ().Be (cached.TodaysWord);
			word.TodaysExample.Should ().Be (cached.TodaysExample);
		}
	}
}

