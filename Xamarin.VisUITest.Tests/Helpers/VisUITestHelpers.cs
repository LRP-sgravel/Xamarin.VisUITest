using System.Drawing;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.iOS;
using Xamarin.UITest.Queries;

namespace Xamarin.VisUITest.Tests.Helpers
{
    class VisUITestHelpers
    {
        public static void RemoveAllReferenceImage(string imageName)
        {
            if (Directory.Exists(VisUITest.ReferenceImagePath))
            {
                Directory.Delete(VisUITest.ReferenceImagePath);
            }
        }

        public static void RemoveReferenceImage(string imageName)
        {
            string imagePath = VisUITest.ReferenceImagePath + imageName + ".png";

            if(File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }

        public static Bitmap LoadReferenceImage(string imageName)
        {
            string imagePath = VisUITest.ReferenceImagePath + imageName + ".png";
            Bitmap result = null;

            if(File.Exists(imagePath))
            {
                result = AForge.Imaging.Image.FromFile(imagePath);
            }

            return result;
        }

        public static AppResult GetTopMostView(IApp app)
        {
            Platform platform = app is iOSApp ? Platform.iOS : Platform.Android;
            AppResult result = null;
            
            if (platform == Platform.Android)
            {
                result = app.Query(e => e.Id("content")).FirstOrDefault();
            }
            else
            {
                result = app.Query(e => e.Class("Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer")).FirstOrDefault();

                if (VisUITest.AlwaysRemoveStatusBar)
                {
                    result.Rect.Y = 24;
                }
            }

            return result;
        }
    }
}