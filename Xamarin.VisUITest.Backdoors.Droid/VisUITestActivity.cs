using System;
using Android.App;
using Android.Runtime;

namespace Xamarin.VisUITest.Backdoors.Droid
{
    public partial class VisUITestActivity : Activity
    {
        public VisUITestActivity()
        {
        }

        public VisUITestActivity(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
    }
}