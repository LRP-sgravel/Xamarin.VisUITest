using Java.Interop;

namespace Xamarin.VisUITest.Backdoors.Droid
{
    public partial class VisUITestActivity
    {
        [Export("GetUsableScreenCoordinates")]
        public string GetUsableScreenCoordinates()
        {
            return "{ X: 0, Y: 0, Width: 480, Height: 568 }";
        }
    }
}
