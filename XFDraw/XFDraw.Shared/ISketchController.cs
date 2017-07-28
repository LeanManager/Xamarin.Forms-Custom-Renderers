using System;
namespace XFDraw
{
	// We are going to send a notifcation from the plaform-specific renderers 
    // to the Xamarin.Forms sketch view element. To do this we're going to add 
    // a public method on the element. However we only want to raise it from the 
    // renderer. To reduce discoverability, we're going to define the method 
    // within an interface and explicitly implement it.

	public interface ISketchController
    {
        void SendSketchUpdated();
    }
}
