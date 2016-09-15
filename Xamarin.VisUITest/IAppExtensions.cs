using System;
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
            if (SetupDirectories())
            {
                FileInfo newImageInfo = app.SaveNamedScreenshot(imageName);
                string referencePath = VisUITest.ReferenceImagePath + imageName + ".png";

                if (File.Exists(referencePath))
                {
                    Bitmap source = Imaging.Image.FromFile(newImageInfo.FullName);
                    Bitmap reference = Imaging.Image.FromFile(referencePath);

                    Assert.IsTrue(source.IsIdenticalTo(reference, VisUITest.MaximumDeviation),
                                  "There is a deviation between screenshot and reference image above allowed deviation");
                }
                else
                {
                    newImageInfo.CopyTo(referencePath);
                    Assert.Inconclusive();
                }
            }
            else
            {
                Assert.Fail("Failed to setup VisUITest image folders");
            }
        }

        public static void SeeVisualChanges(this IApp app, string imageName)
        {
            if (SetupDirectories())
            {
                FileInfo newImageInfo = app.SaveNamedScreenshot(imageName);
                string referencePath = VisUITest.ReferenceImagePath + imageName + ".png";

                if (File.Exists(referencePath))
                {
                    Bitmap source = Imaging.Image.FromFile(newImageInfo.FullName);
                    Bitmap reference = Imaging.Image.FromFile(referencePath);

                    Assert.IsFalse(source.IsIdenticalTo(reference, VisUITest.MaximumDeviation),
                                   "There is a deviation between screenshot and reference image below allowed deviation");
                }
                else
                {
                    newImageInfo.CopyTo(referencePath);
                }
            }
            else
            {
                Assert.Fail("Failed to setup VisUITest image folders");
            }
        }

        private static bool SetupDirectories()
        {
            bool result = true;

            if (!Directory.Exists(VisUITest.CurrentImagePath))
            {
                try
                {
                    Directory.CreateDirectory(VisUITest.CurrentImagePath);
                }
                catch (Exception)
                {
                    result = false;
                }
            }

            if (!Directory.Exists(VisUITest.ReferenceImagePath))
            {
                try
                {
                    Directory.CreateDirectory(VisUITest.ReferenceImagePath);
                }
                catch (Exception)
                {
                    result = false;
                }
            }

            return true;
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