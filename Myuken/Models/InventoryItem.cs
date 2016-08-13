using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Myuken.Models
{
    public class InventoryItem
    {
        [Key]
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int StockCount { get; set; }
        public int ReOrderCount { get; set; }
        public int UnitPrice { get; set; }
    }
}