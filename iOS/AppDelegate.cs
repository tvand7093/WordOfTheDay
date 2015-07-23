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

			#if RELEASE
			ConfigureInsights ();
			#endif
			LoadApplication (new App ());
			return base.FinishedLaunching (app, options);
		}

		#if RELEASE

		//Insights only for release version

		void ConfigureInsights(){
			Insights.Initialize(Configuration.InsightsApiKey);


			Insights.HasPendingCrashReport += (sender, isStartupCrash) =>
			{
				if (isStartupCrash) {
					Insights.PurgePendingCrashReports().Wait();
				}
			};
		}
		#endif

	}
}

