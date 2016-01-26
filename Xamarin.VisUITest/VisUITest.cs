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