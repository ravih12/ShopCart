using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopCart.Models
{
    public class DisplayOrderDetailsVM
    {
        public List<CartDetailsViewModel> CDVM { get; set; }
        public ShippingViewModel SVM { get; set; }

    }
}