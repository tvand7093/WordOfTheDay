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
using System.Collections.Generic;
using WordOfTheDay.Helpers;

namespace WordOfTheDay.ViewModels
{
	public class HomeViewModel : BaseViewModel<Word>, ISubscriber
	{
		const int Gutter = 15;
		IApplication app;
		string lang;

		#if TEST

		public Language LastLanguage { get; set; }

		#endif

		public string SelectedLanguage {
			get {
				return lang;
			}
			set {
				OnPropertyChanged ("SelectedLanguage");
				lang = value;


				#if TEST

				LastLanguage = LanguageInfo.ParseLanguage(lang); 

				#else

				if (Settings.AppSettings != null) {
					Settings.LastLanguage = LanguageInfo.ParseLanguage(lang);
				}

				#endif

                MessagingCenter.Send<String>(lang, "LanguageChanged");
			}
		}

		public IEnumerable<String> Languages {
			get {
				return LanguageInfo.AllLanguages;
			}
		}


		public async Task Loading(Page sender) 
		{
			Padding = default(Thickness);

			//hide all labels and show loading
			sender.IsBusy = IsBusy = true;
			ShowLabels = false;

			try{
				DataSource = await FeedService.GetWordAsync (
					(Language)Enum.Parse(typeof(Language), SelectedLanguage))
					.ConfigureAwait(true);
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

            MessagingCenter.Subscribe<String>(this,
                "LanguageChanged", async (p) => {
                    if (app != null) await Loading(app.MainPage);
                });
		}

		public void Unsubscribe() {
			MessagingCenter.Unsubscribe<Page> (this, "Appearing");
            MessagingCenter.Unsubscribe<String>(this, "LanguageChanged");
			IsBusy = false;
			ShowLabels = false;
		}
			
		public HomeViewModel (IApplication app)
			: base (app)
		{
			this.app = app;
			var defaultLanguage = new LanguageInfo (Language.Italian);

			#if TEST
			this.lang = "Italian";
			defaultLanguage = new LanguageInfo(Language.Italian);
			#else

			if (Settings.AppSettings != null) {
				var lastLanguage = Settings.LastLanguage;
				var li = new LanguageInfo (lastLanguage);
				this.lang = li.Name;
				defaultLanguage = li;
			}
			#endif
			Subscribe ();

			OpenCommand = new Command(() => Device.OpenUri (new Uri (DataSource.Url)));

			DataSource = new Word () {
				WordLanguage = defaultLanguage
			};
		}
	}
}

