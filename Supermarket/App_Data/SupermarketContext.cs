using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Supermarket.Models
{
    public class SupermarketContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        //
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        //public SupermarketContext() : base("name=SupermarketContext")
        //{
        //}

        // public System.Data.Entity.DbSet<Supermarket.Models.Product> Products { get; set; }

        public SupermarketContext()
        {
            Products = new List<Product>();
        }

        public IList<Supermarket.Models.Product> Products { get; set; }
    }
}