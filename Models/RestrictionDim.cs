using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiC.Models
{
    public class RestrictionDim : Restriction
    {

        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

    }
}