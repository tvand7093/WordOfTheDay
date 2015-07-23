using System;
using FluentAssertions;
using NUnit.Framework;
using WordOfTheDay.Structures;
using WordOfTheDay.Models;
using PCLStorage;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WordOfTheDayTests.Helpers;

namespace WordOfTheDayTests.Structures
{
	[TestFixture]
	public class FileService_Unit_Tests
	{
		Word data;

		[SetUp]
		public void Setup()
		{
			Cleanup.CleanCache ();
			data = Generate.CachedWord ();
		}

		[TearDown]
		public void Teardown()
		{
			Cleanup.CleanCache ();
		}

		[Test]
		public void HasCorrectCacheFileName ()
		{
			FileService.CacheFile.Should ().Be ("CachedWord.json");
		}

		[Test]
		public async Task SavesFile()
		{
			FileService.SaveWordAsync (data);
			var exists = await FileSystem.Current.LocalStorage.CheckExistsAsync (FileService.CacheFile);
			exists.Should ().Be (ExistenceCheckResult.FileExists);
		}

		[Test]
		public async Task SavesWordCorrectly()
		{
			FileService.SaveWordAsync (data);
			data.Date = data.Date.Date;

			var file = await FileSystem.Current.LocalStorage.GetFileAsync (FileService.CacheFile);
			var json = JsonConvert.DeserializeObject<Word>(await file.ReadAllTextAsync ());

			json.Date.Should ().Be (data.Date);
			json.EnglishWord.Should ().Be (data.EnglishWord);
			json.EnglishExample.Should ().Be (data.EnglishExample);
			json.TodaysWord.Should ().Be (data.TodaysWord);
			json.TodaysExample.Should ().Be (data.TodaysExample);
		}

		[Test]
		public async Task ReturnsNoCachedWord()
		{
			var word = await FileService.LoadWordAsync ();
			word.Should ().BeNull ();
		}

		[Test]
		public async Task ReturnsNullForDifferentDay()
		{
			FileService.SaveWordAsync (data);
			var word = await FileService.LoadWordAsync ();
			word.Should ().BeNull ();
		}

		[Test]
		public async Task ReturnsACachedWord()
		{
			data.Date = DateTime.Now.Date;
			FileService.SaveWordAsync (data);
			var word = await FileService.LoadWordAsync ();
			word.Should ().NotBeNull ();
		}

		[Test]
		public async Task ReturnsCorrectCachedWord()
		{
			data.Date = DateTime.Now.Date;
			FileService.SaveWordAsync (data);
			var word = await FileService.LoadWordAsync ();
			word.Date.Should ().Be (data.Date);
			word.EnglishWord.Should ().Be (data.EnglishWord);
			word.EnglishExample.Should ().Be (data.EnglishExample);
			word.TodaysWord.Should ().Be (data.TodaysWord);
			word.TodaysExample.Should ().Be (data.TodaysExample);
		}
	}
}

