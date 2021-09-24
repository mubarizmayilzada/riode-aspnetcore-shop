//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Riode.WebUI.Models.DataContexts;
//using Riode.WebUI.Models.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Riode.WebUI.Controllers
//{
//    public class CategoryController : Controller
//    {
//        public IActionResult details()
//        {
//            var viewModel = new CategoryViewModel();

//            var db = new RiodeDbContext();

//            viewModel.Categories = db.Categories
//                .Include(c => c.Parent)
//                .Include(c => c.Children)
//                .ThenInclude(c => c.Children)
//                .ThenInclude(c => c.Children)
//                .Where(c => c.ParentId == null && c.DeletedByUserId == null)
//                .ToList();

//            viewModel.Brands = db.Brands
//                .Where(b => b.DeletedByUserId == null)
//                .ToList();

//            viewModel.Sizes = db.Sizes
//                .Where(b => b.DeletedByUserId == null)
//                .ToList();

//            viewModel.Colors = db.Colors
//                .Where(b => b.DeletedByUserId == null)
//                .ToList();

//            viewModel.Products = db.Products
//                .Include(p =>p.Images)
//                .Where(b => b.DeletedByUserId == null)
//                .ToList();

//            return View(viewModel);
//        }
//    }
//}
