using System.Drawing;
using System.IO;
using Xamarin.UITest;
using Imaging = AForge.Imaging;
using NUnit.Framework;

namespace Xamarin.VisUITest
{
    public static class IAppExtensions
    {
        public static void SeeVisualChanges(this IApp app, string imageName)
        {
            FileInfo newImageInfo = app.Screenshot(imageName);
            string movePath = VisUITest.CurrentImagePath + newImageInfo.Name;
            string referencePath = VisUITest.ReferenceImagePath + newImageInfo.Name;

            if (File.Exists(movePath))
            {
                File.Delete(movePath);
            }
            newImageInfo.MoveTo(movePath);

            if (File.Exists(referencePath))
            {
                Bitmap source = Imaging.Image.FromFile(movePath);
                Bitmap reference = Imaging.Image.FromFile(referencePath);

                Assert.IsTrue(source.IsIdenticalTo(reference, VisUITest.MaximumDeviation), "The two images have a visual difference above maximum deviation");
            }
            else
            {
                newImageInfo.CopyTo(referencePath);
            }
        }
    }
}