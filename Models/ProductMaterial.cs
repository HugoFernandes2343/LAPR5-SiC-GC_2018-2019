namespace SiC.Models
{
    public class ProductMaterial
    {
        public int MaterialId { get; set; }
        public virtual Material Material { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}