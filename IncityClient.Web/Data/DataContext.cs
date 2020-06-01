using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IncityClient.Web.Data.Entities;

namespace IncityClient.Web.Data
{
    public class DataContext : IdentityDbContext<UserEntity>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<AffiliateEntity> Affiliates { get; set; }

        public DbSet<CustomerEntity> Customers { get; set; }

        public DbSet<ItemEntity> Items { get; set; }

        public DbSet<ItemTypeEntity> ItemTypes { get; set; }

        public DbSet<ManagerEntity> Managers { get; set; }

        public DbSet<OrderEntity> Orders { get; set; }

        public DbSet<OrderDetailEntity> OrderDetails { get; set; }

        public DbSet<PlaceEntity> Places { get; set; }

        public DbSet<StatusEntity> Statuses { get; set; }
    }
}
