using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System;

namespace SiC.Models
{
    public class Measure
    {

        public Measure(double Value)
        {
            this.Value = Value;
            this.ValueMax = 0;
            this.isDiscrete = true;
        }

        public Measure(double Value, double ValueMax)
        {
            this.Value = Value;
            this.ValueMax = ValueMax;
            this.isDiscrete = false;
        }

        public Measure()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MeasureId { get; set; }

        public double Value { get; set; }

        public double ValueMax { get; set; }

        public Boolean isDiscrete { get; set; }
    }
}