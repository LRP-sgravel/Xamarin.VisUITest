using System;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.UITest;
using Xamarin.UITest.iOS;

namespace Xamarin.VisUITest
{
    public class VisUITest
    {
        private string _referenceImagePath = "../../../_output/VisUITest/ref/";
        public static string ReferenceImagePath
        {
            get { return _Instance._referenceImagePath; }
            set { _Instance._referenceImagePath = value; }
        }

        private string _currentImagePath = "../../../_output/VisUITest/";
        public static string CurrentImagePath
        {
            get { return _Instance._currentImagePath; }
            set { _Instance._currentImagePath = value; }
        }

        private float _maximumDeviation = 0;
        public static float MaximumDeviation
        {
            get { return _Instance._maximumDeviation; }
            set
            {
                if (value > 100)
                {
                    throw new ArgumentOutOfRangeException("MaximumDeviation", "Maximum deviation must be between 0 and 100");
                }

                _Instance._maximumDeviation = value;
            }
        }

        private static VisUITest _instance = null;
        private static VisUITest _Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new VisUITest();
                }

                return _instance;
            }
        }

        private VisUITest()
        {
        }

        private static string GetPlatformScreenCoordsBackdoorName(IApp app)
        {
            Platform platform = app is iOSApp
                                    ? Platform.iOS
                                    : Platform.Android;
            string backdoorName = string.Empty;

            if (platform == Platform.iOS)
            {
                backdoorName = "getScreenSize:";
            }
            else
            {
                backdoorName = "GetUsableScreenCoordinates";
            }

            return backdoorName;
        }

        public static Rectangle GetUsableScreenCoordinates(IApp app)
        {
            try
            {
                string coordsJson = app.Invoke(GetPlatformScreenCoordsBackdoorName(app)) as string;
                JObject coordsJObject = JsonConvert.DeserializeObject<JObject>(coordsJson);

                return new Rectangle(coordsJObject.Value<int>("X"),
                                     coordsJObject.Value<int>("Y"),
                                     coordsJObject.Value<int>("Width"),
                                     coordsJObject.Value<int>("Height"));
            }
            catch (Exception e)
            {
                Platform platform = app is iOSApp
                                        ? Platform.iOS
                                        : Platform.Android;
                string error = "Cannot find 'GetScreenCoords' VisUITest backdoor.  ";

                if (platform == Platform.Android)
                {
                    error += "Was the Android NuGet package added to your project and does your current Activity subclass VisUITestActivity?";
                }
                else if (platform == Platform.iOS)
                {
                    error += "Did you include the iOS VisUITest NuGet package in your project?";
                }

                throw new MissingMethodException(error);
            }
        }
    }
}