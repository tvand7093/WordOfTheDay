// Helpers/Settings.cs
using Refractored.Xam.Settings;
using Refractored.Xam.Settings.Abstractions;
using WordOfTheDay.Models;

namespace WordOfTheDay.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		static ISettings currentSettings = CrossSettings.Current;
		public static ISettings AppSettings {
			get {
				return currentSettings;
			}
			set {
				currentSettings = value ?? CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string SettingsKey = "WordOfTheDay_LastLanguage";
		private static readonly Language SettingsDefault = Language.Italian;

		#endregion

		public static Language LastLanguage {
			get {
				return AppSettings.GetValueOrDefault (SettingsKey, SettingsDefault);
			}
			set {
				AppSettings.AddOrUpdateValue (SettingsKey, (int)value);
			}
		}
	}
}