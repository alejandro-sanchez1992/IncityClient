using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using IncityClient.Common.Models;
using Prism.Commands;
using Prism.Navigation;

namespace IncityClient.Prism.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private bool _isRunning;
        private bool _isEnabled;
        private static HomePageViewModel _instance;
        private string _image;
        private ObservableCollection<Carousel> _carousels = new ObservableCollection<Carousel>();

        public HomePageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            _instance = this;
            _navigationService = navigationService;
            Title = "Incity";
            IsEnabled = true;
            LoadCarousel();
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

        public string Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public ObservableCollection<Carousel> Carousels {
            get => _carousels;
            set => SetProperty(ref _carousels, value);
        }

        public void LoadCarousel()
        {
            _carousels.Add(
                new Carousel
                {
                    Image = "slider_1.jpg",
                    Title = "slider 1"
                }
            );

            _carousels.Add(
                new Carousel
                {
                    Image = "slider_2.jpg",
                    Title = "slider 2"
                }
            );

            _carousels.Add(
                new Carousel
                {
                    Image = "slider_3.jpg",
                    Title = "slider 3"
                }
            );
        }
    }
}
