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
                                         "../../../Xamarin.VisUITest.App.iOS/bin/iPhoneSimulator/Debug/Xamarin.VisUITest.App.iOS.app")
                                     .DeviceIdentifier("C1DB5CD9-0648-4D1C-ACAA-A4115BE48714")
                                     .EnableLocalScreenshots()
                                     .PreferIdeSettings()
                                     .StartApp();
            }

            if(platform == Platform.Android)
            {
                // This is required to ensure the view is properly loaded (and therefore the backdoor is available)
                result.WaitForElement(query => query.Id("content"));
                result.Query("content");
            }
            else
            {
                result.WaitForElement(query => query.Class("Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer"));
            }

            return result;
        }
    }
}

