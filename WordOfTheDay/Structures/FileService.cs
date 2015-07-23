using System;
using WordOfTheDay.Models;
using PCLStorage;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace WordOfTheDay.Structures
{
	public static class FileService
	{
		public const string CacheFile = "CachedWord.json";
		public static async Task<Word> LoadWordAsync(){

			IFolder rootFolder = FileSystem.Current.LocalStorage;
			var exists = await rootFolder.CheckExistsAsync (CacheFile);
			if (exists == ExistenceCheckResult.FileExists) {
				//read contents
				var file = await rootFolder.GetFileAsync (CacheFile);
				var json = await file.ReadAllTextAsync ();
				var cachedWord = JsonConvert.DeserializeObject<Word> (json);
				var cachedDate = cachedWord.Date.Date;
				var currentDate = DateTime.Now.Date;

				if (cachedDate == currentDate) {
					//date same, so just return cached word.
					return cachedWord;
				}
			} 

			//file doesn't exist or needs updating, so return null.
			return null;
		}

		public static async void SaveWordAsync(Word toCache){
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			var file = await rootFolder.CreateFileAsync (CacheFile, 
				CreationCollisionOption.ReplaceExisting);
			//set date to simple date, not including time stuff
			toCache.Date = toCache.Date.Date;
			await file.WriteAllTextAsync (JsonConvert.SerializeObject (toCache));
		}
	}
}

