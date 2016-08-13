using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Myuken.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string OrderTitle { get; set; }
        public List<Item> CartItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Vat { get; set; }
        public decimal Total { get; set; }
        public bool Status { get; set; }
        public Order()
        {
            CartItems = new List<Item>();
        }
    }
}