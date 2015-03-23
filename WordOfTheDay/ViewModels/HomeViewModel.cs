using System;
using Xamarin.Forms;
using WordOfTheDay.Structures;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WordOfTheDay.ViewModels
{
	public class HomeViewModel : BaseViewModel<Word>, ISubscriber
	{
		private const int Gutter = 15;

		public async void Loading(Page sender) 
		{
			var page = sender as Page;

			//hide all labels and show loading
			Padding = new Thickness(0,0,0,0);
			page.IsBusy = IsBusy = true;
			ShowLabels = false;

			DataSource = await FeedService.WOTD ();
			OnPropertyChanged ("DataSource");
			//set padding 
			Padding = CalculatePadding (page);

			//disable loading stuff.
			page.IsBusy = IsBusy = false;

			//show resulting labels
			ShowLabels = true;
		}

		public ICommand RefreshCommand {get; private set;}

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

		public void Subscribe() {
			MessagingCenter.Subscribe<Page> (this,
				"Appearing", Loading);
		}

		public void Unsubscribe() {
			MessagingCenter.Unsubscribe<Page> (this, "Appearing");
		}

		public HomeViewModel ()
		{
			Subscribe ();
			RefreshCommand = new Command (() => Loading (App.Current.MainPage));
		}
	}
}

