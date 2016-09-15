using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Xamarin.VisUITest.Tests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .ApkFile("../../../Xamarin.VisUITest.App.Droid/bin/Debug/Xamarin.VisUITest.App.Droid-Signed.apk")
                    .DeviceSerial("emulator-5554")
                    .EnableLocalScreenshots()
                    .StartApp();
            }

            return ConfigureApp
                .iOS
                .AppBundle("../../../Xamarin.VisUITest.App.iOS/bin/iPhoneSimulator/Debug/XamarinVisUITestAppiOS.app")
                .DeviceIdentifier("CC1A7354-8452-4C33-9BF3-453905AC0456")
                .EnableLocalScreenshots()
                .StartApp();
        }
    }
}

