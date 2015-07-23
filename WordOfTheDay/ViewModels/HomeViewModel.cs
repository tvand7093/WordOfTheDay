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
using WordOfTheDay.Interfaces;

namespace WordOfTheDay.ViewModels
{
	public class HomeViewModel : BaseViewModel<Word>, ISubscriber
	{
		const int Gutter = 15;

		public async Task Loading(Page sender) 
		{
			Padding = default(Thickness);

			//hide all labels and show loading
			sender.IsBusy = IsBusy = true;
			ShowLabels = false;

			try{
				DataSource = await FeedService.GetWordAsync ().ConfigureAwait(true);
			}
			// Analysis disable once EmptyGeneralCatchClause
			catch(Exception e){
				#if RELEASE
				Insights.Report (e);
				#endif 
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

		bool showLabels;
		public bool ShowLabels {
			get { return showLabels; }
			set {
				showLabels = value;
				OnPropertyChanged ("ShowLabels");
			}
		}

		Thickness padding;
		public Thickness Padding {
			get { return padding; }
			set {
				padding = value;
				OnPropertyChanged ("Padding");
			}
		}

		Thickness CalculatePadding(Page view){
			Thickness thickness = default(Thickness);

			Device.OnPlatform (
				iOS: () => thickness = new Thickness (Gutter, view.Height / 6, Gutter, Gutter),
				Android: () => thickness = new Thickness (Gutter, view.Height / 10, Gutter, Gutter)
			);
			return thickness;
		}

		public void Subscribe() {
			MessagingCenter.Subscribe<Page> (this,
				"Appearing", async (p) => await Loading(p));
		}

		public void Unsubscribe() {
			MessagingCenter.Unsubscribe<Page> (this, "Appearing");
			IsBusy = false;
			ShowLabels = false;
		}
			
		public HomeViewModel (IApplication app)
			: base (app)
		{
			Subscribe ();

			OpenCommand = new Command(() => {
				Device.OpenUri(new Uri(DataSource.Url)); 
			});
			DataSource = new Word ();
		}
	}
}

