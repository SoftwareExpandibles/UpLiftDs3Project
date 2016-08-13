using Myuken.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myuken.Controllers
{
    public class CartController : Controller
    {
          readonly  ApplicationDbContext db = new ApplicationDbContext();
        //[Authorize]
        //
        // GET: /Cart/
        public ActionResult Index()
        {
            return View();
        }
        private int isExisting(int id)
        {
            List<Item> cart = (List<Item>)Session["Cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.ProductId.Equals(id))
                    return i;
            return -1;
        }
        public ActionResult Buy(int id)
        {
            try
            {
                if (Session["Cart"] != null)
                {
                    List<Item> cart = (List<Item>)Session["Cart"];
                    int index = isExisting(id);
                    if (index != -1)
                    {
                        cart[index].Quantity++;
                    }
                    if (index == -1)
                    {
                        cart.Add(new Item { ItemId = (cart.Count + 1), Product = db.Products.Find(id), Quantity = 1 });
                    }
                }

                if (Session["Cart"] == null)
                {
                    List<Item> cart = new List<Item>();
                    cart.Add(new Item { ItemId = 1, Product = db.Products.Find(id), Quantity = 1 });
                    Session["Cart"] = cart;
                }
            }
            catch
            {
                return View("Cart");
            }
            return View("cart");
        }
        public ActionResult CheckOut()
        {
            Order ooo = new Order();
            List<Item> cart = (List<Item>)Session["Cart"];
            if (cart!=null)
            {
                foreach (Item cad in cart)
                {
                    ooo.SubTotal += (cad.Quantity * cad.Product.SellingPrice);
                }
            }
            var man = new Client();
            foreach (Client item in db.Clients.ToList())
            {
                if (db.Clients.Count() <= 1)
                {
                    man = item;
                }
            }
            ooo.OrderId = db.Orders.Count() + 1;
            ooo.CartItems = cart;
            ooo.OrderTitle = man.FullName.Substring(0, 3)+"Order"+ooo.OrderId.ToString();
            ooo.Vat = ooo.SubTotal *Convert.ToDecimal(0.14);
            ooo.Total = ooo.SubTotal + ooo.Vat;
            ooo.Status = false;
            db.Orders.Add(ooo);
            db.SaveChanges();
            Session["Order"] = ooo;
            return View("CheckOut");
        }
        public ActionResult Delete(int id)
        {
            List<Item> cart = (List<Item>)Session["Cart"];
            int index = BisExisting(id);
            cart.RemoveAt(index);
            if (cart.Count == 0)
            {
                cart = null;
            }
            Session["Cart"] = cart;
            return View("Cart");
        }

        private int BisExisting(int id)
        {
            List<Item> cart = (List<Item>)Session["Cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.ProductId == id)
                    return i;
            return -1;
        }

        public ActionResult Shipping()
        {

            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CheckOut([Bind(Include = "DeliveryTitle,PickUpDate,Status")] DeliveryOption deliveryoption)
        //{
        //    decimal sum = 0;
        //    int EndOfMonth = 0;
        //    deliveryoption.DeliveryTitle = "Pick Up Onsite";
        //    if (ModelState.IsValid)
        //    {
        //        if (deliveryoption.Status == true)
        //        {
        //            List<Item> carty = (List<Item>)this.Session["cart"];
        //            if (deliveryoption.PickUpDate.Month == DateTime.Now.Month)
        //            {
        //                decimal subsum = 0;
        //                subsum += (deliveryoption.PickUpDate.Day - DateTime.Now.Date.Day) * carty[0].Product.Price;
        //                //shouldnt be  here
        //                if (subsum < 0)
        //                {
        //                    subsum *= -1;
        //                    sum = 0;
        //                }
        //                //same day
        //                if (subsum == 0)
        //                {
        //                    sum += 350;
        //                }
        //                //not same date
        //                if (subsum > 0)
        //                {
        //                    //3 days R300
        //                    if (deliveryoption.PickUpDate.Day - DateTime.Now.Date.Day > 1 && deliveryoption.PickUpDate.Day - DateTime.Now.Date.Day < 5)
        //                    {
        //                        sum += 300;
        //                    }
        //                    //5 days R260
        //                    if (deliveryoption.PickUpDate.Day - DateTime.Now.Date.Day > 4 && deliveryoption.PickUpDate.Day - DateTime.Now.Date.Day < 7)
        //                    {
        //                        sum += 260;
        //                    }
        //                    //7 days R200
        //                    if (deliveryoption.PickUpDate.Day - DateTime.Now.Date.Day >= 7)
        //                    {
        //                        sum += 200;
        //                    }
        //                }

        //            }
        //            if (deliveryoption.PickUpDate.Month > DateTime.Now.Month)
        //            {
        //                switch (DateTime.Now.Month)
        //                {
        //                    case 1:
        //                        EndOfMonth += 31;
        //                        break;
        //                    case 2:
        //                        EndOfMonth += 28;
        //                        break;
        //                    case 3:
        //                        EndOfMonth += 31;
        //                        break;
        //                    case 4:
        //                        EndOfMonth += 30;
        //                        break;
        //                    case 5:
        //                        EndOfMonth += 31;
        //                        break;
        //                    case 6:
        //                        EndOfMonth += 30;
        //                        break;
        //                    case 7:
        //                        EndOfMonth += 31;
        //                        break;
        //                    case 8:
        //                        EndOfMonth += 31;
        //                        break;
        //                    case 9:
        //                        EndOfMonth += 30;
        //                        break;
        //                    case 10:
        //                        EndOfMonth += 31;
        //                        break;
        //                    case 11:
        //                        EndOfMonth += 30;
        //                        break;
        //                    case 12:
        //                        EndOfMonth += 31;
        //                        break;
        //                    default:
        //                        break;
        //                }
        //                sum += ((EndOfMonth - DateTime.Now.Date.Day) + deliveryoption.PickUpDate.Day) * carty[0].Product.Price;
        //                //3 days R300
        //                if ((EndOfMonth - DateTime.Now.Date.Day) + deliveryoption.PickUpDate.Day > 1 && (EndOfMonth - DateTime.Now.Date.Day) + deliveryoption.PickUpDate.Day < 5)
        //                {
        //                    sum += 300;
        //                }
        //                //5 days R260
        //                if ((EndOfMonth - DateTime.Now.Date.Day) + deliveryoption.PickUpDate.Day > 4 && (EndOfMonth - DateTime.Now.Date.Day) + deliveryoption.PickUpDate.Day < 7)
        //                {
        //                    sum += 260;
        //                }
        //                //7 days R200
        //                if ((EndOfMonth - DateTime.Now.Date.Day) + deliveryoption.PickUpDate.Day >= 7)
        //                {
        //                    sum += 200;
        //                }
        //            }
        //            Session["deliveryCharges"] = sum;
        //            deliveryoption.DeliveryTitle = "Deliver To Your Address";
        //            Session["deliveryOption"] = deliveryoption;
        //            return RedirectToAction("CaptureAddress");
        //        }
        //        else
        //        {
        //            Session["deliveryOption"] = deliveryoption;
        //            return RedirectToAction("GenerateOrderLayout");
        //        }


        //    }

        //    return View("error");
        //}
        //public ActionResult CaptureAddress()
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();
        //    Order newOrder = new Order();
        //    OrderDetail od = new OrderDetail();
        //    DeliveryOption ed = (DeliveryOption)Session["deliveryOption"];
        //    List<Item> carty = (List<Item>)this.Session["cart"];
        //    if (carty != null)
        //    {
        //        newOrder.OrderDate = DateTime.Now;
        //        newOrder.OrderId = (db.Orders.Count()) + 1;
        //        newOrder.UserName = User.Identity.Name;
        //        od.OrderId = newOrder.OrderId;
        //        od.Items = carty;
        //        decimal delcharge = (decimal)Session["deliveryCharges"];
        //        decimal sum = 0;
        //        if ((od.Items.Count) > 0)
        //        {
        //            foreach (Item nm in od.Items)
        //            {
        //                sum += nm.Product.Price * nm.Quantity;
        //            }
        //        }
        //        newOrder.Total = sum + delcharge;
        //    }
        //    Session["order"] = newOrder;
        //    Session["orderDetails"] = od;
        //    return View("CaptureAddress");
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CaptureAddress([Bind(Include = "Country,State,City,Address,PostalCode")]Order order)
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();

        //    if (ModelState.IsValid)
        //    {

        //        //Order oz = (Order)Session["order"];
        //        //order.OrderId = oz.OrderId;
        //        //order.OrderDate = oz.OrderDate;
        //        //order.OrderDetails = oz.OrderDetails;
        //        //order.Email = oz.Email;
        //        //order.Total = oz.Total;
        //        //order.UserName = oz.UserName;
        //        //order.ApplicationUser = oz.ApplicationUser;
        //        db.Orders.Add(order);
        //        db.SaveChanges();
        //        return RedirectToAction("Complete");

        //    }
        //    return View("CaptureAddress");
        //}
        //public ActionResult GenerateOrderLayout()
        //{
        //    DeliveryOption od = (DeliveryOption)Session["deliveryOption"];
        //    return View();
        //}
        //public ActionResult CheckOut()
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();
        //    Order newOrder = new Order();
        //    OrderDetail od = new OrderDetail();
        //    string email = (string)Session["email"];
        //    List<Item> carty = (List<Item>)this.Session["cart"];
        //    if (carty != null)
        //    {
        //        newOrder.OrderDate = DateTime.Now;
        //        newOrder.OrderId = (db.Orders.Count()) + 1;
        //        newOrder.UserName = User.Identity.Name;
        //        newOrder.Email = (string)Session["email"];
        //        od.OrderDetailId = (db.OrderDetails.Count()) + 1;
        //        od.OrderId = newOrder.OrderId;
        //        //newOrder.OrderDetails.Add(od);
        //        od.Items = carty;
        //        decimal sum = 0;
        //        if ((od.Items.Count) > 0)
        //        {
        //            foreach (Item nm in od.Items)
        //            {
        //                sum += nm.Product.Price * nm.Quantity;
        //            }
        //        }
        //        newOrder.Total = sum;
        //    }
        //    Session["order"] = newOrder;
        //    Session["orderDetails"] = od;
        //    return View("CheckOut");
        //}
        ////public ActionResult mail()
        ////{
        ////    ApplicationDbContext db = new ApplicationDbContext();
        ////    return View();
        ////}

        ////[HttpPost]
        ////public ActionResult mail(WebApplication2.SendEmail.Models.MailModel objModelMail, HttpPostedFileBase fileUploader)
        ////{
        ////    if (ModelState.IsValid)
        ////    {
        ////        string from = "Your Gmail Id"; //example:- sourabh9303@gmail.com
        ////        using (MailMessage mail = new MailMessage(from, objModelMail.To))
        ////        {
        ////            mail.Subject = objModelMail.Subject;
        ////            mail.Body = objModelMail.Body;
        ////            if (fileUploader != null)
        ////            {
        ////                string fileName = Path.GetFileName(fileUploader.FileName);
        ////                mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
        ////            }
        ////            mail.IsBodyHtml = false;
        ////            SmtpClient smtp = new SmtpClient();
        ////            smtp.Host = "smtp.gmail.com";
        ////            smtp.EnableSsl = true;
        ////            NetworkCredential networkCredential = new NetworkCredential(from, "Your Gmail Password");
        ////            smtp.UseDefaultCredentials = true;
        ////            smtp.Credentials = networkCredential;
        ////            smtp.Port = 587;
        ////            smtp.Send(mail);
        ////            ViewBag.Message = "Sent";
        ////            return View("mail", objModelMail);
        ////        }
        ////    }
        ////    else
        ////    {
        ////        return View();
        ////    }
        ////}

    }
}