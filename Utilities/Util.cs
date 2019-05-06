using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoppingCart.Models;
using System.Security.Cryptography;
using System.Text;

namespace ShoppingCart.Utilities
{
    public class Util
    {
        //Check this session is valid
        public static bool SessionExist(string sessionId)
        {
            using (var db = new ShoppingCartDbContext())
            {
                List<Customer> customers = db.Customer.Where(x => x.SessionId.Contains(sessionId)).ToList();
                //Session exists if there is something in the list (its in database)
                if (customers.Count != 0)
                {
                    return true;
                }
                else
                    return false;
            }
        }
        public static bool PasswordIsRight(string Username, string password)
        {
            //Hash the password
            string hashedPassword = HashGenerator(password);

            using (var db = new ShoppingCartDbContext())
            {
                //fetch data of customers
                List<Customer> customers = db.Customer.Where(x => x.Username.Contains(Username)).ToList();
                //Check if there is Username, and check if password is same.
                //If match, return true.
                if (customers.Count == 1)
                {
                    if (customers.ElementAt(0).Password == hashedPassword)
                    {
                        return true;
                    }
                }
                //If exit from check, no matching username and password. Return false.
                return false;
            }
        }
        //Generate hashed password
        public static string HashGenerator(string password)
        {
            //HashedPassword using MD5 hash. Note: need to hash the byte of the password
            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(password);
            Byte[] encodedBytes = md5.ComputeHash(originalBytes);
            //return as a hash string
            return BitConverter.ToString(encodedBytes);
        }
        public static Product GetProductById(int Id)
        {
            using(var db = new ShoppingCartDbContext())
            {
                Product product = db.Product.Where(x => x.Id == Id).Single();
                return product;
            }
        }
        public static Customer GetCustomerBySessionId(string SessionId)
        {
            using (var db = new ShoppingCartDbContext())
            {
                Customer customer = db.Customer.Where(x => x.SessionId == SessionId).FirstOrDefault();
                return customer;
            }
        }
        
        //Create Session for customer and returns the sessionId to be used.
        //Note: Assumes Username exists (must login successfully first)
        public static string CreateSession(string Username)
        {
            //Acess database and enter session into database
            using (var db = new ShoppingCartDbContext())
            {
                //Add sessionId to customer table for customer who login
                string sessionId = Guid.NewGuid().ToString();
                Customer customer = db.Customer.Where(x => x.Username == Username).FirstOrDefault();
                customer.SessionId = sessionId;
                //save changes
                db.SaveChanges();

                return sessionId;
            }
        }
        public static List<Product> GetProducts()
        {
            using(var db = new ShoppingCartDbContext())
            {
                return db.Product.ToList();
            }
        }
        public static List<Product> GetProducts(string search)
        {
            using (var db = new ShoppingCartDbContext())
            {
                return db.Product.Where(x => x.Name.Contains(search)).ToList();
            }
        }
        public static string DateFromUnix(int unix)
        {
            string[] monthNames = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unix).ToLocalTime();

            return dateTime.Day + " " + monthNames[dateTime.Month - 1] + " " + dateTime.Year;
        }
        public Product getProductById(int Id)
        {
            using(var db = new ShoppingCartDbContext())
            {
                Product product = db.Product.Where(x => x.Id == Id).FirstOrDefault();
                return product;
            }
        }
    }
}