using System.Drawing;
using System.IO;

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
            
        }

    }
}