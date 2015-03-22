using System;
using System.Collections.Generic;

using Xamarin.Forms;
using WordOfTheDay.ViewModels;

namespace WordOfTheDay.Pages
{
	public partial class HomePage : ContentPage
	{
		private HomeViewModel ctx;
		public HomePage ()
		{
			ctx = new HomeViewModel ();
			BindingContext = ctx;

			this.Appearing += ctx.Loaded;

			this.Disappearing += (object sender, EventArgs e) => {
				this.Appearing -= ctx.Loaded;
			};

			InitializeComponent ();

		}
	}
}

