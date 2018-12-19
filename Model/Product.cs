using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiC.Model
{
    public class Product
    {

        public Product()
        {
            Components = new HashSet<Product>();
            Materials = new List<Material>();
            MinOccupation = 0;
            MaxOccupation = 100;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double MaxOccupation { get; set; }
        public double MinOccupation { get; set; }
        public double? Occupation { get; set; }

        public List<Material> Materials { get; set; }

        public long DimensionId { get; set; }
        public Dimension Dimension { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }

        public long? RestrictionId { get; set; }
        public Restriction Restriction { get; set; }

        public long? ParentId { get; set; }
        public virtual Product ParentProduct { get; set; }
        public virtual ICollection<Product> Components { get; set; }
    }
}