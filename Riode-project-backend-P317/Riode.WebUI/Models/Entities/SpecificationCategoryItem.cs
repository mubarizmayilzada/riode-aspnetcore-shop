using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Models.Entities
{
    public class SpecificationCategoryItem : BaseEntity
    {
        public int SpecificationId { get; set; }
        public virtual Specification Specification { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
