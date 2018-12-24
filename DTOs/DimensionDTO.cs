using System;
using SiC.Models;

namespace SiC.DTOs
{
    public class DimensionDTO
    {


        public int DimensionId { get; set; }

        public MeasureDTO Width { get; set; }

        public MeasureDTO Height { get; set; }

        public virtual MeasureDTO Depth { get; set; }

        public DimensionDTO()
        {

        }

    }
}