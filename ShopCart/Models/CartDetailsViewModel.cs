using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopCart.Models
{
    public class CartDetailsViewModel
    {
       
            public int Id { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string Description { get; set; }
            public string ImagePath { get; set; }
            public int Price { get; set; }
            public int Quantity { get; set; }
            public int TotalPrice { get; set; }
        

    }
}