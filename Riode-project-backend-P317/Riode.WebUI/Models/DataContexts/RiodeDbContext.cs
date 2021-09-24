using Microsoft.EntityFrameworkCore;
using Riode.WebUI.Models.Entities;
using System;

namespace Riode.WebUI.Models.DataContexts
{
    public class RiodeDbContext : DbContext
    {
        public Guid InstanceNumber { get; private set; }

        public RiodeDbContext(DbContextOptions options)
            : base(options)
        {
            InstanceNumber = Guid.NewGuid();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS; Initial Catalog=Riode;User Id=sa;Password=query");
            }
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<Sizes> Sizes { get; set; }
        public DbSet<Colors> Colors { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

    }
}
