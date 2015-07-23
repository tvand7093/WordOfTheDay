using System;
using PCLStorage;
using WordOfTheDay.Structures;

namespace WordOfTheDayTests.Helpers
{
	public static class Cleanup
	{
		public static void CleanCache()
		{
			try
			{
				//delete cached file if there.
				var file = FileSystem.Current
					.LocalStorage.GetFileAsync (FileService.CacheFile).Result;
				file.DeleteAsync ();
			}
			catch(Exception){ }
		}
	}
}

