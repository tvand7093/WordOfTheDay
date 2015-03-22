using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace WordOfTheDay.ViewModels
{

	public abstract class BaseViewModel : INotifyPropertyChanged 
	{
		private bool isBusy;
		public bool IsBusy {
			get { 
				return isBusy;
			}
			set { 
				if(Application.Current.MainPage != null)
					Application.Current.MainPage.IsBusy = value;
				
				isBusy = value;
				OnPropertyChanged ("IsBusy");
			}
		}

		public INavigation Navigation {
			get { 
				var mainPage = Application.Current.MainPage;
				if (mainPage is NavigationPage) {
					return (INavigation)mainPage;
				}
				if (mainPage is TabbedPage) {
					return ((TabbedPage)mainPage).Children [0].Navigation;
				}
				return Application.Current.MainPage.Navigation;
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

