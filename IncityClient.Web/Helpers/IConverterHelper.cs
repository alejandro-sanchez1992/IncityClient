using System.Threading.Tasks;
using IncityClient.Common.Models;
using IncityClient.Web.Data.Entities;
using IncityClient.Web.Models;

namespace IncityClient.Web.Helpers
{
    public interface IConverterHelper
    {
        CustomerResponse ToCustomerResponse(CustomerEntity customer);
    }
}