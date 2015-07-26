using System;
using WordOfTheDay.Interfaces;
using Moq;
using Xamarin.Forms;
using WordOfTheDay.Models;
using WordOfTheDay.Structures;
using System.IO;

namespace WordOfTheDayTests.Helpers
{
	public static class Generate
	{
		public static async void ConfigureFeedService()
		{
			using(var sr = new StreamReader("./Html/wotd.html"))
			{
				FeedService.TestHTML = await sr.ReadToEndAsync ();
			}
		}
		public static IApplication GetApp()
		{
			var mockApp = new Mock<IApplication> ();
			var mockPage = new Mock<Page> ();
			mockApp.Setup (a => a.MainPage).Returns(mockPage.Object);
			return mockApp.Object;
		}

		public static Word HtmlWord ()
		{
			return new Word () 
			{
				Date = new DateTime(2015, 7, 23),
				EnglishWord = "to hold",
				TodaysWord = "tenere",
				EnglishExample = "If you hold it too tightly, the egg will break.",
				TodaysExample = "Se lo tieni così stretto, l'uovo si rompe.",
				PartOfSpeech = "verb",
				WordLanguage = new LanguageInfo(Language.Italian)
			};
		}
		public static Word CachedWord ()
		{
			return new Word () 
			{
				Date = new DateTime(2015, 3, 15),
				EnglishWord = "Word",
				TodaysWord = "Parola",
				EnglishExample = "I can speak more than one word.",
				TodaysExample = "Posso parlare più di una parola.",
				PartOfSpeech = "Noun",
				WordLanguage = new LanguageInfo(Language.Italian)
			};
		}
	}
}

