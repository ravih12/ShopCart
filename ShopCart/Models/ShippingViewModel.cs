using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopCart.Models
{
    public class ShippingViewModel
    {
        [Display(Name="Customer Name")]
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string  Mobile{ get; set; }
        
       

    }
}