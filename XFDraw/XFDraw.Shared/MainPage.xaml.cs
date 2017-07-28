using System;
using Xamarin.Forms;

// Using an Android preprocessor directive, add Android-specific using statements 
// to bring in the namespaces required to add an Android floating action button:

#if __ANDROID__
// Add the Android specific using statements for Android widgets
// Add the Xamarin.Forms Android platform using statement to gain access to the native embedding using statements.
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using Android.Support.Design.Widget;
#endif

namespace XFDraw
{
    public partial class MainPage : ContentPage
    {
		// Create a boolean property named IsCanvasDirty with a backing field 
        // to track the state of the canvas. Define the command and call ChangeCanExcute 
        // on the command when the IsCanvasDirty property is updated.
		bool IsCanvasDirty
		{
			get { return isCanvasDirty; }
			set
			{
				isCanvasDirty = value;

				if (clearCommand != null)
					clearCommand.ChangeCanExecute();
			}
		}
		bool isCanvasDirty;

        Command clearCommand;

        public MainPage()
        {
            InitializeComponent();

			sketchView.SketchUpdated += OnSketchUpdated;

			clearCommand = new Command(OnClearClicked, () => { return IsCanvasDirty; });

			var trash = new ToolbarItem()
			{
				Text = "Clear",
				Icon = "trash.png",
				Command = clearCommand
			};
            
            ToolbarItems.Add(trash);

            #if __ANDROID__

			var actionButton = new FloatingActionButton(Forms.Context);

			actionButton.SetImageResource(XFDraw.Droid.Resource.Drawable.pencil);
			actionButton.Click += (s, e) =>
			{
				OnColorClicked();

                actionButton.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(sketchView.InkColor.ToAndroid());
			};

			// Create an Android Frame Layout to hold the button
			var actionButtonFrame = new FrameLayout(Forms.Context);

			// SetClipToPadding should be set to false to ensure the drop-shadow is visible
			actionButtonFrame.SetClipToPadding(false);

			// Call SetPadding with large enough values to contain the button's drop-shadow
			actionButtonFrame.SetPadding(0, 0, 50, 50);

			actionButtonFrame.AddView(actionButton);


			var actionButtonFrameView = actionButtonFrame.ToView();

			actionButtonFrameView.HorizontalOptions = LayoutOptions.End;
			actionButtonFrameView.VerticalOptions = LayoutOptions.End;

			// With our Android FloatingActionButton ready and wrapped in a FrameLayout 
            // large enough to display it entirely, we put it in our main Xamarin.Forms Grid layout.
			mainLayout.Children.Add(actionButtonFrameView);

            #else

            ToolbarItems.Add(new ToolbarItem("New Color", "pencil.png", OnColorClicked));

            #endif
		}

		void OnSketchUpdated(object sender, EventArgs e)
		{
			IsCanvasDirty = true;
		}

		void OnClearClicked()
		{
			sketchView.Clear();
			IsCanvasDirty = false;
		}

        void OnColorClicked ()
        {
			// Set a new random color every time the "Color changed" button is pressed.
			sketchView.InkColor = GetRandomColor();
        }

        Random rand = new Random();

        Color GetRandomColor ()
        {
            return new Color(rand.NextDouble(), rand.NextDouble(), rand.NextDouble());
        }
    }
}