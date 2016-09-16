using System;
using Acr.Settings;
using Xamarin.VisUITest.App.Services;
using Xamarin.VisUITest.App.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;

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

            RegisterAppStart<LoadingViewModel>();
        }

        public static void InitializeResources()
        {
            Mvx.RegisterSingleton(new MainThread());
            Mvx.RegisterSingleton<ISettings>(Settings.Local);
        }
    }
}