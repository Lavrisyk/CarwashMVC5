using System.Collections.Generic;

namespace WebCarWash.Domain.Entities
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