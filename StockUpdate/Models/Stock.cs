// Stock model class

using System;
using System.ComponentModel.DataAnnotations;


namespace StockUpdate.Models
{
    // an order for stock, with data annotations
    public class Stock
    {
        // all value types are impicility required
        [Key] 
        public int StockReference { get; set; }  //PK
        [StringLength(20, MinimumLength = 1)]    //min 1 to max 20 chars

        public String Ticker { get; set; }
        [StringLength(200, MinimumLength = 1)]   //min 1 to max 200 chars


        // required : not empty string or white spance
        [Required(ErrorMessage = "Stock name must not be blank!")]
        public string StockName { get; set; }

        [DataType(DataType.Currency)]           //display $ 
        public double Price { get; set; }
    }
}