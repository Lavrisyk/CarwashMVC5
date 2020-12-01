using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCarWash.Domain.Entities;

namespace WebCarWash.Domain.Abstract
{
    public interface IUnitOfWork:IDisposable
    {
        IRepository<Client> Clients { get; }
        IRepository<Order> Orders { get; }

        IRepository<Service> Services { get; }

        void Save();

    }
}
