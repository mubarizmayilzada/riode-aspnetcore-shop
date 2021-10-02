using System;

namespace Riode.WebUI.Models.Entities
{
    public class Subscribe : BaseEntity
    {
        public string Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        public DateTime? EmailConfirmedDate { get; set; }
    }
}
