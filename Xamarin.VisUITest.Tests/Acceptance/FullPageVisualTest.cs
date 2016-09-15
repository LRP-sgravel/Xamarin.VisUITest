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
    public class FullPageVisualTest
    {
        IApp app;
        Platform platform;

        public FullPageVisualTest(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void ICanCompareFullScreens()
        {
            Bitmap referenceImage;
            AppResult page;

            page = app.Query("content").FirstOrDefault();

            app.DontSeeVisualChanges("FullScreenTest");
            referenceImage = VisUITestHelpers.LoadReferenceImage("FullScreenTest");

            Assert.AreEqual(page.Rect.Height, referenceImage.Height);
            Assert.AreEqual(page.Rect.Width, referenceImage.Width);
        }
    }
}

