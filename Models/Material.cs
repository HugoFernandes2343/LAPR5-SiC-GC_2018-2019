using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiC.Models
{
    public class Material
    {

        public int MaterialId { get; set; }
        public string name { get; set; }
        public string description { get; set;}

        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }

        public virtual ICollection<MaterialFinishing> MaterialFinishings { get; set; }

    }
}