using Myuken.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myuken.Controllers
{
    public class CatalogController : Controller
    {
        // GET: Catalog
        public ActionResult Index()
        {
            CatalogHead ch = new CatalogHead();
            ViewBag.Products = ch.AllowedProducts();
            return View();
        }
        public ActionResult Buy(int id)
        {
            CartController cc = new CartController();
            //return (cc.Buy(id));
            return RedirectToAction("Buy", "Cart", new { Id = id });
        }
    }
}