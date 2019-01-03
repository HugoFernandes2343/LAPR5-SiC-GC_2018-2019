using SiC.Models;

namespace SiC.Models
{
    public class CatalogProduct
    {
        public int CatalogId { get; set; }
        public virtual Catalog Catalog { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}