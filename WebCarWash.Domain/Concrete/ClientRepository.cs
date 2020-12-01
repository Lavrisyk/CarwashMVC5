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
    public class ClientRepository : IRepository<Client>
    {

      ServicesContext db;

        public ClientRepository(ServicesContext context)
        {
            db = context;
        }

        public void Create(Client client)
        {
            db.Clients.Add(client);
        }

        public void Delete(int id)
        {
            Client client = db.Clients.Find(id);
            if(client!=null)
                db.Clients.Remove(client);
        }

        public Client Get(int id)
        {
           return db.Clients.Find(id);
        }

        public IEnumerable<Client> GetAll()
        {
            
           var order = db.Orders.Include(c => c.Client).Include(o => o.Services).ToList();
           
            return db.Clients.Include(o => o.Orders).ToList();
            
        }

        public void Update(Client client)
        {
            db.Entry(client).State =EntityState.Modified;
        }
    }
}
