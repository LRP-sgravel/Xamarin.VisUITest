using System;
using System.Linq;
using System.Reflection;
using MvvmCross.Platform.Platform;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.VisUITest.App.Extensions
{
    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            ImageSource result = null;

            if (Source != null)
            {
                string resourceFullName = BuildPath(Constants.GeneralNamespace,
                                                    Constants.RootImagesFolder,
                                                    Source);

#if DEBUG
                var names = Assembly.Load(new AssemblyName("Xamarin.VisUITest.App")).GetManifestResourceNames();
                if (!names.Contains(resourceFullName))
                {
                    MvxTrace.Error("Trying to bind image from resource {0}, but it wasn't found");
                }
#endif

                result = ImageSource.FromResource(resourceFullName, typeof(ImageResourceExtension));
            }

            return result;
        }

        private string BuildPath(params string[] list)
        {
            string result = "";

            foreach (string segment in list)
            {
                result += "." + segment.Replace("/", ".");
            }

            return result.Remove(0, 1);
        }
    }
}