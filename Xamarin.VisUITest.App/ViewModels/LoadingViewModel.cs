using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Xamarin.VisUITest.App.Models;

namespace Xamarin.VisUITest.App.ViewModels
{
    class LoadingViewModel : MvxViewModel
    {
        public string ScreenSizeWithoutStatusbar
        {
            get
            {
                return "AlwaysRemove(true) : " + Mvx.Resolve<ScreenSizes>()
                                                    .WithoutStatus;
            }
        }

        public string ScreenSizeWithStatusbar
        {
            get
            {
                return "AlwaysRemove(false) : " + Mvx.Resolve<ScreenSizes>()
                                                     .WithStatus;
            }
        }

        public LoadingViewModel()
        {
        }

        public override void Start()
        {
            base.Start();
        }
    }
}
