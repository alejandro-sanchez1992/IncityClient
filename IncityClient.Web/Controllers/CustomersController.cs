using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IncityClient.Web.Data;
using IncityClient.Web.Data.Entities;
using IncityClient.Web.Helpers;
using IncityClient.Web.Models;

namespace IncityClient.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomersController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IMailHelper _mailHelper;

        public CustomersController(
            DataContext context,
            IUserHelper userHelper,
            IConverterHelper converterHelper,
            IImageHelper imageHelper,
            IMailHelper mailHelper)
        {
            _dataContext = context;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
            _mailHelper = mailHelper;
        }

        public IActionResult Index()
        {
            return View(_dataContext.Customers
                .Include(o => o.User)
                .Include(o => o.Orders));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _dataContext.Customers
                .Include(o => o.User)
                .Include(o => o.Orders)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserEntity
                {
                    Address = model.Address,
                    Document = model.Document,
                    Email = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Username
                };

                var response = await _userHelper.AddUserAsync(user, model.Password);
                if (response.Succeeded)
                {
                    var userInDB = await _userHelper.GetUserAsync(model.Username);
                    await _userHelper.AddUserToRoleAsync(userInDB, "Customer");

                    var customer = new CustomerEntity
                    {
                        Orders = new List<OrderEntity>(),
                        User = userInDB
                    };

                    _dataContext.Customers.Add(customer);

                    try
                    {
                        await _dataContext.SaveChangesAsync();

                        var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                        var tokenLink = Url.Action("ConfirmEmail", "Account", new
                        {
                            userid = user.Id,
                            token = myToken
                        }, protocol: HttpContext.Request.Scheme);

                        _mailHelper.SendMail(model.Username, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                            $"To allow the user, " +
                            $"plase click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");

                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.ToString());
                        return View(model);
                    }
                }

                ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _dataContext.Customers
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Address = customer.User.Address,
                Document = customer.User.Document,
                FirstName = customer.User.FirstName,
                Id = customer.Id,
                LastName = customer.User.LastName,
                PhoneNumber = customer.User.PhoneNumber
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = await _dataContext.Customers
                    .Include(o => o.User)
                    .FirstOrDefaultAsync(o => o.Id == model.Id);

                customer.User.Document = model.Document;
                customer.User.FirstName = model.FirstName;
                customer.User.LastName = model.LastName;
                customer.User.Address = model.Address;
                customer.User.PhoneNumber = model.PhoneNumber;

                await _userHelper.UpdateUserAsync(customer.User);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        private bool OwnerExists(int id)
        {
            return _dataContext.Customers.Any(e => e.Id == id);
        }
    }
}
