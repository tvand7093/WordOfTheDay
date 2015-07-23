using System;

using Xamarin.Forms;
using System.Diagnostics;
using WordOfTheDay.Pages;
using WordOfTheDay.ViewModels;
using WordOfTheDay.Interfaces;

namespace WordOfTheDay
{
	public class App : Application, IApplication
	{
		public App ()
		{
			// The root page of your application
			MainPage = new HomePage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
			var ctx = MainPage.BindingContext as HomeViewModel;
			ctx.Unsubscribe ();
			MainPage.IsBusy = false;
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
			MainPage = new HomePage ();
		}
	}
}

