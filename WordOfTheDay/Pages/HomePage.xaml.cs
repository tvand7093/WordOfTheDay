using System;
using System.Collections.Generic;

using Xamarin.Forms;
using WordOfTheDay.ViewModels;
using WordOfTheDay.Interfaces;

namespace WordOfTheDay.Pages
{
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			BindingContext = new HomeViewModel ((IApplication)Application.Current);

			this.Appearing += (object sender, EventArgs e) => {
				MessagingCenter.Send<Page>(sender as Page, "Appearing");
			};

			InitializeComponent ();

		}
	}
}

