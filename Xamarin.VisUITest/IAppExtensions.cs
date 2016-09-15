using System.Drawing;
using System.IO;
using Xamarin.UITest;
using Imaging = AForge.Imaging;
using NUnit.Framework;

namespace Xamarin.VisUITest
{
    // ReSharper disable once InconsistentNaming
    public static class IAppExtensions
    {
        public static void DontSeeVisualChanges(this IApp app, string imageName)
        {
            FileInfo newImageInfo = app.SaveNamedScreenshot(imageName);
            string referencePath = VisUITest.ReferenceImagePath + imageName + ".png";

            if (File.Exists(referencePath))
            {
                Bitmap source = Imaging.Image.FromFile(newImageInfo.FullName);
                Bitmap reference = Imaging.Image.FromFile(referencePath);

                Assert.IsTrue(source.IsIdenticalTo(reference, VisUITest.MaximumDeviation), "The two images have a visual difference above maximum deviation");
            }
            else
            {
                newImageInfo.CopyTo(referencePath);
            }
        }

        public static void SeeVisualChanges(this IApp app, string imageName)
        {
            FileInfo newImageInfo = app.SaveNamedScreenshot(imageName);
            string referencePath = VisUITest.ReferenceImagePath + imageName + ".png";

            if (File.Exists(referencePath))
            {
                Bitmap source = Imaging.Image.FromFile(newImageInfo.FullName);
                Bitmap reference = Imaging.Image.FromFile(referencePath);

                Assert.IsFalse(source.IsIdenticalTo(reference, VisUITest.MaximumDeviation), "The two images have a visual difference below maximum deviation");
            }
            else
            {
                newImageInfo.CopyTo(referencePath);
            }
        }

        private static FileInfo SaveNamedScreenshot(this IApp app, string imageName)
        {
            FileInfo newImageInfo = app.Screenshot(imageName);
            string destination = VisUITest.CurrentImagePath + imageName + ".png";

            if (File.Exists(destination))
            {
                File.Delete(destination);
            }
            newImageInfo.MoveTo(destination);

            return new FileInfo(destination);
        }
    }
}