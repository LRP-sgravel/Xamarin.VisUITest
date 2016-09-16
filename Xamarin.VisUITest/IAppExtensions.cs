using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using AForge.Imaging.Filters;
using Xamarin.UITest;
using Imaging = AForge.Imaging;
using NUnit.Framework;
using Xamarin.UITest.Queries;

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

                Assert.IsTrue(ValidateScreenshotSimilarity(imageName, newImageInfo),
                              "There is a deviation between screenshot and reference image above allowed deviation");
            }
            else
            {
                Assert.Fail("Failed to setup VisUITest image folders");
            }
        }

        public static void DontSeeVisualChanges(this IApp app, string imageName, AppResult element)
        {
            if (SetupDirectories())
            {
                FileInfo newImageInfo = app.SaveNamedScreenshot(imageName, element.Rect);

                Assert.IsTrue(ValidateScreenshotSimilarity(imageName, newImageInfo),
                              "There is a deviation between screenshot and reference image above allowed deviation");
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

                Assert.IsFalse(ValidateScreenshotSimilarity(imageName, newImageInfo),
                               "There is a deviation between screenshot and reference image below allowed deviation");

            }
            else
            {
                Assert.Fail("Failed to setup VisUITest image folders");
            }
        }

        public static void SeeVisualChanges(this IApp app, string imageName, AppResult element)
        {
            if (SetupDirectories())
            {
                FileInfo newImageInfo = app.SaveNamedScreenshot(imageName, element.Rect);

                Assert.IsFalse(ValidateScreenshotSimilarity(imageName, newImageInfo),
                               "There is a deviation between screenshot and reference image below allowed deviation");
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

        private static bool ValidateScreenshotSimilarity(string imageName, FileInfo referenceFile)
        {
            string referencePath = VisUITest.ReferenceImagePath + imageName + ".png";

            if (File.Exists(referencePath))
            {
                Bitmap source = Imaging.Image.FromFile(referenceFile.FullName);
                Bitmap reference = Imaging.Image.FromFile(referencePath);

                return source.IsIdenticalTo(reference, VisUITest.MaximumDeviation);
            }
            else
            {
                referenceFile.CopyTo(referencePath);
                Assert.Inconclusive();
            }

            return false;
        }

        private static FileInfo SaveNamedScreenshot(this IApp app, string imageName)
        {
            Rectangle screenCoords = VisUITest.GetUsableScreenCoordinates(app);

            return app.SaveNamedScreenshot(imageName, screenCoords);
        }

        private static FileInfo SaveNamedScreenshot(this IApp app, string imageName, AppRect rect)
        {
            Rectangle screenCoords = new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);

            return app.SaveNamedScreenshot(imageName, screenCoords);
        }

        private static FileInfo SaveNamedScreenshot(this IApp app, string imageName, Rectangle screenCoords)
        {
            FileInfo newImageInfo = app.Screenshot(imageName);
            string destination = VisUITest.CurrentImagePath + imageName + ".png";

            if (File.Exists(destination))
            {
                File.Delete(destination);
            }

            CropToCoordinatesAndSave(newImageInfo, imageName, screenCoords);

            return new FileInfo(destination);
        }

        private static void CropToCoordinatesAndSave(FileInfo source, string imageName, Rectangle coords)
        {
            string destination = VisUITest.CurrentImagePath + imageName + ".png";
            Bitmap originalImage = Imaging.Image.FromFile(source.FullName);
            Bitmap croppedImage;
            Crop cropper = new Crop(coords);

            croppedImage = cropper.Apply(originalImage);
            croppedImage.Save(destination, ImageFormat.Png);
        }
    }
}