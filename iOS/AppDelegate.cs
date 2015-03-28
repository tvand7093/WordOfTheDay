using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin;
using WordOfTheDay.Structures;
using System.Diagnostics;

namespace WordOfTheDay.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();
			app.SetStatusBarStyle (UIStatusBarStyle.LightContent, false);
			ConfigureInsights ();

			LoadApplication (new App ());
			return base.FinishedLaunching (app, options);
		}

		void ConfigureInsights(){
			#if DEBUG
			Insights.Initialize(Insights.DebugModeKey);
			#else
			Insights.Initialize(Configuration.InsightsApiKey);
			#endif

			Insights.HasPendingCrashReport += (sender, isStartupCrash) =>
			{
				if (isStartupCrash) {
					Insights.PurgePendingCrashReports().Wait();
				}
			};
		}
	}
}

