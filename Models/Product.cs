using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiC.Models
{
    public class Product
    {

        public int ProductId { get; set; }

        public string name { get; set; }
        public string description { get; set; }
        public virtual List<Dimension> dimensions { get; set; }
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }
        public virtual Category category { get; set; }

    }
}