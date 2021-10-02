namespace Riode.WebUI.Models.Entities
{
    public class SpecificationValue : BaseEntity
    {
        public int SpecificationId { get; set; }
        public virtual Specification Specification { get; set; }
        public int ProductId { get; set; }
        public virtual Products Product { get; set; }
        public string Value { get; set; }
    }
}
