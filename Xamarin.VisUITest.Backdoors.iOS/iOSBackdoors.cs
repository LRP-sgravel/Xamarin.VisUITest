using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Xamarin.VisUITest.Backdoors.iOS
{
    public class iOSBackdoors
    {
        [Export ("getScreenCoordinates:")]
        public NSString GetScreenCoordinates (NSString removeStatusBar)
        {
            int y = (int)UIScreen.MainScreen.Bounds.Top;

            if(removeStatusBar.ToString() == true.ToString())
            {
                CGSize statusBarSize = UIApplication.SharedApplication.StatusBarFrame.Size;
                double barHeight = Math.Min(statusBarSize.Width, statusBarSize.Height);
                
                y = (int)(barHeight * UIScreen.MainScreen.Scale);
            }

            return new NSString(string.Format(@"{{ ""X"": {0}, ""Y"": {1}, ""Width"": {2}, ""Height"": {3} }}",
                                              (int)UIScreen.MainScreen.Bounds.Left,
                                              y,
                                              (int)UIScreen.MainScreen.Bounds.Width,
                                              (int)UIScreen.MainScreen.Bounds.Height - y));
        }
    }
}
