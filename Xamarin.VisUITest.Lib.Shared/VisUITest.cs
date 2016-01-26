using System;

namespace Xamarin.VisUITest
{
    public class VisUITest
    {
        private string mReferenceImagePath = "./_output/VisUITest/ref/";
        public static string ReferenceImagePath
        {
            get { return Instance.mReferenceImagePath; }
            set { Instance.mReferenceImagePath = value; }
        }

        private string mCurrentImagePath = "./_output/VisUITest/";
        public static string CurrentImagePath
        {
            get { return Instance.mCurrentImagePath; }
            set { Instance.mCurrentImagePath = value; }
        }

        private ushort mMaximumDeviation = 0;
        public static ushort MaximumDeviation
        {
            get { return Instance.mMaximumDeviation; }
            set
            {
                if (value > 100)
                {
                    throw new ArgumentOutOfRangeException("MaximumDeviation", "Maximum deviation must be between 0 and 100");
                }

                Instance.mMaximumDeviation = value;
            }
        }
        private static VisUITest mInstance = null;
        private static VisUITest Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new VisUITest();
                }

                return mInstance;
            }
        }

        private VisUITest()
        {
        }
    }
}