using System;
using System.Threading;

namespace Xamarin.VisUITest.App.Services
{
    class MainThread
    {
        private static MainThread mInstance = null;

        private SynchronizationContext mMainThreadContext;
        public static SynchronizationContext Context
        {
            get
            {
                return mInstance.mMainThreadContext;
            }
        }

        public MainThread()
        {
            mInstance = this;
            mMainThreadContext = SynchronizationContext.Current;
        }
    }
}
