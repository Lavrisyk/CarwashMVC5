using System;
using System.Web.Mvc;
using WebCarWash.Domain.Abstract;
using WebCarWash.Domain.Entities;

namespace WebCarWash.Controllers
{
    public class ClientController : Controller
    {

        private IUnitOfWork unitOfWork; 
        private IRepository<Client> clientRepo;

        //public ClientController()
        //{
        //    unitOfWork = (IUnitOfWork) new UnitOfWork();
        //    clientRepo = unitOfWork.Clients;
        //}
        public ClientController(IUnitOfWork unit)
        {
            unitOfWork = unit;
            clientRepo = unit.Clients;
        }

        // GET: Client
        public ActionResult GetClients()
        {
            var clients = clientRepo.GetAll();

            return View("GetClients", clients);
        }

        [HttpGet]
        public ActionResult ClientDetails(int id)
        {
            Client client = null;

            if (ModelState.IsValid)
            {
                
                client = clientRepo.Get(id);
              

                if (client == null)
                { return HttpNotFound("Client not found."); }

                return View("ClientDetails", client);
            }

            return Content("Client ID not valid.");
        }

        [HttpGet]
        public ActionResult ClientCreate()
        {
            Client newCl = new Client();

            return View("ClientCreate", newCl);
        }

        [HttpPost]
        public ActionResult ClientCreate(Client newCl)
        {

            if (!ModelState.IsValid)
                return Content("Client date not Valid.");
            
                if (newCl.Id == 0)
                {//создаем нового заказчика

                    clientRepo.Create(newCl);
                    unitOfWork.Save();

                  return RedirectToAction("GetClients");
                }
            return Content("Client is exsist.");
        }


        [HttpPost]
        public ActionResult ClientUpdate(Client client)
        {

            if (ModelState.IsValid)
            {

                clientRepo.Update(client);
                unitOfWork.Save();

                return RedirectToAction("GetClients");
            }

            return Content("Client date not Valid.");

        }

        protected override void Dispose(bool disposing)
        {
            
            base.Dispose(disposing);
        }
    }
}