using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Myuken.Models
{
    public class DailyOrderCounter
    {
        [Key]
        public DateTime DatedRecord { get; set; }
        public int OrdersInADay { get; set; }
        public List<Item> ProductsPurchased { get; set; }
        public decimal DailySales { get; set; }
    }
}