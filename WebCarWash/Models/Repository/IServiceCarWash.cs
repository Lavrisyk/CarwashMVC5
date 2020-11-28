using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCarWash.Models.Repository
{
    public interface IServiceCarWash:IDisposable 
    {
        IEnumerable<Service> GetServices();
        IEnumerable<Client> GetClients();
        IEnumerable<Order> GetOrders();

        Client GetClient(int? id);
        void AddClient(Client newClient);

        Order GetOrder(int? id);
        int SaveOrder(Order newO);
    }
}
