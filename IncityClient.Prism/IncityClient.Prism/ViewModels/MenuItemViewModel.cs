using IncityClient.Common.Helpers;
using IncityClient.Common.Models;
using Prism.Commands;
using Prism.Navigation;

namespace IncityClient.Prism.ViewModels
{
    public class MenuItemViewModel : Menu
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectMenuCommand;

        public MenuItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectMenuCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));

        private async void SelectMenuAsync()
        {
            if (PageName == "LoginPage" && Settings.IsLogin)
            {
                Settings.IsLogin = false;
                Settings.Customer = null;
                Settings.Token = null;
            }

            await _navigationService.NavigateAsync($"/MainMasterDetailPage/NavigationPage/{PageName}");
        }
    }
}
