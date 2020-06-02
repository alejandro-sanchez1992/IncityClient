using System;
namespace IncityClient.Prism.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            instance = this;
        }

        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
    }
}
