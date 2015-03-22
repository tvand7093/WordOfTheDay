using System;

using Xamarin.Forms;

namespace WordOfTheDay.Pages
{
	public class LoadingPage : ContentPage
	{
		public LoadingPage ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}


