using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoppingCart.Models;

namespace ShoppingCart.Models
{
    public class CartItem : Product
    {
        public int Quantity {get;set;}
    }
}