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
            string path = VisUITest.ReferenceImagePath + imageName + ".png";

            if(File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}