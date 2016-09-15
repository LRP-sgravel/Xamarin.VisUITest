using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Localization;
using MvvmCross.Platform;
using Xamarin.VisUITest.App.Services;

namespace Xamarin.VisUITest.App.ViewModels
{
    class LoadingViewModel : MvxViewModel
    {
        private Bootstrapper Bootstrapper { get; set; }

        public string CopyrightText { get; set; }

        private string mStatusText = string.Empty;
        public string StatusText
        {
            get { return mStatusText; }
            private set
            {
                if (StatusText != value)
                {
                    mStatusText = value;
                    RaisePropertyChanged(() => StatusText);
                }
            }
        }

        public LoadingViewModel()
        {
            IMvxTextProvider textProvider = Mvx.Resolve<IMvxTextProvider>();

            Bootstrapper = new Bootstrapper();

            CopyrightText = textProvider.GetText(Constants.GeneralNamespace, Constants.TextTypeKey, "Copyright");
        }

        public override void Start()
        {
            base.Start();

            Bootstrapper.BootTextChanged += status => { StatusText = status; };
            Bootstrapper.BootCompleted += OnBootCompleted;

            Bootstrapper.Boot();
        }

        public void OnBootCompleted()
        {

        }
    }
}
