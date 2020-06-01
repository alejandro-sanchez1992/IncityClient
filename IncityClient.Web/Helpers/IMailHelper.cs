using IncityClient.Common.Models;

namespace IncityClient.Web.Helpers
{
    public interface IMailHelper
    {
        Response<object> SendMail(string to, string subject, string body);
    }
}
