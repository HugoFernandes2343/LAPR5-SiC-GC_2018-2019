using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiC.Models
{
    public class Price
    {

        public int PriceId { get; set; }

        public string designation { get; set; }

        public double price { get; set; }

        public DateTime date { get; set;}
    }
}