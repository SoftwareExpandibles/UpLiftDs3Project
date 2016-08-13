using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Myuken.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public string FullName { get; set; }
    }
}