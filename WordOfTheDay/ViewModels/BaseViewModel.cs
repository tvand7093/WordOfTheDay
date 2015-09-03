using System;
using System.ComponentModel;
using Xamarin.Forms;
using WordOfTheDay.Interfaces;

namespace WordOfTheDay.ViewModels
{

	public abstract class BaseViewModel : INotifyPropertyChanged 
	{
		IApplication app;

		protected BaseViewModel (IApplication app)
		{
			this.app = app;
		}

		bool isBusy;
		public bool IsBusy {
			get { 
				return isBusy;
			}
			set { 
				if (app != null && app.MainPage != null)
					app.MainPage.IsBusy = value;
				
				isBusy = value;
				OnPropertyChanged ("IsBusy");
			}
		}

		public INavigation Navigation {
			get { 
				var mainPage = app.MainPage;
				if (mainPage != null) {
					if (mainPage is NavigationPage) {
						return (INavigation)mainPage;
					}
					if (mainPage is TabbedPage) {
						return ((TabbedPage)mainPage).Children [0].Navigation;
					}
					return app.MainPage.Navigation;
				}
				return null;
			}
		}
			
		#region Property Changed Implementation

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
					new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion
	}

	public abstract class BaseViewModel<T> : BaseViewModel where T : class
	{
		protected BaseViewModel (IApplication app) 
			:base(app)
		{
			
		}
		private T dataSource;
		public T DataSource {
			get { return dataSource; }
			set {
				dataSource = value;
				OnPropertyChanged ("DataSource");
			}
		}
	}
}

