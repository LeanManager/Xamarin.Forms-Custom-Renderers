using System;
using Xamarin.Forms;

namespace XFDraw
{
    public class SketchView : View, ISketchController
    {
		// Create a BindableProperty named InkColorProperty. Set the propertyName as "InkColor", 
        // the return type as Color and the declaring type as the new element - SketchView.
		public static readonly BindableProperty InkColorProperty = BindableProperty.Create("InkColor", 
                                                                                            typeof(Color), 
                                                                                            typeof(SketchView), 
                                                                                            Color.Blue);
		// Create a Color property named InkColor for the bindable property. 
        // Call the GetValue and SetValue methods, passing in InkColorProperty, in the getter/setter.
		public Color InkColor
		{
			get 
            { 
                return (Color)GetValue(InkColorProperty); 
            }
			set 
            { 
                SetValue(InkColorProperty, value); 
            }
		}

		// We want to clear our drawing surface when the user taps the clear/delete button. 
        // However, we don't want to hold a reference from the element to the renderer, 
        // so we'll send a notification using the built-in Messaging Center.
		public void Clear()
		{
			MessagingCenter.Send(this, "Clear");
		}


        public event EventHandler SketchUpdated;

		void ISketchController.SendSketchUpdated()
		{
			if (SketchUpdated != null)
				SketchUpdated(this, EventArgs.Empty);
		}
    }
}
