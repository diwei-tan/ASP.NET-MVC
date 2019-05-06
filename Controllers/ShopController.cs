using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
using ShoppingCart.Models;
using System.Data.Entity;
using ShoppingCart.Utilities;

namespace ShoppingCart.Controllers
{
    public class ShopController : Controller
    {

        public ActionResult Gallery(string SessionId, string Id, string Search)
        { 
            //Check whether sessionexist. If not, throw user back
            if (!Util.SessionExist(SessionId))
            {
                return RedirectToAction("Login", "Login");
            }
            
            //if there is Id, User requested add the product into the cart
            if (Id != null)
            {
                // add to shopping cart
                TempCart Cart = (TempCart)Session[SessionId];
                Product product = Util.GetProductById(int.Parse(Id));
                CartItem item = product.ConvertToCartItem();
                Cart.Add(item);
                Session[SessionId] = Cart;
            }

            //get products dependant on search
            List<Product> products = new List<Product>();
            if (Search == null || Search == "")
                products = Util.GetProducts();
            else
                products = Util.GetProducts(Search);

            //pass to view for view use
            ViewData["Products"] = products;
            ViewData["SessionId"] = SessionId;
            ViewData["Username"] = Util.GetCustomerBySessionId(SessionId).Username;
            ViewData["Search"] = Search;
            return View();
        }

        public ActionResult Cart(string SessionId, string Id, string cmd)
        {
            if (!Util.SessionExist(SessionId))
            {
                return RedirectToAction("Login", "Login");
            }

            if (Id != null)
            {
                Product product = Util.GetProductById(int.Parse(Id));
                CartItem item = product.ConvertToCartItem();
                TempCart Cart = (TempCart)Session[SessionId];
                if (cmd == "minus")
                {
                    Cart.Minus(item);
                }
                else if (cmd == "plus")
                {
                    Cart.Add(item);
                }
                Session[SessionId] = Cart;
            }
            ViewData["SessionId"] = SessionId;
            return View();
        }
        public ActionResult Checkout(string SessionId)
        {
            if (!Util.SessionExist(SessionId))
            {
                return RedirectToAction("Login", "Login");
            }

            //get current time
            int curUnix = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            Customer customer = Util.GetCustomerBySessionId(SessionId);

            //For each item in the cart, add to customer purchased under the user
            TempCart Cart = (TempCart)Session[SessionId];
            using (var db = new ShoppingCartDbContext())
            {
                foreach (CartItem item in Cart.Items)
                {                    
                    for (int i = 0; i < item.Quantity; i++)
                    {
                        CustomerPurchase purchase = new CustomerPurchase();
                        purchase.CustomerId = customer.Id;
                        purchase.ProductId = item.Id;
                        purchase.DatePurchased = curUnix;
                        purchase.ActivationCode = Guid.NewGuid().ToString();
                        db.CustomerPurchase.Add(purchase);
                    }
                }
                db.SaveChanges();
            }

            //Cart has been checked out, remove old cart, reset cart and redirect to view purchase
            Session[SessionId] = new TempCart();
            return RedirectToAction("MyPurchase", "ViewPurchase", new { SessionId });
        }
    }
}