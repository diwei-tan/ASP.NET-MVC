using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ShoppingCart.Models;
using ShoppingCart.Utilities;

namespace ShoppingCart.Models
{
    public class ShoppingCartDbContext : DbContext
    {
        public ShoppingCartDbContext() : base("Server=LAPTOP-4CBSFNAO; Database=shoppingcartDB; Integrated Security = True")
        {
            Database.SetInitializer(new ShoppingCartDbInitializer<ShoppingCartDbContext>());
        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<CustomerPurchase> CustomerPurchase { get; set; }

    }

    //Drops and creates new database EVERYTIME for testing convinience
    public class ShoppingCartDbInitializer<T> :
        DropCreateDatabaseAlways<ShoppingCartDbContext>
    {
        protected override void Seed(ShoppingCartDbContext context)
        {
            //Create 4 users
            Customer c1 = new Customer();
            c1.Username = "diwei";
            c1.Password = Util.HashGenerator("diwei");
            context.Customer.Add(c1);
            Customer c2 = new Customer();
            c2.Username = "annie";
            c2.Password = Util.HashGenerator("annie");
            context.Customer.Add(c2);
            Customer c3 = new Customer();
            c3.Username = "siqi";
            c3.Password = Util.HashGenerator("siqi");
            context.Customer.Add(c3);
            Customer c4 = new Customer();
            c4.Username = "denghan";
            c4.Password = Util.HashGenerator("denghan");
            context.Customer.Add(c4);

            //Create Products as shown in the CA requirements
            Product p1 = new Product();
            p1.Name = ".NET Charts";
            p1.Price = 99;
            p1.Description = "Brings powerful charing capabilities to your .Net applications.";
            p1.ImageUrl = "/images/1.png";
            context.Product.Add(p1);
            Product p2 = new Product();
            p2.Name = ".NET PayPal";
            p2.Price = 69;
            p2.Description = "Integrate your .NET apps with PayPal the easy way!";
            p2.ImageUrl = "/images/2.png";
            context.Product.Add(p2);
            Product p3 = new Product();
            p3.Name = ".NET ML";
            p3.Price = 299;
            p3.Description = "Supercharged .NET machine learning libraries";
            p3.ImageUrl = "/images/3.png";
            context.Product.Add(p3);
            Product p4 = new Product();
            p4.Name = ".NET Analytics";
            p4.Price = 299;
            p4.Description = "Performs data mining and analytics easily in .NET";
            p4.ImageUrl = "/images/4.png";
            context.Product.Add(p4);
            Product p5 = new Product();
            p5.Name = ".NET Logger";
            p5.Price = 49;
            p5.Description = "Logs and aggregates events easily in your .NET apps";
            p5.ImageUrl = "/images/5.png";
            context.Product.Add(p5);
            Product p6 = new Product();
            p6.Name = ".NET Numerics";
            p6.Price = 199;
            p6.Description = "Powerful numerical methods for your .NET simulations";
            p6.ImageUrl = "/images/6.png";
            context.Product.Add(p6);
            Product p7 = new Product();
            p7.Name = ".NET Visa";
            p7.Price = 299;
            p7.Description = "Integrate visa payment to your .NET apps for ease of payment!";
            p7.ImageUrl = "/images/7.png";
            context.Product.Add(p7);
            Product p8 = new Product();
            p8.Name = ".NET Cloud";
            p8.Price = 99;
            p8.Description = "Upgrade your applications with cloud capabilities smoothly in .NET";
            p8.ImageUrl = "/images/8.png";
            context.Product.Add(p8);
            Product p9 = new Product();
            p9.Name = ".Net Python";
            p9.Price = 49;
            p9.Description = "Choose to code .NET in Python!";
            p9.ImageUrl = "/images/9.png";
            context.Product.Add(p9);

            //Create CustomerPurchases for 1st user to test
            CustomerPurchase cp1 = new CustomerPurchase();
            cp1.Id = 1;
            cp1.Customer = c1;
            cp1.ActivationCode = Guid.NewGuid().ToString();
            cp1.DatePurchased = 1546344000;
            cp1.Product = p1;
            context.CustomerPurchase.Add(cp1);
            CustomerPurchase cp2 = new CustomerPurchase();
            cp2.Id = 2;
            cp2.Customer = c1;
            cp2.ActivationCode = Guid.NewGuid().ToString();
            cp2.DatePurchased = 1546344000;
            cp2.Product = p1;
            context.CustomerPurchase.Add(cp2);
            CustomerPurchase cp3 = new CustomerPurchase();
            cp3.Id = 3;
            cp3.Customer = c1;
            cp3.ActivationCode = Guid.NewGuid().ToString();
            cp3.DatePurchased = 1546344000;
            cp3.Product = p2;
            context.CustomerPurchase.Add(cp3);


            base.Seed(context);
        }
    }
}