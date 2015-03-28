using System;
using System.Reflection;
using System.IO;
using System.Xml.Linq;

namespace WordOfTheDay.Structures
{
	public static class Configuration
	{
		public static string InsightsApiKey { get; private set; }

		static Configuration() {
			var assembly = typeof(Configuration).GetTypeInfo().Assembly;
			using (var stream = assembly.GetManifestResourceStream ("WordOfTheDay.settings.config")) {
				var doc = XDocument.Load (stream);
				InsightsApiKey = doc.Root
					.Element ("Insights")
						.Attribute ("apiKey")
							.Value;
			}
		}
	}
}

