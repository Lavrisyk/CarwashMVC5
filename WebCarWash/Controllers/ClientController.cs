using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCarWash.Models;
using WebCarWash.Models.Repository;

namespace WebCarWash.Controllers
{
    public class ClientController : Controller
    {

        private IServiceCarWash  repo; //CarWashRepository 

        
        public ClientController()
        {
            repo = new CarWashRepository();
        }

        public ClientController(IServiceCarWash repository )
        {
            repo = repository;
        }

        // GET: Client
        public ActionResult GetClients()
        {
            var clients =  repo.GetClients();

           return View("GetClients",clients);
        }
      
          [HttpGet]
           public ActionResult ClientDetails(int? id)
           {
             Client client = null;

            if (ModelState.IsValid)
            {
                try
                {
                    client = repo.GetClient(id);
                }
                catch (Exception e)
                {
                    // Logger
                    string err = e.Message;
                }

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
           
            if (ModelState.IsValid)
            { //создаем нового заказчика
                
                repo.AddClient(newCl);
              return RedirectToAction("GetClients");
            }

            return Content("Client date not Valid.");
         
           
        }


        [HttpPost]
        public ActionResult ClientUpdate(Client client)
        {

            if (ModelState.IsValid)
            {

                repo.AddClient(client);

                return RedirectToAction("GetClients");
            }

               return Content("Client date not Valid.");

        }

        protected override void Dispose(bool disposing)
        {
            repo.Dispose();
            base.Dispose(disposing);
        }
    }
}