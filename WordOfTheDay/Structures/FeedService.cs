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

namespace WordOfTheDay.Structures
{
	public class Word 
	{
		public string EnglishWord {get;set;}
		public string ItalianWord {get;set;}
		public string PartOfSpeach {get;set;}
		public string EnglishExample {get;set;}
		public string NativeExample {get;set;}
		public void Copy(Word toCopy){
			EnglishWord = toCopy.EnglishWord;
			ItalianWord = toCopy.ItalianWord;
			PartOfSpeach = toCopy.PartOfSpeach;
			EnglishExample = toCopy.EnglishExample;
			NativeExample = toCopy.NativeExample;
		}
	}

	public static class FeedService
	{

		public static async Task<Word> WOTD(){

			string responseString = string.Empty;
			using(var httpClient = new HttpClient())
			{
				var feed = "http://feeds.feedblitz.com/italian-word-of-the-day&x=1";
				responseString = await httpClient.GetStringAsync(feed);
			}
				

			var xdoc = XDocument.Parse (responseString);

			var item = xdoc.Descendants ("item").First ();
			var titleParts = item.Element ("title").Value.Split (':');
			var desc = item.Element("description").Value;

			//get rid of HTML tags
			desc = Regex.Replace(desc, "<[^>]*>", string.Empty);

			//get rid of multiple blank lines
			desc = Regex.Replace(desc, @"^\s*$\n", string.Empty, RegexOptions.Multiline);

			StringBuilder sb = new StringBuilder ();

			foreach (var value in desc.Split ('\n')) {
				var trimmed = value.Trim ();
				var indexOfColon = trimmed.IndexOf (':');

				if (indexOfColon > -1) {
					trimmed = trimmed.Remove (0, indexOfColon+1);
				}

				if (trimmed.Length == 0) {
					continue;
				} else {
					sb.AppendLine (trimmed);
				}
			}

			var split = sb.ToString ().Split('\n');

			return new Word {
				ItalianWord = titleParts[0].Trim() ?? string.Empty,
				EnglishWord = titleParts[1].Trim() ?? string.Empty,
				PartOfSpeach = split[0] ?? string.Empty,
				NativeExample = split[1] ?? string.Empty,
				EnglishExample = split[2] ?? string.Empty,
			};
		}
	}
}

