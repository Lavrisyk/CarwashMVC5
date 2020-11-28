using System;
using System.Collections.Generic;
using System.EnterpriseServices;

namespace WebCarWash.Models
{
    public class Order
    {

        public int OrderId { get; set; }

    //    public int Number { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }

        public DateTime ServiceDate { get; set; }

        public OrderState State { get; set; }

        public int? ClientId { get; set; }
        public Client Client { get; set; }

        public  ICollection<Service> Services { get; set; }

     
        public Order()
        {
            Services = new List<Service>();
        }
    }
} 