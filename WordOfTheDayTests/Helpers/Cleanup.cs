using System;
using PCLStorage;
using WordOfTheDay.Structures;
using WordOfTheDay.Helpers;

namespace WordOfTheDayTests.Helpers
{
	public static class Cleanup
	{
		public static void CleanCache()
		{
			try
			{
				//delete cached file if there.
				var folder = FileSystem.Current.LocalStorage;
				var files = folder.GetFilesAsync().Result;

				foreach (var file in files) {
					if(file.Name.Contains(".json")){
						file.DeleteAsync().Wait();
					}
				}
			}
			catch(Exception){ }
		}
	}
}

