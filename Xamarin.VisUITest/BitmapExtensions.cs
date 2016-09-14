using System;
using System.Drawing;
using AForge.Imaging;

namespace Xamarin.VisUITest
{
    public static class BitmapExtensions
    {
        public static bool IsIdenticalTo(this Bitmap source, Bitmap reference, int permittedDeviation = 0)
        {
            bool result = true;

            if (source != reference)
            {
                TemplateMatch[] compareResult = null;
                ExhaustiveTemplateMatching matcher = new ExhaustiveTemplateMatching();

                if (source.Width != reference.Width || source.Height != reference.Height)
                {
                    result = false;
                }
                else
                {
                    // Process the images
                    compareResult = matcher.ProcessImage(source, reference);

                    if (compareResult.Length > 0)
                    {
                        result = (Math.Round(compareResult[0].Similarity * 100, MidpointRounding.AwayFromZero) + permittedDeviation) >= 100;
                    }
                }
            }

            return result;
        }
    }
}
