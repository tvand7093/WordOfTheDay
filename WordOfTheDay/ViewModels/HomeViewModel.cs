using System;
using Xamarin.Forms;
using WordOfTheDay.Structures;
using System.Linq;
using System.Diagnostics;

namespace WordOfTheDay.ViewModels
{
	public class HomeViewModel : BaseViewModel<Word>
	{
		private const int Gutter = 15;

		public async void Loaded(object sender, EventArgs args) 
		{
			var page = sender as Page;

			//hide all labels and show loading
			page.IsBusy = IsBusy = true;
			ShowLabels = false;

			//fetch wotd
			DataSource = await FeedService.WOTD ();

			//set padding 
			Padding = CalculatePadding (page);

			//disable loading stuff.
			page.IsBusy = IsBusy = false;

			//show resulting labels
			ShowLabels = true;
		}

		private bool showLabels;
		public bool ShowLabels {
			get { return showLabels; }
			set {
				showLabels = value;
				OnPropertyChanged ("ShowLabels");
			}
		}

		private Thickness padding;
		public Thickness Padding {
			get { return padding; }
			set {
				padding = value;
				OnPropertyChanged ("Padding");
			}
		}

		private Thickness CalculatePadding(Page view){
			return new Thickness (Gutter, view.Height / 6, Gutter, Gutter);
		}

		public HomeViewModel ()
		{
		}
	}
}

