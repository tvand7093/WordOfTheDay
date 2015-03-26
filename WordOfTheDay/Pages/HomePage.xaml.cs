using System;
using System.Collections.Generic;

using Xamarin.Forms;
using WordOfTheDay.ViewModels;

namespace WordOfTheDay.Pages
{
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			BindingContext = new HomeViewModel ();;

			this.Appearing += (object sender, EventArgs e) => {
				MessagingCenter.Send<Page>(sender as Page, "Appearing");
			};

			InitializeComponent ();

		}
	}
}

