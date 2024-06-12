using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Snack_Sniker.Models
{
    public class OrderTypeMV
    {
        public OrderTypeMV() {
            
        }
        public int OrderTypeID { get; set; }
        public string OrderType { get; set; }
    }
}