using System;

namespace SiC.DTOs
{
    public class MeasureDTO
    {
        public int Id { get; set; }

        public double Value { get; set; }

        public double ValueMax { get; set; }

        public Boolean isDiscrete { get; set; }
    }
}