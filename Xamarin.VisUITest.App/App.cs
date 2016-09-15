using System;
using Acr.Settings;
using Xamarin.VisUITest.App.Services;
using Xamarin.VisUITest.App.ViewModels;
using MvvmCross.Localization;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using MvvmCross.Plugins.JsonLocalization;

namespace Xamarin.VisUITest.App
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            InitializeText();

            RegisterAppStart<LoadingViewModel>();
        }

        public static void InitializeResources()
        {
            Mvx.RegisterSingleton(new MainThread());
            Mvx.RegisterSingleton<ISettings>(Settings.Local);
        }

        private void InitializeText()
        {
            var builder = new TextProviderBuilder();
            Mvx.RegisterSingleton<IMvxTextProviderBuilder>(builder);
            Mvx.RegisterSingleton<IMvxTextProvider>(builder.TextProvider);

            // Set language
            builder.LoadResources("FR_fr");
        }
    }
}