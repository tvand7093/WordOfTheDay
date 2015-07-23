using System;

namespace WordOfTheDay.Models
{
	public class Word
	{
		private const string APIUrlFormat 
			= "http://www.transparent.com/word-of-the-day/today/italian.html?date={0}";

		public string EnglishWord {get;set;}
		public string TodaysWord {get;set;}
		public string PartOfSpeach {get;set;}
		public string EnglishExample {get;set;}
		public string TodaysExample {get;set;}
		public DateTime Date {get;set;}

		string url;
		public string Url 
		{
			get {
				if (String.IsNullOrEmpty (url))
					url = string.Format (APIUrlFormat, Date.ToString ("MM-dd-yyyy"));
				return url;
			}
		}
	}
}

