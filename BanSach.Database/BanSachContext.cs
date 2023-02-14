using BanSach.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanSach.Database
{
    public class BanSachContext : DbContext, IDisposable
    {
        public BanSachContext() : base("BanSachConnection")
        {

        }
        public DbSet<Config> Configurations { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
