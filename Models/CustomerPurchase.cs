using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class CustomerPurchase
    {
        public int Id { get; set; }
        public int DatePurchased { get; set; }
        public string ActivationCode { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}