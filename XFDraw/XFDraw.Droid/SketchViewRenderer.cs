using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XFDraw;
using XFDraw.Droid;

// Add the ExportRenderer assembly attribute above the namespace declaration to connect the SketchViewRenderer to the SketchView.
[assembly: ExportRenderer(typeof(SketchView), typeof(SketchViewRenderer))]
namespace XFDraw.Droid
{
	// Derive from ViewRenderer. The first type argument is the element; use SketchView. 
    // The second type element is the native control; use PaintView.
	class SketchViewRenderer : ViewRenderer<SketchView, PaintView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SketchView> e)
        {
            base.OnElementChanged(e);

			// The code should only be performed once. Surround the code in an if statement that only executes if Control is null.
			if (Control == null)
			{
				// Create and instantiate a local PaintView instance named paintView. 
                // On Android you'll need to pass in the context: Forms.Context; on the other platforms the constructor takes no parameters.
				var paintView = new PaintView(Forms.Context);

                paintView.LineDrawn += PaintViewLineDrawn;

				// Set the color on paintView using the SetInkColor method. You can reach the bindable InkColor property on Element. 
                // On Android, there is an extension method to convert the Xamarin.Forms color to a native color: ToAndroid()
				paintView.SetInkColor(this.Element.InkColor.ToAndroid());

				// Assign paintView as the native control using the SetNativeControl method.
				SetNativeControl(paintView);

				// After the native control is assigned, call MessagingCenter.Subscribe to subscribe 
                // to the clear message and set OnMessageClear to the Action callback.
				MessagingCenter.Subscribe<SketchView>(this, "Clear", OnMessageClear);
			}
        }

		// To clean up after ourselves, override Dispose and Unsubscribe from these messages.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				MessagingCenter.Unsubscribe<SketchView>(this, "Clear");
			}

			base.Dispose(disposing);
		}

		// We want to ensure the native control is updated when properties are changed on the Xamarin.Forms element.
		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

			// The passed in PropertyChangedEventArgs has a PropertyName property which holds exactly what you'd expect: 
            // the name of the property on the element. Compare this to the name of our InkProperty. 
            // You can do this is a type-safe manner by checking the static SketchView.InkColorProperty.PropertyName
			if (e.PropertyName == SketchView.InkColorProperty.PropertyName)
			{
				// If the property name is correct, update the ink property on the native control using the InkColor property on the element.
				Control.SetInkColor(Element.InkColor.ToAndroid());
			}
        }

		// This method will be called when the clear message is received.
		void OnMessageClear(SketchView sender)
		{
			// We want to ensure we only respond to messages from the element associated to this instance of the renderer. 
            // Check if sender matches Element. If it does, call the Clear method on Control.
			if (sender == Element)
				Control.Clear();
		}

		private void PaintViewLineDrawn(object sender, System.EventArgs e)
		{
			// Cast Element to ISketchController and call the explicitly defined SendSketchUpdated method.

			var sketchCon = (ISketchController)Element;

			if (sketchCon == null)
				return;

			sketchCon.SendSketchUpdated();
		}
    }
}
