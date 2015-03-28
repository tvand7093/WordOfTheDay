using System;
using System.Net.Http;
using System.Xml;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using WordOfTheDay.Models;
using Xamarin.Forms;
using Xamarin;

namespace WordOfTheDay.Structures
{
	public static class FeedService
	{
		const string RSSUrl = "http://feeds.feedblitz.com/italian-word-of-the-day&x=1";
		const string CacheKey = "CachedWord";

		static Word ParseHtml(string html){
			if (String.IsNullOrEmpty (html)) {
				Insights.Report (new Exception ("Html from fetching new word was null or empty"),
					ReportSeverity.Error);
				return null;
			}
				
			var xdoc = XDocument.Parse (html);

			var pubDate = xdoc.Root.Element ("channel").Element ("pubDate").Value;
			var item = xdoc.Descendants ("item").First ();
			var titleParts = item.Element ("title").Value.Split (':');
			var desc = item.Element ("description").Value;

			//get rid of HTML tags
			desc = Regex.Replace (desc, "<[^>]*>", string.Empty);

			//get rid of multiple blank lines
			desc = Regex.Replace (desc, @"^\s*$\n", string.Empty, RegexOptions.Multiline);

			StringBuilder sb = new StringBuilder ();

			foreach (var value in desc.Split ('\n')) {
				var trimmed = value.Trim ();
				var indexOfColon = trimmed.IndexOf (':');

				if (indexOfColon > -1) {
					trimmed = trimmed.Remove (0, indexOfColon + 1);
				}

				if (trimmed.Length == 0) {
					continue;
				} else {
					sb.AppendLine (trimmed);
				}
			}
			var split = sb.ToString ().Split ('\n');
			var word = new Word {
				TodaysWord = titleParts [0].Trim () ?? string.Empty,
				EnglishWord = titleParts [1].Trim () ?? string.Empty,
				PartOfSpeach = split [0] ?? string.Empty,
				TodaysExample = split [1] ?? string.Empty,
				EnglishExample = split [2] ?? string.Empty,
				Date = DateTime.Parse(pubDate)
			};
			return word;
		}

		static Task<Word> GetCachedWordAsync(){
			return Task.Run<Word> (() => {
				if (Application.Current.Properties.ContainsKey (CacheKey)) {
					var cachedWord = Application.Current.Properties [CacheKey] as Word;
					var cachedDate = cachedWord.Date.Date;
					var currentDate = DateTime.Now.Date;
					if (cachedDate == currentDate) {
						//date same, so just return cached word.
						return cachedWord;
					}
				}
				return null;
			});
		}

		public static async Task<Word> GetWordAsync(){
			//see if we already have today's word.
			var cached = await GetCachedWordAsync ();

			if (cached != null)
				return cached;

			//no cached word, or we need to fetch the next day's word.
			string html = string.Empty;

			var handle = Insights.TrackTime("TimeToFetchWord");
			handle.Start();

			using(var httpClient = new HttpClient())
			{
				html = await httpClient.GetStringAsync(RSSUrl);
			}

			handle.Stop ();

			handle = Insights.TrackTime ("TimeToParseHTMLAndSave");

			handle.Start ();
			//parse out the word info
			var word = await Task.Run(() => ParseHtml(html));

			if (word != null) {
				//remove old word from cache
				Application.Current.Properties.Remove(CacheKey);
				//save new word into cache
				Application.Current.Properties.Add (CacheKey, word);
				await Application.Current.SavePropertiesAsync ();
			}
			handle.Stop ();
			return word;
		}
	}
}

