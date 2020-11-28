using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebCarWash.Models.Repository
{
    public class ServicesContext:DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Service> Services { get; set; }

        //public DbSet<OrderServices> OrderInfo { get; set; }

        public ServicesContext()
        {

           // Database.SetInitializer(new ServiceDbInitializer());
            //  Database.SetInitializer<SchoolDBContext>(new CreateDatabaseIfNotExists<SchoolDBContext>());
            //Database.SetInitializer<MobileContext>(new MyContextInitializer());
        
        }


        public class ServiceDbInitializer : DropCreateDatabaseAlways<ServicesContext>
        //DropCreateDatabaseAlways<ServicesContext> //DropCreateDatabaseIfModelChanges<ServicesContext>
        {
            protected override void Seed(ServicesContext db)
            {
                //Servises


                var s1 = new Service { ServiceId = 1, Title = "Hand wash without shampoo", Cost = 10.5m };
                var s2 = new Service { ServiceId = 2, Title = "Hand wash with shampoo", Cost = 15.5m };
                var s3 = new Service { ServiceId = 3, Title = "Automatic bodywork wash", Cost = 12.0m };
                var s4 = new Service { ServiceId = 4, Title = "Engine wash", Cost = 20.5m };
                var s5 = new Service { ServiceId = 5, Title = "Car interior cleaning", Cost = 35.5m };


                db.Services.Add(s1);
                db.Services.Add(s2);
                db.Services.Add(s3);
                db.Services.Add(s4);
                db.Services.Add(s5);


                var cl1 = new Client { Id = 1, Name = "Mike", Phone = "123-123-1123" };
                var cl2 = new Client { Id = 2, Name = "Pit", Phone = "123-987-4478" };
                var cl3 = new Client { Id = 3, Name = "Sam", Phone = "143-222-1267" };

                db.Clients.Add(cl1);
                db.Clients.Add(cl2);
                db.Clients.Add(cl3);


                //db.Orders.Add(new Order { Number = 1, ClientId = cl1.Id, Client = cl1, Amount = 1, ServiceDate = new DateTime(2020, 10, 10, 12, 40, 00), Services = new List<Service> { s1, s3, s4 } });
                //db.Orders.Add(new Order { Number=2, ClientId = cl2.Id, Client = cl2,  Amount = 1, ServiceDate =new  DateTime(2020,10,15,16,40,34) , Services = new List<Service>{ s1, s3, s4 } });
                //db.Orders.Add(new Order {  Number = 3, ClientId = cl2.Id, Client = cl2, Amount = 1, ServiceDate = DateTime.Now, Services = new List<Service> { s1, s3, s4 } });
                //db.Orders.Add(new Order {  Number = 4, ClientId = cl3.Id, Client = cl3, Amount = 1, ServiceDate = DateTime.Now, Services = new List<Service> { s1, s3, s4 } });

                base.Seed(db);
            }
        }

        //public MobileContext() : base("DefaultConnection")
        //{ }

        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}
    }

    

    public class ServiceDbInitializer : DropCreateDatabaseIfModelChanges<ServicesContext>
    //DropCreateDatabaseAlways<ServicesContext> //DropCreateDatabaseIfModelChanges<ServicesContext>
    {
        protected override void Seed(ServicesContext db)
        {
            //Servises
            

            var s1= new Service { ServiceId = 1, Title = "Hand wash without shampoo", Cost = 10.5m };
            var s2 = new Service { ServiceId = 2, Title = "Hand wash with shampoo", Cost = 15.5m };
            var s3 = new Service { ServiceId = 3, Title = "Automatic bodywork wash", Cost = 12.0m };
            var s4 = new Service { ServiceId = 4, Title = "Engine wash", Cost = 20.5m };
            var s5 = new Service { ServiceId = 5, Title = "Car interior cleaning", Cost = 35.5m };


            db.Services.Add(s1);
            db.Services.Add(s2);
            db.Services.Add(s3);
            db.Services.Add(s4);
            db.Services.Add(s5);


            var cl1= new Client { Id = 1, Name = "Mike", Phone = "123-123-1123" };
            var cl2 = new Client { Id = 2, Name = "Pit", Phone = "123-987-4478" };
            var cl3 = new Client { Id = 3, Name = "Sam", Phone = "143-222-1267" };

            db.Clients.Add(cl1);
            db.Clients.Add(cl2);
            db.Clients.Add(cl3);


            //db.Orders.Add(new Order { Number = 1, ClientId = cl1.Id, Client = cl1, Amount = 1, ServiceDate = new DateTime(2020, 10, 10, 12, 40, 00), Services = new List<Service> { s1, s3, s4 } });
            //db.Orders.Add(new Order { Number=2, ClientId = cl2.Id, Client = cl2,  Amount = 1, ServiceDate =new  DateTime(2020,10,15,16,40,34) , Services = new List<Service>{ s1, s3, s4 } });
            //db.Orders.Add(new Order {  Number = 3, ClientId = cl2.Id, Client = cl2, Amount = 1, ServiceDate = DateTime.Now, Services = new List<Service> { s1, s3, s4 } });
            //db.Orders.Add(new Order {  Number = 4, ClientId = cl3.Id, Client = cl3, Amount = 1, ServiceDate = DateTime.Now, Services = new List<Service> { s1, s3, s4 } });

            base.Seed(db);
        }
    }
}