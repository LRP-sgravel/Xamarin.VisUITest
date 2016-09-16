using System;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.UITest;

namespace Xamarin.VisUITest
{
    public class VisUITest
    {
        private string _referenceImagePath = "_output/VisUITest/ref/";
        public static string ReferenceImagePath
        {
            get { return _Instance._referenceImagePath; }
            set { _Instance._referenceImagePath = value; }
        }

        private string _currentImagePath = "_output/VisUITest/";
        public static string CurrentImagePath
        {
            get { return _Instance._currentImagePath; }
            set { _Instance._currentImagePath = value; }
        }
        
        private Platform _platform = Platform.iOS;
        public static Platform Platform
        {
            get { return _Instance._platform; }
            set { _Instance._platform = value; }
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

        private static string GetPlatformScreenCoordsBackdoorName()
        {
            string backdoorName = string.Empty;

            if (Platform == Platform.iOS)
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
                string coordsJson = app.Invoke(GetPlatformScreenCoordsBackdoorName()) as string;
                JObject coordsJObject = JsonConvert.DeserializeObject<JObject>(coordsJson);

                return new Rectangle(coordsJObject.Value<int>("X"),
                                     coordsJObject.Value<int>("Y"),
                                     coordsJObject.Value<int>("Width"),
                                     coordsJObject.Value<int>("Height"));
            }
            catch (Exception e)
            {
                string error = "Cannot find 'GetScreenCoords' VisUITest backdoor.  ";

                if (Platform == Platform.Android)
                {
                    error += "Was teh Android NuGet package added to your project and does your current Activity subclass VisUITestActivity?";
                }
                else if (Platform == Platform.iOS)
                {
                    error += "Did you include the iOS VisUITest NuGet package in your project?";
                }

                throw new MissingMethodException(error);
            }
        }
    }
}