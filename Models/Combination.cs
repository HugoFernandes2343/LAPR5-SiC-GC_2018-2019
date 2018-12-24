using System;

namespace SiC.Models
{
    public class Combination
    {
        public int CombinationId { get; set; }

        public virtual Product containingProduct { get; set; }

        public virtual Product containedProduct { get; set; }

        public bool required { get; set; }

        public Combination(Product containingProduct, Product containedProduct, bool required)
        {
            if (!fit(containingProduct, containedProduct))
            {
                throw new ArgumentException();
            }
            this.containedProduct = containedProduct;
            this.containingProduct = containingProduct;
            this.required=required;
        }

        public Boolean fit(Product containing, Product contained)
        {
            foreach (Dimension dim in containing.dimensions)
            {
                foreach (Dimension dim1 in contained.dimensions)
                {
                    if (dim1.Depth.Value < dim.Depth.Value && dim1.Height.Value < dim.Height.Value && dim1.Width.Value < dim.Width.Value)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Combination()
        {


        }

    }
}