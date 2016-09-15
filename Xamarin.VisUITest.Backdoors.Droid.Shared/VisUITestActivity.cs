using Android.Graphics;
using Java.Interop;
using Org.Json;

namespace Xamarin.VisUITest.Backdoors.Droid
{
    public partial class VisUITestActivity
    {
        [Export("GetUsableScreenCoordinates")]
        public string GetUsableScreenCoordinates()
        {
            Rect windowRect = new Rect();
            JSONObject rectObject = new JSONObject();

            Window.DecorView.GetWindowVisibleDisplayFrame(windowRect);

            rectObject.Put("X", windowRect.Left);
            rectObject.Put("Y", windowRect.Top);
            rectObject.Put("Width", windowRect.Width());
            rectObject.Put("Height", windowRect.Height());

            return rectObject.ToString();
        }
    }
}
