using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCarWash.Models.Repository;

namespace WebCarWash.Models
{
    public interface IUnitOfWork
    {
        void Commit();
    }
    public class UnitOfWork  : IUnitOfWork
    {
       private ServicesContext sContext;

        private CarWashRepository repo;

        public UnitOfWork()
        {
           
        }

        public CarWashRepository Repo
        {
            get { return repo ?? (repo = CreateRepo()); }
        }

        private CarWashRepository CreateRepo()
        {
           return new CarWashRepository();
        }

        public ServicesContext DbContext
        {
            get { return sContext ?? (sContext = new ServicesContext()); }
        }

        public void Commit()
        {
            sContext.SaveChanges();
        }
    }
}