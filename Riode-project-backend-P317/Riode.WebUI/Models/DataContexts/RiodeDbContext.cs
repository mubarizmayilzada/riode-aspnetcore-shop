using Microsoft.EntityFrameworkCore;
using Riode.WebUI.Models.Entities;

namespace Riode.WebUI.Models.DataContexts
{
    public class RiodeDbContext : DbContext
    {
        public RiodeDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ContactPost> ContactPosts { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<Sizes> Sizes { get; set; }
        public DbSet<Colors> Colors { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductSizeColorItem> ProductSizeColorCollection { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<SpecificationCategoryItem> SpecificationCategoryCollection { get; set; }
        public DbSet<SpecificationValue> SpecificationValues { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }

    }
}
