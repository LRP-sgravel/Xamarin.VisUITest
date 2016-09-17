using Xamarin.UITest;

namespace Xamarin.VisUITest.Tests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            IApp result = null;

            if (platform == Platform.Android)
            {
                result = ConfigureApp.Android
                                     .ApkFile(
                                         "../../../Xamarin.VisUITest.App.Droid/bin/Debug/Xamarin.VisUITest.App.Droid-Signed.apk")
                                     .DeviceSerial("emulator-5554")
                                     .EnableLocalScreenshots()
                                     .StartApp();
            }
            else
            {
                result = ConfigureApp.iOS
                                     .AppBundle(
                                         "../../../Xamarin.VisUITest.App.iOS/bin/iPhoneSimulator/Debug/XamarinVisUITestAppiOS.app")
                                     .DeviceIdentifier("CC1A7354-8452-4C33-9BF3-453905AC0456")
                                     .EnableLocalScreenshots()
                                     .StartApp();
            }

            // This is required to ensure the view is properly loaded (and therefore the backdoor is available)
            result.WaitForElement(query => query.Id("content"));
            result.Query("content");

            return result;
        }
    }
}

