using System;
using Xamarin.Forms;
using WordOfTheDay.Controls;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Android.Widget;
using Android.Graphics;
using WordOfTheDay.Droid.Interop;

[assembly: ExportRenderer(typeof(HeaderLabel), typeof(HeaderLabelRenderer))]
namespace WordOfTheDay.Droid.Interop
{
	public class HeaderLabelRenderer : LabelRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Label> e) {
			base.OnElementChanged (e);
			Control.Paint.UnderlineText = true;
		}
	}
}

