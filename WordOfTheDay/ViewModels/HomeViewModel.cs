using System;
using Xamarin.Forms;
using WordOfTheDay.Structures;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using WordOfTheDay.Models;
using WordOfTheDay.Pages;
using Xamarin;

namespace WordOfTheDay.ViewModels
{
	public class HomeViewModel : BaseViewModel<Word>, ISubscriber
	{
		const int Gutter = 15;
		const string WebPageURL = "http://www.transparent.com/word-of-the-day/today/italian.html?date={0}-{1}-{2}";
		readonly Thickness defaultThickness = new Thickness (0, 0, 0, 0);

		public async void Loading(Page sender) 
		{
			Padding = defaultThickness;

			//hide all labels and show loading
			sender.IsBusy = IsBusy = true;
			ShowLabels = false;

			try{
				DataSource = await FeedService.GetWordAsync ().ConfigureAwait(true);
				OnPropertyChanged ("DataSource");
			}
			catch(Exception e){
				Insights.Report (e);
			}
			finally {
				Padding = CalculatePadding (sender);

				//disable loading stuff.
				sender.IsBusy = IsBusy = false;

				//show resulting labels
				ShowLabels = true;
			}

		}

		public ICommand OpenCommand { get; private set; }

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
			Thickness thickness = defaultThickness;

			Device.OnPlatform (
				iOS: () => thickness = new Thickness (Gutter, view.Height / 6, Gutter, Gutter),
				Android: () => thickness = new Thickness (Gutter, view.Height / 10, Gutter, Gutter)
			);
			return thickness;
		}

		public void Subscribe() {
			MessagingCenter.Subscribe<Page> (this,
				"Appearing", Loading);
		}

		public void Unsubscribe() {
			MessagingCenter.Unsubscribe<Page> (this, "Appearing");
			IsBusy = false;
			ShowLabels = false;
		}
			
		public HomeViewModel ()
		{
			Subscribe ();

			OpenCommand = new Command(() => {
				var url = string.Format(WebPageURL, DataSource.Date.Month, 
					DataSource.Date.Day, DataSource.Date.Year);
				Device.OpenUri(new Uri(url)); 
			});
			DataSource = new Word ();
		}
	}
}

