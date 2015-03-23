using System;
using Xamarin.Forms;
using WordOfTheDay.Controls;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using WordOfTheDay.iOS.Interop;
using Foundation;


[assembly: ExportRenderer(typeof(HeaderLabel), typeof(HeaderLabelRenderer))]
namespace WordOfTheDay.iOS.Interop
{
	public class HeaderLabelRenderer : LabelRenderer
	{
		private UILabel ctrl;

		protected override void OnElementChanged (ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged (e);

			if (ctrl != null)
				return;

			if (Control != null) {
				ctrl = Control;
				var underline = new NSAttributedString (ctrl.Text,
					                font: ctrl.Font,  strokeColor: ctrl.TextColor,
									underlineStyle: NSUnderlineStyle.Single
				);
				ctrl.AttributedText = underline;
			}
		}

		public HeaderLabelRenderer ()
		{
		}
	}
}

