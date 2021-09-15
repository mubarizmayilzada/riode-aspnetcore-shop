using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.Models.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Controllers
{
    public class ProductsController : Controller
    {

        public IActionResult index()
        {
            return View();
        }

        public IActionResult details(int id)
        {
            var db = new RiodeDbContext();
            var product = db.Products
                .Include(products => products.Images)
                .Include(products => products.Brand)
                .Include(products => products.Category)
                .FirstOrDefault(b => b.Id == id && b.DeletedByUserId == null);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
