using System;

namespace SiC.Models
{
    public class Dimension
    {

        public int DimensionId { get; set; }
        public virtual Measure Width { get; set; }
        public virtual Measure Height { get; set; }
        public virtual Measure Depth { get; set; }

        public Dimension()
        {

        }
      
    }
}