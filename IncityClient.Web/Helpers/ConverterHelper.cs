using System.Threading.Tasks;
using IncityClient.Common.Models;
using IncityClient.Web.Data;
using IncityClient.Web.Data.Entities;
using IncityClient.Web.Models;

namespace IncityClient.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _dataContext;

        public ConverterHelper(
            DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public CustomerResponse ToCustomerResponse(CustomerEntity customer)
        {
            if (customer == null)
            {
                return null;
            }

            return new CustomerResponse
            {
                Id = customer.User.Id,
                Address = customer.User.Address,
                Document = customer.User.Document,
                Email = customer.User.Email,
                FirstName = customer.User.FirstName,
                LastName = customer.User.LastName,
                LoginType = customer.User.LoginType,
                PhoneNumber = customer.User.PhoneNumber,
                PicturePath = customer.User.PicturePath,
            };
        }
    }
}
