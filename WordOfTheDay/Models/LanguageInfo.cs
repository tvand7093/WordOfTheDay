using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
			Name = ParseLanguageName (this.Language);

			var split = Name.Split (' ');
			if (split.Length == 3) {
				// Format: <lang> For <Lang>
				ApiUrl = string.Format(BaseUrl, split[0] + "-" + split[2]);
				RSSUrl = string.Format (RSSBaseUrl, split[0], "-for-" + split[2]);
			} else {
				ApiUrl = string.Format(BaseUrl, Name);
				RSSUrl = string.Format (RSSBaseUrl, Name, string.Empty);
			}

			ApiUrl = ApiUrl.ToLower ();
			RSSUrl = RSSUrl.ToLower ();
		}

		string ParseLanguageName(Language lang)
		{
			var name = Enum.GetName (typeof(Language), lang);
			const string regexFormat = @"(\w+)For(\w+)Speakers";
			var matchResult = Regex.Match (name, regexFormat);

			if (matchResult.Success) {
				return string.Format ("{0} For {1}",
					matchResult.Groups [1].Value, matchResult.Groups [2].Value);
			} 

			return name;
		}

		public static IEnumerable<String> AllLanguages
		{
			get {
				return Enum.GetValues (typeof(Language))
					.Cast<Language> ()
					.Select (l => new LanguageInfo (l))
					.Select (l => l.Name);
			}
		}

		public static Language ParseLanguage(string language){
			var words = language.Split(' ');

			var sb = new StringBuilder ();
			foreach (var word in words) {
				sb.Append (CapitalizeFirstLetter (word));
			}

			language = sb.ToString();

			if (words.Length > 1) {
				language += "Speakers";
			}

			Language result;

			if (Enum.TryParse (language, out result)) {
				return result;
			}
			return Language.Italian;
		}

		static string CapitalizeFirstLetter(string word){
			var capsLetter = Char.ToUpper(word[0]);
			return word.Remove (0, 1).Insert (0, capsLetter.ToString ());
		}

		#region IComparable implementation

		public int CompareTo (LanguageInfo other)
		{
			return this.Language.CompareTo (other.Language);
		}

		#endregion
	}
}

