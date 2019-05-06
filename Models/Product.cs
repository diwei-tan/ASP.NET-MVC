using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        //Converts itself to one quantity of an item that can be added to cart
        public CartItem ConvertToCartItem()
        {
            CartItem item = new CartItem();
            item.Id = Id;
            item.ImageUrl = ImageUrl;
            item.Description = Description;
            item.Name = Name;
            item.Price = Price;
            item.Quantity = 1;
            return item;
        }
    }
}