using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using IncityClient.Common.Helpers;
using IncityClient.Common.interfaces;
using IncityClient.Common.Models;
using IncityClient.Prism.Helpers;
using IncityClient.Prism.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;

namespace IncityClient.Prism.ViewModels
{
    public class MainMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private CustomerResponse _user;
        private DelegateCommand _modifyUserCommand;
        private static MainMasterDetailPageViewModel _instance;

        public MainMasterDetailPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _instance = this;
            _navigationService = navigationService;
            this._apiService = apiService;
            LoadUser();
            LoadMenus();
        }

        public DelegateCommand ModifyUserCommand => _modifyUserCommand ?? (_modifyUserCommand = new DelegateCommand(ModifyUserAsync));

        public CustomerResponse Customer
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public static MainMasterDetailPageViewModel GetInstance()
        {
            return _instance;
        }

        public async void ReloadUser()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                return;
            }

            CustomerResponse customer = JsonConvert.DeserializeObject<CustomerResponse>(Settings.Customer);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            EmailRequest emailRequest = new EmailRequest
            {
                CultureInfo = Languages.Culture,
                Email = customer.Email
            };

            Response<CustomerResponse> response = await _apiService.GetUserByEmail(url, "api", "/Account/GetUserByEmail", "bearer", token.Token, emailRequest);
            CustomerResponse userResponse = (CustomerResponse)response.Result;
            Settings.Customer = JsonConvert.SerializeObject(userResponse);
            LoadUser();
        }

        private async void ModifyUserAsync()
        {
            await _navigationService.NavigateAsync($"/MainMasterDetailPage/NavigationPage/{nameof(ModifyUserPage)}");
        }

        private void LoadUser()
        {
            if (Settings.IsLogin)
            {
                Customer = JsonConvert.DeserializeObject<CustomerResponse>(Settings.Customer);
            }
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            var menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_trips",
                    PageName = "HomePage",
                    Title = "My Orders"
                },

                new Menu
                {
                    Icon = "ic_edit_user",
                    PageName = "ModifyUserPage",
                    Title = "Modify Profile"
                },

                new Menu
                {
                    Icon = "ic_exit",
                    PageName = "LoginPage",
                    Title = "Logout"
                }
            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
        }
    }
}
