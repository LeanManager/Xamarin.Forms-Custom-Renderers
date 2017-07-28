using System;
using Android.Text.Util;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XFDraw;
using XFDraw.Droid;

// Add the ExportRenderer assembly attribute above the namespace declaration to 
// connect the HyperlinkLabelRenderer to the Xamarin.Forms HyperlinkLabel element

[assembly: ExportRenderer(typeof(HyperlinkLabel), typeof(HyperlinkLabelRenderer))]
namespace XFDraw.Droid
{
    public class HyperlinkLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Label> e)
        {
            base.OnElementChanged(e);

			// The Label renderer creates an Android TextView which is reachable 
            // from the Control property. Call Linkify.AddLinks to update the native 
            // TextView to a clickable hyperlink control; set the match options to All.

			Linkify.AddLinks(Control, MatchOptions.All);
        }
    }
}
