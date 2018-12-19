using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiC.DTO
{
    public class PostProductDTO
    {

        public PostProductDTO()
        {
            ParentId = -1;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double? MaxOccupation { get; set; }
        public double? MinOccupation { get; set; }
        public long DimensionId { get; set; }
        public long CategoryId { get; set; }
        public long RestrictionId { get; set; }
        public long? ParentId { get; set; }
    }
}