using System.Linq;
using System.Web.Mvc;
using WebCarWash.Models;
using WebCarWash.Models.Repository;
using System;
using WebCarWash.ViewModel;
using System.Collections.Generic;
using System.IO;

namespace WebCarWash.Controllers
{
    public class OrderController : Controller
    {
        private IServiceCarWash repo;  //CarWashRepository repo; 

        public OrderController()
        {
            repo = new CarWashRepository();

        }

        public OrderController(IServiceCarWash repository)
        {
            repo = repository;
        }

        // GET: Order
        public ActionResult GetOrders()
        {
            var orders = repo.GetOrders(); 



            return View("GetOrders",orders.ToList());
        }
        
        [HttpGet]
        public ActionResult OrderDetails(int? id)
        {
            if (ModelState.IsValid)
            {
               var order = repo.GetOrder(id);

                if (order == null)
                {
                    return HttpNotFound();
                }

                return View("OrderDetails", order);
            }
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult OrderCreate(int? id)
        {
            if (ModelState.IsValid)
            {
                var client = repo.GetClient(id);


                OrderViewModel ovm = new OrderViewModel()
                {
                    ClientId = client.Id,
                    ClientName = client.Name,
                    OrderDate = DateTime.Now,
                    State = OrderState.IsForm,
                    Price = 0,
                    Amount = 0

                };

                ovm.Servises = repo.GetServices().Select(s => new SelectListItem
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
        public ActionResult OrderCreate(OrderViewModel ovm)
        {
            if (!ModelState.IsValid)
                return HttpNotFound();

            var newServices = new List<Service>();
            // Список выбранных услуг
            if (ovm.SelectedListServices != null)
            {
                var allServicse = repo.GetServices();

                //получаем выбранные услуги
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

            var client = repo.GetClient(ovm.ClientId);

           var newOrder = new Order()
            {
               OrderId=ovm.OrderId,
                Client = client,
                ClientId = client.Id,
                ServiceDate = DateTime.Now,
                State = ovm.State,
                Services = newServices
                
            };


          int i=  repo.SaveOrder(newOrder);

          newOrder = repo.GetOrder(newOrder.OrderId);

            // return  Content("OK");
            return View("OrderDetails", newOrder);
        }

        public ActionResult OrderUpdate(OrderViewModel ovm)
        {
            OrderCreate(ovm);

            return Content("Update executed");
        }

        public ActionResult OrderEdit(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Content("Order not found");
            }

            var order = repo.GetOrder(id);


            OrderViewModel ovm = new OrderViewModel()
            {
                OrderId= order.OrderId,
                ClientId = order.Client.Id,
                ClientName = order.Client.Name,
                OrderDate = order.ServiceDate,
                State = order.State,
                Price = order.Price,
                Amount = order.Amount

            };

            ovm.Servises = repo.GetServices().Select(s => new SelectListItem
            {
                Selected = false,
                Text = s.Title,
                Value = s.ServiceId.ToString()
            });

            ovm.SelectedListServices = order.Services.Select(s => s.ServiceId.ToString()).ToList();

            return View("OrderCreate",ovm);
        }


        public ActionResult OrderPdf(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Content("Order not found");
            }

            var order = repo.GetOrder(id);
            var pathTemplate = Server.MapPath(@"~/Resource/car_wash.indd");
            var fileName = order.OrderId + ".PDF"; 
            var pathToSave= Server.MapPath(@"~/Content/Files/");

            if (order.ConvertToPdfFile(pathTemplate, pathToSave, fileName) == true)
                return File(Path.Combine(pathToSave, fileName), "application/pdf", fileName);
            else
                return Content("Ошибка при создании файла .PDF");

        }
    }
}