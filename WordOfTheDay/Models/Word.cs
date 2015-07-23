using System;

namespace WordOfTheDay.Models
{
	public class Word : IComparable<Word>
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

		#region IComparable implementation

		public int CompareTo (Word obj)
		{
			if (obj == null)
				throw new NullReferenceException ();
			
			if (obj.Date.CompareTo(Date) == 0
				&& obj.EnglishExample.CompareTo(EnglishExample) == 0
				&& obj.EnglishWord.CompareTo(EnglishWord) == 0
				&& obj.TodaysWord.CompareTo(TodaysWord) == 0
				&& obj.TodaysExample.CompareTo(TodaysExample) == 0
				&& obj.PartOfSpeach.CompareTo(PartOfSpeach) == 0)
				return 0;

			if (Date.CompareTo (obj.Date) != 0)
				return Date.CompareTo (obj.Date);
			else {
				if (EnglishExample.CompareTo (obj.EnglishExample) != 0)
					return EnglishExample.CompareTo (obj.EnglishExample);
				else
				{
					if (EnglishWord.CompareTo(obj.EnglishWord) != 0)
						return EnglishWord.CompareTo(obj.EnglishWord);
					else
					{
						if (TodaysWord.CompareTo (obj.TodaysWord) != 0)
							return TodaysWord.CompareTo (obj.TodaysWord);
						else {
							if (TodaysExample.CompareTo (obj.TodaysExample) != 0)
								return TodaysExample.CompareTo (obj.TodaysExample);
							else {
								return PartOfSpeach.CompareTo (obj.PartOfSpeach);
							}
						}
					}
				}
			}
		}

		#endregion
	}
}

