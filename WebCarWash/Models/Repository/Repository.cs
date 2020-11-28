using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebCarWash.Models.Repository
{
    public class CarWashRepository : IServiceCarWash
    {
        public CarWashRepository()
        {
            context = new ServicesContext();
        }

        //public static CarWashRepository repo;
        //private static readonly object mylockobject = new object();
        //public static CarWashRepository GetInstance()
        //{
        //    lock (mylockobject)
        //    {
        //        if (repo == null)
        //        {
        //            repo = new CarWashRepository();

        //        }
        //    }
        //    return repo;

        //}

        private ServicesContext context ;

        public IEnumerable<Client>  GetClients()
        {
            //get {
                  var order = GetOrders();
                
                  return context.Clients.Include(o => o.Orders).ToList(); 
            
            //    }
        }

        public IEnumerable<Service> GetServices()
        {
            //get { 
                return context.Services; 
            //}
        }

        public IEnumerable<Order> GetOrders()
        {
            //get
            //{
             
                return context.Orders.Include(c => c.Client).Include(o => o.Services).ToList();
           // }
        }

        public Order GetOrder(int? id)

        {
            Order order = new Order();
            if (id != null)
            {
                order = GetOrders().FirstOrDefault(i=>i.OrderId==id);
            }

             return order;
        }

        public int SaveOrder(Order newO)  //, Client client, IEnumerable<Service> services)
        {
            if (newO.ClientId == null)
                throw new Exception(" Не задан клиент");
                   
            Client client =context.Clients.Find(newO.ClientId);


            if(newO.OrderId ==0)
            { //Создать
                newO.Price = newO.Services.Sum(p => p.Cost);
                newO.Amount = newO.Services.Count;
                newO.ServiceDate = DateTime.Now;
                newO.State = OrderState.IsConfirmed;
                context.Orders.Add(newO);
                newO.ClientId = client.Id;
               // client.Orders.Add(newO);


            }
            else
            { //обновить
               Order order = context.Orders.Find(newO.OrderId);
             
                if(order !=null)
                {
                    order.ServiceDate = DateTime.Now;
                    order.State = OrderState.IsForm;
                    order.Services.Clear();
                    order.Price = 0; order.Amount = 0;
                    foreach (var ns in newO.Services)
                    {
                        order.Services.Add(ns);
                        order.Price += ns.Cost;
                        order.Amount += 1;
                    }
                    
                }
            }
                  

           var id= context.SaveChanges();

            return id;
        }


        public void DeleteOrder(Order order)
        {
            Order orderfnd = context.Orders.Find(order.OrderId);
            orderfnd.State = OrderState.IsCancel;
            context.Entry(orderfnd).State= (System.Data.Entity.EntityState)EntityState.Modified;
            context.SaveChanges();
        }

        public void AddClient(Client newclient)
        {
            if(newclient.Id==0)
            {// new Client
                context.Entry<Client>(newclient).State = (System.Data.Entity.EntityState)EntityState.Added;

            }
            else
            {// update
                var client = context.Clients.Find(newclient.Id);

                if (client != null)
                {
                    client.Name = newclient.Name;
                    client.Phone = newclient.Phone;
                    client.Address = newclient.Address;
                   
                    
                }

            }
          
            context.SaveChanges();
        }

        public Client GetClient(int? selectedClient)
        {
            Client fndClient = null;

            if (selectedClient != null)
            {
                fndClient = context.Clients.Find(selectedClient);
            }

                return fndClient;

        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}