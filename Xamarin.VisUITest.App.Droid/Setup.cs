using System.Collections.Generic;
using System.Reflection;
using Android.Content;
using Xamarin.VisUITest.App;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MvvmCross.Droid.Platform;
using MvvmCross.Droid.Views;
using MvvmCross.Forms.Presenter.Droid;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Localization;
using Xamarin.VisUITest.App.Models;

namespace Xamarin.VisUITest.App.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var presenter = new MvxFormsDroidPagePresenter();
            Mvx.RegisterSingleton<IMvxViewPresenter>(presenter);

            return presenter;
        }

        protected override void InitializePlatformServices()
        {
            base.InitializePlatformServices();
        }

        protected override IEnumerable<Assembly> ValueConverterAssemblies
        {
            get
            {
                List<Assembly> result = new List<Assembly>(base.ValueConverterAssemblies);

                result.Add(typeof(MvxLanguageConverter).Assembly);

                return result;
            }
        }
    }
}