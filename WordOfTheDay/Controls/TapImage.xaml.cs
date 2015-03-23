using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;

namespace WordOfTheDay.Controls
{
	public partial class TapImage : Image
	{
		public readonly BindableProperty TappedProperty =
			BindableProperty.Create<TapImage, ICommand>(
				(l) => l.Command,
				null,
				propertyChanged: (label, oldVal, newVal) => {
					(label as TapImage).Command = newVal;
				});

		public readonly BindableProperty TappedCommandProperty =
			BindableProperty.Create<TapImage, object>(
				(l) => l.Command,
				null,
				propertyChanged: (label, oldVal, newVal) => {
					(label as TapImage).CommandParameter = newVal;
				});


		private readonly TapGestureRecognizer tappedRecognizer;

		public ICommand Command {
			get { return GetValue (TappedProperty) as ICommand; }
			set {
				SetValue (TappedProperty, value);
				tappedRecognizer.Command = value;
				OnPropertyChanged ("Command");
			}
		}

		public object CommandParameter {
			get { return GetValue (TappedCommandProperty); }
			set {
				SetValue (TappedCommandProperty, value);
				tappedRecognizer.CommandParameter = value;
				OnPropertyChanged ("Command");
			}
		}
	
		public TapImage ()
		{
			tappedRecognizer = new TapGestureRecognizer ();
			GestureRecognizers.Add (tappedRecognizer);
			InitializeComponent ();
		}
	}
}

