using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCarWash.Domain.Abstract;
using WebCarWash.Domain.Entities;

namespace WebCarWash.Domain.Concrete
{
    public class OrderRepository : IRepository<Order>
    {
        private ServicesContext db;

        public OrderRepository(ServicesContext context)
        {
            this.db = context;
        }

        public IEnumerable<Order> GetAll()
        {
           return db.Orders.Include(c=>c.Client).Include(o => o.Services).ToList();
        }

        public Order Get(int id)
        {
            return GetAll().FirstOrDefault(i => i.OrderId == id);  //db.Orders.Find(id);
        }

        public void Create(Order order)
        {
            db.Orders.Add(order);
        }

        public void Update(Order order)
        {
            db.Entry(order).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Order order = db.Orders.Find(id);
            if (order != null)
                db.Orders.Remove(order);
        }

    }
}
