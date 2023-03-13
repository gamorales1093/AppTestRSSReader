using AppTestRSSReader.Services;
using AppTestRSSReader.Services.Common;
using AppTestRSSReader.Themes;
using AppTestRSSReader.Views;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Montserrat-Bold.ttf",Alias="Montserrat-Bold")]
     [assembly: ExportFont("Montserrat-Medium.ttf", Alias = "Montserrat-Medium")]
     [assembly: ExportFont("Montserrat-Regular.ttf", Alias = "Montserrat-Regular")]
     [assembly: ExportFont("Montserrat-SemiBold.ttf", Alias = "Montserrat-SemiBold")]
     [assembly: ExportFont("UIFontIcons.ttf", Alias = "FontIcons")]
namespace AppTestRSSReader
{
    public partial class App : Application
    {
        private readonly ISettings _settingsSrv;

        public App()
        {
            InitializeComponent();
           
            ChangeThemeToLight();
            Current.RequestedThemeChanged += (s, a) =>
            {
                if (Current.UserAppTheme != OSAppTheme.Light)
                {
                    ChangeThemeToLight();
                }
            };
             ServicesRegister();
             _settingsSrv = DependencyService.Get<ISettings>();
            RegisterSyncfusionLicense();
            MainPage = new AppShell();
         }
        private void ChangeThemeToLight() 
        {
            Current.UserAppTheme = OSAppTheme.Light;

            ChangeSyncfusionThemeToLight();
        }

        private void RegisterSyncfusionLicense()
        {
            Syncfusion.Licensing
                      .SyncfusionLicenseProvider
                      .RegisterLicense(_settingsSrv.GetSyncfusionLicence());
        }
        private void ChangeSyncfusionThemeToLight()
        {
            var mergedDictionaries = Current.Resources.MergedDictionaries;

            var darkTheme = mergedDictionaries.OfType<DarkTheme>().FirstOrDefault();
            if (darkTheme != null)
            {
                mergedDictionaries.Remove(darkTheme);
            }

            mergedDictionaries.Add(new LightTheme());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void ServicesRegister()
        {
            DependencyService.Register<ISettings, Settings>();
            DependencyService.Register<IProxyApi, ProxyApi>();
        }
    }
}
