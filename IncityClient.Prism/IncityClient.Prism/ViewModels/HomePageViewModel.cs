using System;
using Prism.Commands;
using Prism.Navigation;

namespace IncityClient.Prism.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _addTripsCommand;
        private bool _isRunning;
        private bool _isEnabled;
        private static HomePageViewModel _instance;

        public HomePageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            _instance = this;
            _navigationService = navigationService;
            Title = "My Trips Page";
            IsEnabled = true;
            //LoadEmployee();
        }

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
    }
}
