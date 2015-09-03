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
		public async Task SavesFile()
		{
			await FileService.SaveWordAsync (data);
			var file = string.Format ("CachedWord-Italian.json");
			var exists = await FileSystem.Current.LocalStorage.CheckExistsAsync (file);
			exists.Should ().Be (ExistenceCheckResult.FileExists);
		}

		[Test]
		public async Task SavesWordCorrectly()
		{
			await FileService.SaveWordAsync (data);
			data.Date = data.Date.Date;
			var fileName = string.Format ("CachedWord-Italian.json");

			var file = await FileSystem.Current.LocalStorage.GetFileAsync (fileName);
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
			var word = await FileService.LoadWordAsync (Language.Italian);
			word.Should ().BeNull ();
		}

		[Test]
		public async Task ReturnsNullForDifferentDay()
		{

			await FileService.SaveWordAsync (data);
			var word = await FileService.LoadWordAsync (Language.Italian);
			word.Should ().BeNull ();
		}

		[Test]
		public async Task ReturnsACachedWord()
		{
			data.Date = DateTime.Now.Date;
			await FileService.SaveWordAsync (data);
			var word = await FileService.LoadWordAsync (Language.Italian);
			word.Should ().NotBeNull ();
		}

		[Test]
		public async Task ReturnsCorrectCachedWord()
		{
			data.Date = DateTime.Now.Date;
			await FileService.SaveWordAsync (data);
			var word = await FileService.LoadWordAsync (Language.Italian);
			word.Date.Should ().Be (data.Date);
			word.EnglishWord.Should ().Be (data.EnglishWord);
			word.EnglishExample.Should ().Be (data.EnglishExample);
			word.TodaysWord.Should ().Be (data.TodaysWord);
			word.TodaysExample.Should ().Be (data.TodaysExample);
			word.WordLanguage.Should ().Be (new LanguageInfo(Language.Italian));
		}

		[Test]
		public async Task SavesMultipleLanguageFiles()
		{
			await FileService.SaveWordAsync (data);
			var word2 = data;
			word2.WordLanguage = new LanguageInfo (Language.Chinese);
			await FileService.SaveWordAsync (word2);

			var file = string.Format ("CachedWord-Italian.json");
			var exists = await FileSystem.Current.LocalStorage.CheckExistsAsync (file);
			exists.Should ().Be (ExistenceCheckResult.FileExists);

			file = string.Format ("CachedWord-Chinese.json");
			exists = await FileSystem.Current.LocalStorage.CheckExistsAsync (file);
			exists.Should ().Be (ExistenceCheckResult.FileExists);
		}

		[Test]
		public async Task SavesMultipleWordsWithCorrectValues()
		{
			data.Date = DateTime.Now.Date;
			await FileService.SaveWordAsync (data);
			var word2 = new Word()
			{
				PartOfSpeech = "Adverb",
				Date = data.Date,
				TodaysWord = data.TodaysWord,
				TodaysExample = data.TodaysExample,
				EnglishWord = data.EnglishWord,
				EnglishExample = data.EnglishExample,
				WordLanguage = new LanguageInfo(Language.Chinese)
			};
			await FileService.SaveWordAsync (word2);

			var firstWord = await FileService.LoadWordAsync (Language.Italian);
			var secondeWord = await FileService.LoadWordAsync (Language.Chinese);

			firstWord.Should ().Be (data);
			secondeWord.Should ().Be (word2);
		}
	}
}

