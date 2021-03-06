﻿using FirstApp.Core;
using FirstApp.Core.Interfaces;
using FirstApp.iOS.Converters;
using FirstApp.iOS.Services;
using MvvmCross;
using MvvmCross.Base;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Converters;
using MvvmCross.IoC;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.Plugin.Json;
using MvvmCross.ViewModels;

namespace FirstApp.iOS
{
    public class Setup : MvxIosSetup<App>
    {
        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();

            var registry = Mvx.IoCProvider.Resolve<IMvxTargetBindingFactoryRegistry>();
        }
        protected override IMvxApplication CreateApp()
        {
            CreatableTypes()
                      .EndingWith("Service")
                      .AsInterfaces()
                      .RegisterAsLazySingleton();

            Mvx.IoCProvider.RegisterType<IConnectionService, DBConnectionService>();
            Mvx.IoCProvider.RegisterType<ICurrentPlatformService, CurrentPlatformService>();
            Mvx.IoCProvider.RegisterType<IMvxJsonConverter, MvxJsonConverter>();

            return new App();
        }

        protected override void FillValueConverters(IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);
            registry.AddOrOverwrite(Constants.ByteArrayToImg, new ByteArrayToImgValueConverter());
        }
    }
}
