using Myuken.Controllers;
using Myuken.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myuken.Logic
{
    public class CatalogHead
    {
       readonly ApplicationDbContext db = new ApplicationDbContext();
        public List<Product> AllowedProducts()
        {
            List<Product> ap = new List<Product>();
            foreach (Product item in db.Products.ToList())
            {
                var result = db.InventoryItems.ToList().Find(p => p.ProductId == item.ProductId);
                if (result!=null)
                {
                    if(result.StockCount>25)
                    {
                        ap.Add(item);
                    }
                }
            }
            return ap;
        }

        public void AddSales()
        {
            DailyOrderCounter doc = new DailyOrderCounter();
            doc.DatedRecord = DateTime.Now;
            doc.OrdersInADay = db.Orders.Count();
            doc.ProductsPurchased = db.Items.ToList();
            foreach (Order item in db.Orders.ToList())
            {
                doc.DailySales += item.Total;
            }
            db.DailyOrderCounters.Add(doc);
            db.SaveChanges();
            HomeController asd = new HomeController();
            asd.Index();
        }
    }
}