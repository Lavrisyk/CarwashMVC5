using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCarWash.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string Title { get; set; }
        public decimal Cost { get; set; }

        public ICollection<Order> Orders { get; set; }
        public Service()
        {
            Orders = new List<Order>();

        }
    }
}