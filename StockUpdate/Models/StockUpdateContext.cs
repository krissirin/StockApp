using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StockUpdate.Models
{
    //The scaffolding process use Entity Framework Code First to generate the data context 
    //and the database schema in the SQL database on this project

    //DbContext generally represents a database connection and a set of tables. 
    public class StockUpdateContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public StockUpdateContext() : base("name=StockUpdateContext")
        {
        }
        //DbSet is used to represent a table.
        public System.Data.Entity.DbSet<StockUpdate.Models.Stock> Stocks { get; set; }
    }
}