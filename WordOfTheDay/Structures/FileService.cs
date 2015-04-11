using System;
using WordOfTheDay.Models;
using PCLStorage;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WordOfTheDay.Structures
{
	internal static class FileService
	{
		const string CacheFile = "CachedWord.json";
		internal static async Task<Word> LoadWordAsync(){

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

		internal static async void SaveWordAsync(Word toCache){
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			var file = await rootFolder.CreateFileAsync (CacheFile, CreationCollisionOption.ReplaceExisting);
			//set date to simple date, not including UTC
			toCache.Date = toCache.Date.Date;
			await file.WriteAllTextAsync (JsonConvert.SerializeObject (toCache));
		}
	}
}

