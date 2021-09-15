using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Models.Entities
{
    public class ProductImage : BaseEntity
    {
        public string FileName { get; set; }
        public bool IsMain { get; set; }
        public int ProductId { get; set; }
        public virtual Products Product { get; set; }
    }
}