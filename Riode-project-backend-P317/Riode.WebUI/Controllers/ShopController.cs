using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.Models.DataContexts;
using Riode.WebUI.Models.FormModels;
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
            .Include(p => p.Images.Where(i => i.IsMain == true))
            .Include(c => c.Brand)
            .Where(b => b.DeletedByUserId == null)
            .ToList();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Filter(ShopFilterFormModel model)
        {

            var query = db.Products
            .Include(p => p.Images.Where(i => i.IsMain == true))
            .Include(c => c.Brand)
            .Include(c => c.ProductSizeColorCollection)
            .Where(b => b.DeletedByUserId == null)
            .AsQueryable();

            if (model?.Brands?.Count() > 0)
            {
                query = query.Where(p => model.Brands.Contains(p.BrandId));
            }

            if (model?.Sizes?.Count() > 0)
            {
                query = query
                    .Where(p =>
                    p.ProductSizeColorCollection
                    .Any(pscc => model.Sizes.Contains(pscc.SizeId)));
            }

            if (model?.Colors?.Count() > 0)
            {
                query = query
                    .Where(p =>
                    p.ProductSizeColorCollection
                    .Any(pscc => model.Colors.Contains(pscc.ColorId)));
            }


            return PartialView("_ProductContainer", query.ToList());
            //return Json(new
            //{
            //    error = false,
            //    data = query.ToList()
            //});
        }

        public IActionResult Details(int id)
        {
            var product = db.Products
                    .Include(p => p.Images)
                    .Include(p => p.SpecificationValues.Where(s=>s.DeletedByUserId == null))
                    .ThenInclude(s=>s.Specification)
                    .FirstOrDefault(p => p.Id == id && p.DeletedByUserId == null);


            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
