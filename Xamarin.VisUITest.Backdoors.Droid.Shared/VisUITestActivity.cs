using Android.Graphics;
using Android.Views;
using Java.Interop;
using Org.Json;

namespace Xamarin.VisUITest.Backdoors.Droid
{
    public partial class VisUITestActivity
    {
        [Export("GetUsableScreenCoordinates")]
        public string GetUsableScreenCoordinates(bool alwaysRemoveStatusBar)
        {
            Rect windowRect = new Rect();
            JSONObject rectObject = new JSONObject();
            bool forceRemovedStatusBar = false;

            Window.FindViewById<View>(Window.IdAndroidContent).GetLocalVisibleRect(windowRect);

            // Some devices/OS versions/Launchers/apps allow for a transparent status bar.  When the flag is set, we'll always remove it,
            //  even when the app is "full screen"
            if (alwaysRemoveStatusBar)
            {
                int resourceId = Resources.GetIdentifier("status_bar_height", "dimen", "android");

                if (resourceId > 0)
                {
                    windowRect.Top = Resources.GetDimensionPixelSize(resourceId);
                    forceRemovedStatusBar = true;
                }
            }

            rectObject.Put("X", windowRect.Left);
            rectObject.Put("Y", windowRect.Top);
            rectObject.Put("Width", windowRect.Width());
            rectObject.Put("Height", windowRect.Height());
            rectObject.Put("RemovedSideBar", forceRemovedStatusBar);

            return rectObject.ToString();
        }
    }
}
