using System;
using IncityClient.Common.Helpers;
using IncityClient.Common.interfaces;
using IncityClient.Common.Models;
using IncityClient.Prism.Helpers;
using IncityClient.Prism.Views;
using Newtonsoft.Json;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using Prism.Commands;
using Prism.Navigation;

namespace IncityClient.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private bool _isEnabled;
        private string _password;
        private DelegateCommand _loginCommand;
        private DelegateCommand _registerCommand;
        private DelegateCommand _forgotPasswordCommand;
        private ToastLength _toastLength = ToastLength.Short;

        public LoginPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            Title = "Login";
            IsEnabled = true;
            _navigationService = navigationService;
            _apiService = apiService;
        }

        public DelegateCommand ForgotPasswordCommand => _forgotPasswordCommand ?? (_forgotPasswordCommand = new DelegateCommand(ForgotPasswordAsync));

        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(LoginAsync));

        public DelegateCommand RegisterCommand => _registerCommand ?? (_registerCommand = new DelegateCommand(RegisterAsync));

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public string Email { get; set; }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private async void ForgotPasswordAsync()
        {
            await _navigationService.NavigateAsync(nameof(RememberPasswordPage));
        }

        private async void RegisterAsync()
        {
            await _navigationService.NavigateAsync(nameof(RegisterPage));
        }

        private async void LoginAsync()
        {
            if (string.IsNullOrEmpty(Email))
            {
                CrossToastPopUp.Current.ShowToastError(Languages.EmailError, _toastLength);
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                CrossToastPopUp.Current.ShowToastError(Languages.PasswordError, _toastLength);
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            TokenRequest request = new TokenRequest
            {
                Password = Password,
                Username = Email
            };

            Response<TokenResponse> response = await _apiService.GetTokenAsync(url, "Account", "/CreateToken", request);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LoginError, Languages.Accept);
                Password = string.Empty;
                return;
            }

            TokenResponse token = (TokenResponse)response.Result;
            EmailRequest emailRequest = new EmailRequest
            {
                CultureInfo = Languages.Culture,
                Email = Email
            };

            Response<CustomerResponse> response2 = await _apiService.GetUserByEmail(url, "api", "/Account/GetUserByEmail", "bearer", token.Token, emailRequest);
            CustomerResponse userResponse = (CustomerResponse)response2.Result;

            Settings.Customer = JsonConvert.SerializeObject(userResponse);
            Settings.Token = JsonConvert.SerializeObject(token);
            Settings.IsLogin = true;

            IsRunning = false;
            IsEnabled = true;

            await _navigationService.NavigateAsync($"/MainMasterDetailPage/NavigationPage/{nameof(HomePage)}");
            Password = string.Empty;
        }
    }
}
