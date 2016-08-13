using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Myuken.Models
{
    public enum Status
    {
        [Display(Name ="At The Store")]atSite,
        [Display(Name ="In Transit")]BeingDelivered,
        [Display(Name ="At Customer Addrss")]CustomerDoor,
        [Display(Name ="Customer Verified and Approved")]OrderApproved,
        [Display(Name = "Customer Verified and Disapproved")]
        OrderRejected
    }
    public class Delivery
    {
        [Key]
        public int DeliveryId { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public Status Status { get; set; }
    }
}