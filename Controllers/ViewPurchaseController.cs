using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ShoppingCart.Models;
using ShoppingCart.Utilities;

namespace ShoppingCart.Controllers
{
    public class ViewPurchaseController : Controller
    {
        // GET: ViewPurchase
        public ActionResult MyPurchase(string SessionId)
        {
            //Check for session. if does not exist,redirect back to login screen
            if (!Util.SessionExist(SessionId))
            {
                return RedirectToAction("Login", "Login");
            }

            List<MyPurchaseModel> listofpurchase = new List<MyPurchaseModel>();

            using (var db = new ShoppingCartDbContext())
            {
                var purchases = (from customerpurchase in db.CustomerPurchase
                                 join customer in db.Customer
                                 on customerpurchase.CustomerId equals customer.Id
                                 join product in db.Product
                                 on customerpurchase.ProductId equals product.Id
                                 where customer.SessionId == SessionId
                                 orderby customerpurchase.DatePurchased descending, product.Name ascending 
                                 select new
                                 {
                                     product.ImageUrl,
                                     product.Name,
                                     product.Description,
                                     customerpurchase.DatePurchased,
                                     customerpurchase.ActivationCode
                                 }).ToList();
                foreach (var purchase in purchases)
                {
                    MyPurchaseModel p = new MyPurchaseModel();
                    p.ImageUrl = purchase.ImageUrl;
                    p.Name = purchase.Name;
                    p.Description = purchase.Description;
                    p.DatePurchased = purchase.DatePurchased;
                    p.ActivationCode = purchase.ActivationCode;
                    listofpurchase.Add(p);
                }
            }

            ViewData["CustomerPurchase"] = listofpurchase;
            ViewData["SessionId"] = SessionId;

            return View();
        }
    }
}