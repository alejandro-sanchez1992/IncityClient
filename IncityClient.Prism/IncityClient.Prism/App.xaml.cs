using Prism;
using Prism.Ioc;
using IncityClient.Common.Services;
using System;
using IncityClient.Prism.ViewModels;
using IncityClient.Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using IncityClient.Common.Models;
using IncityClient.Common.Helpers;
using Plugin.Multilingual;
using Syncfusion.Licensing;
using IncityClient.Common.interfaces;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace IncityClient.Prism
{
    public partial class App
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            Prism.Resources.Resource.Culture = CrossMultilingual.Current.DeviceCultureInfo;
            SyncfusionLicenseProvider.RegisterLicense("MjM5MDIxQDMxMzgyZTMxMmUzME9zVWx4ME1xVVpSL2NaMDFnMXVSbXNnQjUzUnNENHR5Wk5SeTNPcWJWUW89");
            InitializeComponent();

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            if (Settings.IsLogin && token?.Expiration > DateTime.Now)
            {
                await NavigationService.NavigateAsync("/MainMasterDetailPage/NavigationPage/HomePage");
            }
            else
            {
                await NavigationService.NavigateAsync($"/NavigationPage/{nameof(LoginPage)}");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.Register<IFilesHelper, FilesHelper>();
            containerRegistry.Register<IRegexHelper, RegexHelper>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<MainMasterDetailPage, MainMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<ModifyUserPage, ModifyUserPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<RememberPasswordPage, RememberPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<ChangePasswordPage, ChangePasswordPageViewModel>();
        }
    }

}