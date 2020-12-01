using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using WebCarWash.Domain.Abstract;
using WebCarWash.Domain.Concrete;
using WebCarWash.Domain.Entities;


namespace WebCarWash.Controllers
{
    public class OrderController : Controller
    {
        private IUnitOfWork unitOfWork; //CarWashRepository 
        private IRepository<Order> orderRepo;
        private IRepository<Client> clientRepo;
        private IRepository<Service> serviceRepo;

        public OrderController()
        {
            unitOfWork = new UnitOfWork();
            orderRepo = unitOfWork.Orders;
            clientRepo = unitOfWork.Clients;
            serviceRepo = unitOfWork.Services;
        }

        public OrderController(IUnitOfWork inpunitOfWork)
        {
            unitOfWork = inpunitOfWork;
            orderRepo = unitOfWork.Orders;
            clientRepo = unitOfWork.Clients;
            serviceRepo = unitOfWork.Services;
            


        }

        // GET: Order
        public ActionResult GetOrders()
        {
            var orders =orderRepo.GetAll();



            return View("GetOrders", orders.ToList());
        }

        [HttpGet]
        public ActionResult OrderDetails(int id)
        {
            if (ModelState.IsValid)
            {
                var order = orderRepo.Get(id);

                if (order == null)
                {
                    return HttpNotFound();
                }

                return View("OrderDetails", order);
            }
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult OrderCreate(int id)
        {
            if (ModelState.IsValid)
            {
                var client =clientRepo.Get(id);

               var ovm = new WebCarWash.Model.OrderViewModel()
                {
                    ClientId = client.Id,
                    ClientName = client.Name,
                    OrderDate = DateTime.Now,
                  //  State = OrderState.IsForm,
                    Price = 0,
                    Amount = 0

                };

                ovm.Servises = serviceRepo.GetAll().Select(s => new SelectListItem
                {
                    Selected = false,
                    Text = s.Title,
                    Value = s.ServiceId.ToString()
                });

                return View(ovm);

            }

            return Content("Order Id not valid");

        }

        [HttpPost]
        public ActionResult OrderCreate(WebCarWash.Model.OrderViewModel ovm)
        {
            if (!ModelState.IsValid)
                return HttpNotFound();

            var newServices = new List<Service>();
            
            // services have selected  in the View
            if (ovm.SelectedListServices != null)
            {
                var allServicse = serviceRepo.GetAll();
               
                foreach (var s in ovm.SelectedListServices)
                {
                    var servise = allServicse.Where(ss => ss.ServiceId.ToString() == s).FirstOrDefault();
                    newServices.Add(servise);
                }
            }
            else
            {
                return HttpNotFound();
            }

            var client = clientRepo.Get(ovm.ClientId);

            var newOrder = new Order()
            {
                OrderId = ovm.OrderId,
                Client = client,
                ClientId = client.Id,
                ServiceDate = DateTime.Now,
                State = ovm.State,
                Services = newServices,
                Price=newServices.Sum(s=>s.Cost),
                Amount= newServices.Count

            };


            orderRepo.Create(newOrder);

           unitOfWork.Save();
  
            return View("OrderDetails", newOrder);
        }

        public ActionResult OrderUpdate(Model.OrderViewModel ovm)
        {
            OrderCreate(ovm);

            return Content("Update executed");
        }

        public ActionResult OrderEdit(int id)
        {
            if (!ModelState.IsValid)
            {
                return Content("Order not found");
            }

            var order =orderRepo.Get(id);


           var ovm = new Model.OrderViewModel()
            {
                OrderId = order.OrderId,
                ClientId = order.Client.Id,
                ClientName = order.Client.Name,
                OrderDate = order.ServiceDate,
                //State = order.State,
                Price = order.Price,
                Amount = order.Amount

            };

            ovm.Servises = serviceRepo.GetAll().Select(s => new SelectListItem
            {
                Selected = false,
                Text = s.Title,
                Value = s.ServiceId.ToString()
            });

            ovm.SelectedListServices = order.Services.Select(s => s.ServiceId.ToString()).ToList();

            return View("OrderCreate", ovm);
        }


        public ActionResult OrderPdf(int id)
        {
            if (!ModelState.IsValid)
            {
                return Content("Order not found");
            }

            var order = orderRepo.Get(id);
                        
            var pathTemplate = Server.MapPath(@"~/Content/Images/car_wash.indd");
            var fileName =  "Order.PDF";
            var pathToSave = Server.MapPath(@"~/Content/Files/");

            if (Models.OrderConvert.ToPdfFile(order,pathTemplate, pathToSave, fileName) == true)
                return File(Path.Combine(pathToSave, fileName), "application/pdf", fileName);
            else
                return Content("Error creating file .PDF");

        }
    }
}