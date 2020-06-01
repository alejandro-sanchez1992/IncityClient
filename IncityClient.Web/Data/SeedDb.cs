using IncityClient.Web.Data.Entities;
using IncityClient.Web.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IncityClient.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public SeedDb(
            DataContext context,
            IUserHelper userHelper)
        {
            _dataContext = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckRoles();
            var manager = await CheckUserAsync("1020451189", "Alejandro", "Sanchez", "alejosanosp@gmail.com", "316 211 8090", "Calle Test 1234", "Admin");
            var affiliate = await CheckUserAsync("123232134", "Alejandro", "Sanchez", "alejandro@ilogica.com", "316 211 8090", "Calle Falsa 123", "Affiliate");
            var customer = await CheckUserAsync("102032445", "Alejandro", "Sanchez", "alejosanosp@hotmail.com", "316 211 8090", "Calle Falsa 123", "Customer");
            
            await CheckCustomerAsync(customer);
            await CheckAffiliateAsync(affiliate);
            await CheckManagerAsync(manager);
        }

        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Affiliate");
            await _userHelper.CheckRoleAsync("Customer");
        }

        private async Task<UserEntity> CheckUserAsync(
            string document, 
            string firstName, 
            string lastName, 
            string email, 
            string phone, 
            string address, 
            string role)
        {
            var user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new UserEntity
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, role);

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            return user;
        }

        private async Task CheckCustomerAsync(UserEntity user)
        {
            if (!_dataContext.Customers.Any())
            {
                _dataContext.Customers.Add(new CustomerEntity { User = user });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckAffiliateAsync(UserEntity user)
        {
            if (!_dataContext.Affiliates.Any())
            {
                _dataContext.Affiliates.Add(new AffiliateEntity { User = user });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckManagerAsync(UserEntity user)
        {
            if (!_dataContext.Managers.Any())
            {
                _dataContext.Managers.Add(new ManagerEntity { User = user });
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}
