using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;

namespace WordOfTheDay.Controls
{
	public partial class TapImage : Image
	{
		public static readonly BindableProperty CommandProperty =
			BindableProperty.Create<TapImage, ICommand>(
				(l) => l.Command,
				null,
				propertyChanged: (img, oldVal, newVal) => {
					(img as TapImage).Command = newVal;
				});

		public static readonly BindableProperty CommandParameterProperty =
			BindableProperty.Create<TapImage, object>(
				(l) => l.CommandParameter,
				null,
				propertyChanged: (img, oldVal, newVal) => {
					(img as TapImage).CommandParameter = newVal;
				});

		private readonly TapGestureRecognizer tappedRecognizer;

		public ICommand Command {
			get { return GetValue (CommandProperty) as ICommand; }
			set {
				SetValue (CommandProperty, value);
				tappedRecognizer.Command = value;
				OnPropertyChanged ("Command");
			}
		}

		public object CommandParameter {
			get { return GetValue (CommandParameterProperty); }
			set {
				SetValue (CommandParameterProperty, value);
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

