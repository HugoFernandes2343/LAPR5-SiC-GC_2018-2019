using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiC.Models
{
    public class Restriction
    {
        public int RestrictionId { get; set; }
        public virtual Combination combination { get; set; }

    }
}