using System.Drawing;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Xamarin.VisUITest.Tests.Helpers;

namespace Xamarin.VisUITest.Tests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class AppResultScreenshotTests
    {
        IApp app;
        Platform platform;

        public AppResultScreenshotTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void ICanCompareASingleElement()
        {
            Bitmap referenceImage;
            AppResult label;

            label = app.Query("Just Another Label").FirstOrDefault();

            app.DontSeeVisualChanges("ElementScreenshotTest", label);
            referenceImage = VisUITestHelpers.LoadReferenceImage("ElementScreenshotTest");

            Assert.AreEqual(label.Rect.Height, referenceImage.Height);
            Assert.AreEqual(label.Rect.Width, referenceImage.Width);
        }
    }
}
