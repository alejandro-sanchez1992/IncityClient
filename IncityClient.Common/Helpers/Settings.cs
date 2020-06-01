using Plugin.Settings;
using Plugin.Settings.Abstractions;
namespace IncityClient.Common.Helpers
{
    public static class Settings
    {
        private const string _customer = "Customer";
        private const string _token = "token";
        private const string _isLogin = "isLogin";
        private const string _isRemembered = "IsRemembered";
        private static readonly string _stringDefault = string.Empty;
        private static readonly bool _boolDefault = false;

        private static ISettings AppSettings => CrossSettings.Current;

        public static string Customer
        {
            get => AppSettings.GetValueOrDefault(_customer, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_customer, value);
        }

        public static string Token
        {
            get => AppSettings.GetValueOrDefault(_token, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_token, value);
        }

        public static bool IsLogin
        {
            get => AppSettings.GetValueOrDefault(_isLogin, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isLogin, value);
        }

        public static bool IsRemembered
        {
            get => AppSettings.GetValueOrDefault(_isRemembered, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isRemembered, value);
        }
    }
}
