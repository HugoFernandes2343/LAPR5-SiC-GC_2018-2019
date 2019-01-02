using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiC.Models
{
    public class Order
    {

        public int OrderId { get; set; }

        public string OrderName { get; set; }

        public DateTime date { get; set;}

        public string address { get; set; }

        public string status { get; set; }

        public double cost { get; set;}

        public List<Product> orderItems { get; set; }

    }
}