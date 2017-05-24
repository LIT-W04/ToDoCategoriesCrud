using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication24.Models
{
    public class Order
    {
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Freight { get; set; }
        public string ShipName { get; set; }
    }
}