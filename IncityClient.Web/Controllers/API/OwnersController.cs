using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IncityClient.Common.Models;
using IncityClient.Web.Data;

namespace IncityClient.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OwnersController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public OwnersController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("GetOwnerByEmail")]
        public async Task<IActionResult> GetOwner(EmailRequest emailRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customer = await _dataContext.Customers
                .Include(o => o.User)
                .Include(o => o.Orders)
                .ThenInclude(p => p.OrderDetails)
                .FirstOrDefaultAsync(o => o.User.UserName.ToLower() == emailRequest.Email.ToLower());

            var response = new CustomerResponse
            {
                Id = customer.User.Id,
                FirstName = customer.User.FirstName,
                LastName = customer.User.LastName,
                Address = customer.User.Address,
                Document = customer.User.Document,
                Email = customer.User.Email,
                PhoneNumber = customer.User.PhoneNumber,
                PicturePath = customer.User.PicturePath,
                Orders = customer.Orders.Select(o => new OrderResponse {
                    Id = o.Id,
                    Date = o.Date,
                    Place = o.Place.Name,
                    Total = o.Total,
                    Remarks = o.Remarks,
                    Status = o.Status.Name,
                    OrderDetails = o.OrderDetails.Select(d => new OrderDetailResponse {
                        Id = d.Id,
                        Quantity = d.Quantity,
                        Items = d.Items.Select(i => new ItemResponse {
                            Id = i.Id,
                            ImageUrl = i.ImageUrl,
                            ItemType = i.ItemType.Name,
                            Name = i.Name,
                            Price = i.Price,
                            Remarks = i.Remarks,
                        }).ToList(),
                    }).ToList(),
                }).ToList(),
            };

            return Ok(response);
        }
    }
}
