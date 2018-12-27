using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopCart.Models
{
    public class CartViewModel
    {
        public int ProductId { get; set; }
        //public string CompanyName { get; set; }
        //public int Price { get; set; }
        //public string Description { get; set; }
        //public string Image { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}