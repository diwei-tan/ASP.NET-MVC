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
    public class LoginController : Controller
    {
        public ActionResult Login(string Username, string Password)
        {
            //if brought to this page without username or password input, login.
            if (Username == null)
            {
                ViewData["WrongPassword"] = false;
                return View();
            }
            //if not, check if password is correct. If yes, succssfully login and create a session for customer
            else
            {
                //If password don't match username in database, incorrect, throw to login again.
                if (!Util.PasswordIsRight(Username, Password))
                {
                    ViewData["WrongPassword"] = true;
                    return View();
                }
                //if successfully login, create sessionId for customer and send to shop. Using Session Object, do not need to pass Session around.
                else
                {
                    //Creat and store Session in database and Session Object, for checking purposes
                    string SessionId = Util.CreateSession(Username);
                    //In new session, create a temporary cart for that session to use, mapped by SessionId
                    Session[SessionId] = new TempCart();
                    return RedirectToAction("Gallery", "Shop", new { SessionId });
                }
            }
        }
        public ActionResult Logout(string Username, string SessionId)
        {
            if (!Util.SessionExist(SessionId))
            {
                return RedirectToAction("Login");
            }
            
            //Remove cart and sessionId in database
            Session[SessionId] = null;
            using (var db=new ShoppingCartDbContext())
            {
                Customer customer = db.Customer.Where(x => x.SessionId == SessionId).FirstOrDefault();
                customer.SessionId = null;
                db.SaveChanges();
            }
            return RedirectToAction("Login");
        }
    }
}