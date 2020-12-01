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
    public class ServiceRepository : IRepository<Service>
    {
        ServicesContext db;
        public ServiceRepository(ServicesContext  context)
        {
            db=context;
        }

        public void Create(Service service)
        {
            db.Services.Add(service);
        }

        public void Delete(int id)
        {
            Service service = db.Services.Find(id);
            if (service != null)
                db.Services.Remove(service);
        }

        public Service Get(int id)
        {
            return db.Services.Find(id);
        }

        public IEnumerable<Service> GetAll()
        {
            return db.Services;
        }

        public void Update(Service service)
        {
            db.Entry(service).State = EntityState.Modified;
        }
    }
}
