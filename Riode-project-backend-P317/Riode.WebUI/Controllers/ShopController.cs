using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.Models.DataContexts;
using Riode.WebUI.Models.ViewModels;
using System.Linq;

namespace Riode.WebUI.Controllers
{
    public class ShopController : Controller
    {
        readonly RiodeDbContext db;
        public ShopController(RiodeDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {



            var viewModel = new CategoryViewModel();

            viewModel.Categories = db.Categories
            .Include(c => c.Parent)
            .Include(c => c.Children)
            .ThenInclude(c => c.Children)
            .ThenInclude(c => c.Children)
            .Where(c => c.ParentId == null && c.DeletedByUserId == null)
            .ToList();

            viewModel.Brands = db.Brands
                .Where(b => b.DeletedByUserId == null)
                .ToList();

            viewModel.Sizes = db.Sizes
                .Where(b => b.DeletedByUserId == null)
                .ToList();

            viewModel.Colors = db.Colors
                .Where(b => b.DeletedByUserId == null)
                .ToList();

            viewModel.Products = db.Products
            .Include(p => p.Images.Where(i=>i.IsMain == true))
            .Include(c=>c.Brand)
            .Where(b => b.DeletedByUserId == null)
            .ToList();

            return View(viewModel);
        }

        public IActionResult Details(int id)
        {
            var product = db.Products
                    .Include(p => p.Images)
                    .FirstOrDefault(p => p.Id == id && p.DeletedByUserId == null);


            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
