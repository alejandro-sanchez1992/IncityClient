using System;
using Prism.Navigation;

namespace IncityClient.Prism.ViewModels
{
    public class MainMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private static MainMasterDetailPageViewModel _instance;

        public MainMasterDetailPageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            _instance = this;
            _navigationService = navigationService;
        }

        public static MainMasterDetailPageViewModel GetInstance()
        {
            return _instance;
        }
    }
}
