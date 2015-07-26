using System;

namespace WordOfTheDay.Models
{
	public class Word : IComparable<Word>
	{

		private const string APIUrlFormat 
		= "{0}?date={1}";

		public string EnglishWord {get;set;}
		public string TodaysWord {get;set;}
		public string PartOfSpeech {get;set;}
		public string EnglishExample {get;set;}
		public string TodaysExample {get;set;}
		public DateTime Date {get;set;}
		public LanguageInfo WordLanguage {get;set;}

		public Word ()
		{
			WordLanguage = new LanguageInfo(Language.Italian);
		}

		string url;
		public string Url 
		{
			get {
				if (String.IsNullOrEmpty (url))
					url = string.Format (APIUrlFormat, WordLanguage.ApiUrl,
						Date.ToString ("MM-dd-yyyy"));
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
				&& obj.WordLanguage.CompareTo(WordLanguage) == 0
				&& obj.PartOfSpeech.CompareTo(PartOfSpeech) == 0)
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
						if (WordLanguage.CompareTo (obj.WordLanguage) != 0)
							return WordLanguage.CompareTo (obj.WordLanguage);
						else {
							if (TodaysWord.CompareTo (obj.TodaysWord) != 0)
								return TodaysWord.CompareTo (obj.TodaysWord);
							else {
								if (TodaysExample.CompareTo (obj.TodaysExample) != 0)
									return TodaysExample.CompareTo (obj.TodaysExample);
								else {
									return PartOfSpeech.CompareTo (obj.PartOfSpeech);
								}
							}
						}
					}
				}
			}
		}

		#endregion
	}
}

