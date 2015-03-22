using System;
using Xamarin.Forms;
using WordOfTheDay.Structures;
using System.Linq;

namespace WordOfTheDay.ViewModels
{
	public class HomeViewModel : BaseViewModel<Word>
	{
		public async void Loaded(object sender, EventArgs args) 
		{
			(sender as Page).IsBusy = true;
			DataSource = await FeedService.WOTD ();
			(sender as Page).IsBusy = false;
		}

		public HomeViewModel ()
		{
		}
	}
}

