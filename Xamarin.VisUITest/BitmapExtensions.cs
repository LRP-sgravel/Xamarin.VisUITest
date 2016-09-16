using System;
using System.Drawing;
using AForge.Imaging;

namespace Xamarin.VisUITest
{
    public static class BitmapExtensions
    {
        public static bool IsIdenticalTo(this Bitmap source, Bitmap reference, float permittedDeviation = 0)
        {
            bool result = (source == reference);

            if (!result)
            {
                if (source.Width == reference.Width && source.Height == reference.Height)
                {
                    try
                    {
                        // Process the images
                        ExhaustiveTemplateMatching matcher = new ExhaustiveTemplateMatching((100 - permittedDeviation) / 100.0f);
                        TemplateMatch[] compareResult = matcher.ProcessImage(source, reference);

                        result = (compareResult.Length > 0);
                    }
                    catch
                    {
                    }
                }
            }

            return result;
        }
    }
}
