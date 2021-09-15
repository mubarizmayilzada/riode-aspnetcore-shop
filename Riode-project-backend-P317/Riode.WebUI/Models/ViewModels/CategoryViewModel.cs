using Riode.WebUI.Migrations;
using Riode.WebUI.Models.Entities;
using System.Collections.Generic;

namespace Riode.WebUI.Models.ViewModels
{
    public class CategoryViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Brands> Brands { get; set; }
        public List<Sizes> Sizes { get; set; }
        public List<Colors> Colors { get; set; }
        public List<Products> Products { get; set; }
    }
}
