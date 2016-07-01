using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinotexPayDemo.Models
{
    public class OrderModel
    {
        public string OrderName { get; set; }
        public string OrderNumber { get; set; }
        public Decimal Price { get; set; }
        public int GoodsCount { get; set; }
    }
}