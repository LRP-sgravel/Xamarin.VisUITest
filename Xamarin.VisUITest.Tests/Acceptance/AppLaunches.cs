using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.VisUITest;

namespace Xamarin.VisUITest.Tests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class AppLaunches
    {
        IApp app;
        Platform platform;

        public AppLaunches(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void TestLaunch()
        {
            app.DontSeeVisualChanges("TestLaunch");
        }
    }
}

