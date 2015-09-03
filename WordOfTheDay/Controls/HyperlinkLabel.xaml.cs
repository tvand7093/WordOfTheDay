using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;

namespace WordOfTheDay.Controls
{
	public partial class HyperlinkLabel : Label
	{
		public static readonly BindableProperty CommandProperty =
			BindableProperty.Create<HyperlinkLabel, ICommand>(
				(l) => l.Command,
				null,
				propertyChanged: (img, oldVal, newVal) => {
					(img as HyperlinkLabel).Command = newVal;
				});

		public static readonly BindableProperty CommandParameterProperty =
			BindableProperty.Create<HyperlinkLabel, object>(
				(l) => l.CommandParameter,
				null,
				propertyChanged: (img, oldVal, newVal) => {
					(img as HyperlinkLabel).CommandParameter = newVal;
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

		public HyperlinkLabel ()
		{
			tappedRecognizer = new TapGestureRecognizer ();
			GestureRecognizers.Add (tappedRecognizer);
			InitializeComponent ();
		}
	}
}

