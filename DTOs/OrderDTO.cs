using System;
using System.Collections.Generic;
using SiC.Models;

namespace SiC.DTOs
{
    public class OrderDTO
    {

        public int OrderId { get; set; }

        public string OrderName { get; set; }

        public DateTime date { get; set; }

        public string address { get; set; }

        public double cost { get; set; }

        public string status { get; set; }

        public List<ProductDTO> orderItems { get; set; }

        public OrderDTO()
        {

        }
    }
}