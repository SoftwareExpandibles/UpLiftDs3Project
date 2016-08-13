using Myuken.Logic;
using Myuken.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myuken.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult AddSales()
        {
            ApplicationDbContext adb = new ApplicationDbContext();
            ViewBag.Sales = adb.DailyOrderCounters.ToList();
            ViewBag.Items = adb.InventoryItems.ToList();
            return View();
            //return Content("Blues");
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}