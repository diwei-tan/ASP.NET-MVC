using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class MyPurchaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DatePurchased { get; set; }
        public string ActivationCode { get; set; }
        public string ImageUrl { get; set; }
    }
}