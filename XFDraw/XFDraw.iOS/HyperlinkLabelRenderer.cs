using System;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XFDraw;
using XFDraw.iOS;

// Add the ExportRenderer assembly attribute above the namespace declaration to 
// connect the HyperlinkLabelRenderer to the Xamarin.Forms HyperlinkLabel element

[assembly: ExportRenderer(typeof(HyperlinkLabel), typeof(HyperlinkLabelRenderer))]
namespace XFDraw.iOS
{
    public class HyperlinkLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Label> e)
        {
            base.OnElementChanged(e);

			// Change the native control's text color to blue by setting the TextColor property.
			Control.TextColor = UIColor.Blue;

			// The Label renderer creates an iOS UILabel which is reachable from the Control property. 
            // Enable touch interaction on the native control by setting UserInteractionEnabled to true.
			Control.UserInteractionEnabled = true;

			var gesture = new UITapGestureRecognizer();

			// Set the gesture recognizer's target using the AddTarget method and point it to either a new named method or a delegate.
			gesture.AddTarget(() =>
			{
				var url = new NSUrl("https://" + Control.Text);

				if (UIApplication.SharedApplication.CanOpenUrl(url))
					UIApplication.SharedApplication.OpenUrl(url);
			});

			// Add the gesture to the native control via the renderer's Control property.
			Control.AddGestureRecognizer(gesture);
        }
    }
}
