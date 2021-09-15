using Microsoft.EntityFrameworkCore;
using Riode.WebUI.Models.Entities;

namespace Riode.WebUI.Models.DataContexts
{
    public class RiodeDbContext : DbContext
    {
        public RiodeDbContext()
            : base()
        {

        }

        public RiodeDbContext(DbContextOptions options)
            : base(options)
        {

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
