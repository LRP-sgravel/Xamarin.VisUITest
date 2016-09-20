using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.VisUITest;

namespace Xamarin.VisUITest.Tests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class StatusBarHideTests
    {
        IApp app;
        Platform platform;

        public StatusBarHideTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void ICanHideTheStatusBar()
        {
            VisUITest.AlwaysRemoveStatusBar = true;

            app.DontSeeVisualChanges("StatusBarHiddenTest");
        }

        [Test]
        public void ICanShowTheStatusBar()
        {
            VisUITest.AlwaysRemoveStatusBar = false;

            app.DontSeeVisualChanges("StatusBarVisibleTest");
        }
    }
}

