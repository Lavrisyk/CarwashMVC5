using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCarWash.Domain.Abstract;
using WebCarWash.Domain.Entities;

namespace WebCarWash.Domain.Concrete
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ServicesContext db = new ServicesContext();


        //IRepository<Client> IUnitOfWork.Clients => throw new NotImplementedException();

        //IRepository<Order> IUnitOfWork.Orders => throw new NotImplementedException();


        private IRepository<Client> clientRepository;
        private IRepository<Order> orderRepository;
        private IRepository<Service> serviseRepository;

        public UnitOfWork()
        {

        }

        public IRepository<Service> Services
        {
            get
            {
                if (serviseRepository == null)
                    serviseRepository = new ServiceRepository(db);
                return serviseRepository;
            }
        }

        public IRepository<Client> Clients
        {
            get
            {
                if (clientRepository == null)
                    clientRepository = new ClientRepository(db);
                return clientRepository;
            }
        }


        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(db);
                return orderRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
