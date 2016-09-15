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

        private ushort _maximumDeviation = 0;
        public static ushort MaximumDeviation
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

        private static string GetPlatformScreenCordsBackdoorName()
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
            string coordsJson = app.Invoke(GetPlatformScreenCordsBackdoorName()) as string;
            JObject coordsJObject = JsonConvert.DeserializeObject<JObject>(coordsJson);

            return new Rectangle(coordsJObject.Value<int>("X"),
                                 coordsJObject.Value<int>("Y"),
                                 coordsJObject.Value<int>("Width"),
                                 coordsJObject.Value<int>("Height"));
        }
    }
}