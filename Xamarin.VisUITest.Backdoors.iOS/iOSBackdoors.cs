using Foundation;
using UIKit;

namespace Xamarin.VisUITest.Backdoors.iOS
{
    public class iOSBackdoors
    {
        [Export ("getScreenCoordinates:")]
        public string GetScreenCoordinates ()
        {
            return string.Format (@"{{ ""X"": {0}, ""Y"": {1}, ""Width"": {2}, ""Height"": {3} }}",
                                 UIScreen.MainScreen.NativeBounds.Left,
                                 UIScreen.MainScreen.NativeBounds.Top,
                                 UIScreen.MainScreen.NativeBounds.Width,
                                 UIScreen.MainScreen.NativeBounds.Height);
        }
    }
}
