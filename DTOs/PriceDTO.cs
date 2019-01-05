using System;
using System.Collections.Generic;
using SiC.Models;

namespace SiC.DTOs
{
    public class PriceDTO
    {

        public int PriceId { get; set; }

        public string designation { get; set; }

        public double price { get; set; }

        public string date { get; set;}

        public PriceDTO()
        {

        }
    }
}