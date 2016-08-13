using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myuken.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Amount()
        {
            return (decimal)(Product.SellingPrice * Quantity);
        }
        public virtual Order Order { get; set; }
    }
}