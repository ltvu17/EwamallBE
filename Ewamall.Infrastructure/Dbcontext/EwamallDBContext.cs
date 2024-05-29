using Ewamall.Business.Entities;
using Ewamall.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Ewamall.Infrastructure.Dbcontext
{
    public class EwamallDBContext : DbContext
    {
        public EwamallDBContext()
        {
            
        }
        public EwamallDBContext(DbContextOptions<EwamallDBContext> options) : base(options)
        {
            
        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<IndustryDetail> IndustryDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ProductDetail> ProductDetail { get; set; }
        public DbSet<ProductSellDetail> ProductSellDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ShipAddress> ShipAddresses { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=EwamallTestDb; Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
                //optionsBuilder.UseSqlServer("Data Source = '116.109.26.89, 1433'; Initial Catalog = EwamallTestDb; User ID = sa; Password = vu27062002; Connect Timeout = 30; Encrypt = False; Trust Server Certificate = False; Application Intent = ReadWrite; Multi Subnet Failover = False");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.Orders)
                .WithOne(e =>e.User)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Seller>()
                .HasMany(e => e.Products)
                .WithOne(e => e.Seller)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
