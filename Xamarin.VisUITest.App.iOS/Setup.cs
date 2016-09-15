using System.Collections.Generic;
using System.Reflection;
using MvvmCross.Platform.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using UIKit;
using MvvmCross.Forms.Presenter.iOS;
using MvvmCross.Forms.Presenter.Core;
using MvvmCross.Localization;

namespace Xamarin.VisUITest.App.iOS
{
    public class Setup : MvxIosSetup
    {
        public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
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

        protected override IMvxIosViewPresenter CreatePresenter()
        {
            Forms.Forms.Init();
            App.InitializeResources();

            var xamarinFormsApp = new MvxFormsApp();

            return new MvxFormsIosPagePresenter(Window, xamarinFormsApp);
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