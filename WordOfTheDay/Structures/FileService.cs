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
		const string CacheFileFormat = "CachedWord-{0}.json";

		public static async Task<Word> LoadWordAsync(Language lang){

			IFolder rootFolder = FileSystem.Current.LocalStorage;
			var fileName = string.Format (CacheFileFormat, lang.ToString());

			var exists = await rootFolder.CheckExistsAsync (fileName);

			if (exists == ExistenceCheckResult.FileExists) {
				//read contents
				var file = await rootFolder.GetFileAsync (fileName);
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

		public static async Task SaveWordAsync(Word toCache){
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			var file = await rootFolder.CreateFileAsync (
				string.Format(CacheFileFormat, toCache.WordLanguage.Language), 
				CreationCollisionOption.ReplaceExisting);
			//set date to simple date, not including time stuff
			toCache.Date = toCache.Date.Date;
			await file.WriteAllTextAsync (JsonConvert.SerializeObject (toCache));
		}
	}
}

