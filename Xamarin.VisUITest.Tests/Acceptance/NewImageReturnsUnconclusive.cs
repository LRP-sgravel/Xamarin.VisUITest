using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.VisUITest;
using Xamarin.VisUITest.Tests.Helpers;

namespace Xamarin.VisUITest.Tests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class NewImageReturnsUnconclusive
    {
        IApp app;
        Platform platform;

        public NewImageReturnsUnconclusive(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void IGetAnInconclusiveResultForNewImage()
        {
            VisUITestHelpers.RemoveReferenceImage("TestLaunch");

            Assert.Throws<InconclusiveException>(() => app.DontSeeVisualChanges("TestLaunch"));
        }
    }
}

