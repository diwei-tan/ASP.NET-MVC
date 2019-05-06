using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoppingCart.Models;

namespace ShoppingCart.Models
{
    public class TempCart
    {
        public string Username { get; set; }
        public int DateOfPurchaseInUnix { get; set; }

        public List<CartItem> Items = new List<CartItem>();

        //Look through list to see if item added. If already exist, increase quantity by 1. If not yet exist, add cartitem to list. Assume when add to cart is press, we are adding one quantity of item to cart only.
        public void Add(CartItem item)
        {
            //Checker for whether a similar item is found in cart. Initiated as false
            bool InCart = false;

            //find all cartitems already added
            var AddedItems = from Added in Items
                       select Added;
            //if similiar to any item already in then add one to quantity. If not, then add item to cart with quantity 1.
            foreach(CartItem Item in AddedItems)
            {
                //If the item exist in Cart already, increase Quantity and notify that the product was in cart
                if (Item.Id == item.Id)
                {
                    Item.Quantity++;
                    InCart = true;
                }
            }
            //If the product was not found in cart during above step, add the product to cart
            if (InCart == false)
            {
                Items.Add(item);
            }
        }
        //Assume that item must exist in Cart as it is shown in ViewCart. Remove one quantity and check whether quantity is 0.
        public void Minus(CartItem item)
        {
            //find all cartitems already added
            var AddedItems = from Added in Items
                             select Added;
            //Find Item that match and minus the quantity
            foreach (CartItem Item in AddedItems)
            {
                if (Item.Id == item.Id)
                {
                    Item.Quantity--;
                }
            }
            //If the product quantity becomes zero, remove it from the list
            Items.RemoveAll(x => x.Quantity <= 0);
        }

    }
}