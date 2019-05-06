using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="*Please input Username")]
        public string Username { get; set; }
        [Required(ErrorMessage ="*Please input Password")]
        public string Password { get; set; }
        public string SessionId { get; set; }
        public virtual ICollection<CustomerPurchase> CustomerPurchases { get; set; }

        public string HashPassword(string password)
        {
            //HashedPassword using MD5 hash. Note: need to hash the byte of the password
            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(password);
            Byte[] encodedBytes = md5.ComputeHash(originalBytes);
            //return as a hash string
            return BitConverter.ToString(encodedBytes);
        }
    }
    
}