using System;

namespace WordOfTheDay.Models
{
	public class LanguageInfo : IComparable<LanguageInfo>
	{
		const string BaseUrl = "http://www.transparent.com/word-of-the-day/today/{0}.html";
		const string RSSBaseUrl = "http://feeds.feedblitz.com/{0}-word-of-the-day{1}&x=1";

		public Language Language {get;set;}
		public string ApiUrl { get; set; }
		public string RSSUrl { get; set; }
		public string Name { get; set; }

		public LanguageInfo (Language language)
		{
			this.Language = language;
			string urlName = ParseLanguageName (this.Language);
			ApiUrl = string.Format(BaseUrl, urlName);
			var split = urlName.Split ('-');
			if (split.Length == 2) {
				RSSUrl = string.Format (RSSBaseUrl, split[0], "-for-" + split[1]);
			} else {
				RSSUrl = string.Format (RSSBaseUrl, urlName, string.Empty);
			}
			var name = Enum.GetName (typeof(Language), this.Language);
			var result = string.Empty;

			for (int i = 0; i < name.Length; i++) {
				var letter = name [i];
				if (Char.IsUpper (letter) && i != 0) {
					//start of a new word...
					result += " ";
				}
				result += letter;
			}
			Name = result;
		}

		string ParseLanguageName(Language lang)
		{
			var name = Enum.GetName (typeof(Language), lang).ToLower();
			var forIndex = name.IndexOf ("for");

			if (forIndex != -1) {
				//language for speaker, so do this.
				var pre = name.Substring (0, forIndex);
				var speakersIndex = name.IndexOf ("speakers");			
				var postLength = speakersIndex - (forIndex + 3);
				var post = name.Substring (forIndex + 3, postLength);
				return pre + '-' + post;
			} else {
				//not for a speaker, so just return the value.
				return name;
			}


		}

		#region IComparable implementation

		public int CompareTo (LanguageInfo other)
		{
			return this.Language.CompareTo (other.Language);
		}

		#endregion
	}
}

