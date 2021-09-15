//using Riode.WebUI.Migrations;
using System.Collections.Generic;

namespace Riode.WebUI.Models.Entities
{

    public class Products : BaseEntity
    {
        public string Name { get; set; }
        public string SkuCode { get; set; }
        public int BrandId { get; set; }
        public virtual Brands Brand { get; set; }  //migrationun adini brand vermisen ona gore qarishdirib gedib migrationu getirib
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ProductImage> Images { get; set; }
    }
}
