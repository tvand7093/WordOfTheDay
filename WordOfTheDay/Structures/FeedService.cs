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
using PCLStorage;
using Newtonsoft.Json;
using System.Net;

namespace WordOfTheDay.Structures
{
	public static class FeedService
	{
		#if TEST
		public static string TestHTML { get; set; }
		#endif

		static Word ParseHtml(Language lang, string html){
			if (String.IsNullOrEmpty (html)) {
				#if RELEASE
				Insights.Report (new Exception ("Html from fetching new word was null or empty"),
					Insights.Severity.Error);
				#endif
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

			var todaysWord = titleParts [0].Trim () ?? string.Empty;
			var englishWord = titleParts [1].Trim () ?? string.Empty;

			var partOfSpeach = split [0].Trim () ?? string.Empty;
			var example = split [1].Trim () ?? string.Empty;
			var englishExample = split [2].Trim () ?? string.Empty;

			var word = new Word {
				TodaysWord = WebUtility.HtmlDecode (todaysWord),
				EnglishWord = WebUtility.HtmlDecode (englishWord),
				PartOfSpeech = WebUtility.HtmlDecode (partOfSpeach),
				TodaysExample = WebUtility.HtmlDecode (example),
				EnglishExample = WebUtility.HtmlDecode (englishExample),
				Date = DateTime.Parse(pubDate).ToUniversalTime().Date,
				WordLanguage = new LanguageInfo(lang)
			};
			return word;
		}

		public static async Task<Word> GetWordAsync(Language lang){
			//see if we already have today's word.
			var cached = await FileService.LoadWordAsync (lang);

			if (cached != null)
				return cached;

			//no cached word, or we need to fetch the next day's word.
			string html = string.Empty;

			#if RELEASE
			// Insights for release only.
			var handle = Insights.TrackTime("TimeToFetchWord");
			handle.Start();
			#endif

			using(var httpClient = new HttpClient())
			{
				#if TEST
				html = TestHTML;
				#else
				var languageInfo = new LanguageInfo(lang);
				html = await httpClient.GetStringAsync(languageInfo.RSSUrl);
				#endif
			}

			#if RELEASE
			// Insights for release only.
			handle.Stop ();
			handle = Insights.TrackTime ("TimeToParseHTMLAndSave");
			handle.Start ();
			#endif

			//parse out the word info
			var word = await Task.Run(() => ParseHtml(lang, html));

			if (word != null) {
				//save new word to cache
				await FileService.SaveWordAsync(word);
			}

			#if RELEASE
			// Insights for release only.
			handle.Stop ();
			#endif

			return word;
		}
	}
}

