using System;

namespace WordOfTheDay.Models
{
	public class Word
	{
		public string EnglishWord {get;set;}
		public string TodaysWord {get;set;}
		public string PartOfSpeach {get;set;}
		public string EnglishExample {get;set;}
		public string TodaysExample {get;set;}
		public DateTime Date {get;set;}
	}
}

