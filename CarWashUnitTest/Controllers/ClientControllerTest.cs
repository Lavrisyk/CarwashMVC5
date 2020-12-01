using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCarWash.Controllers;
using System.Web.Mvc;
using Moq;
using System;
using WebCarWash.Domain.Entities;
using WebCarWash.Domain.Abstract;

namespace CarWashUnitTest.Controllers
{
    /// <summary>
    /// Сводное описание для ClientControllerTest
    /// </summary>
    [TestClass]
    public class ClientControllerTest
    {

        #region Дополнительные атрибуты тестирования
        //
        // При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        // ClassInitialize используется для выполнения кода до запуска первого теста в классе
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // TestInitialize используется для выполнения кода перед запуском каждого теста 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // TestCleanup используется для выполнения кода после завершения каждого теста
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        
        private Client[] clients =
        {
            new Client{Name="Tom", Id=1},
            new Client{Name="Pit",Id=2},
            new Client{Name="Pol",Id=3}
        };

        private Order[] orders =
      {
            new Order{OrderId=1,ClientId=1,ServiceDate=DateTime.Now },
            new Order{OrderId=2,ClientId=1,ServiceDate=DateTime.Now},
            new Order{OrderId=3,ClientId=3,ServiceDate=DateTime.Now}
        };

        Mock<IUnitOfWork> mock;

         ClientController  contr;
    

        [TestInitialize()]
        public void MyTestInitialize()
        {
            
            var clientFake= new Client() { Name = "CLIENTTEST", Id = 1, Phone = "tttttt" };
            var client1 = new Client() { Id = 1, Name = "Pol1" , Phone = "111-546-236" };
            var client2= new Client() { Id = 2, Name = "Pit2", Phone = "222-546-236" };
            var clientLst = new List<Client>(){ clientFake,client1, client2};

            mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.Clients.GetAll()).Returns(clientLst);
           
            mock.Setup(m => m.Clients.Get(It.IsAny<int>())).Returns(clientFake);
                     

            contr = new ClientController(mock.Object);
        }
     

        #region GetClients test

        [TestMethod]
        public void GetClientsViewResultNotNull()
        {
           
           ViewResult result = contr.GetClients() as ViewResult;

           Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetClientsViewResultEqual()
        {
           ViewResult result = contr.GetClients() as ViewResult;

            Assert.AreEqual("GetClients", result.ViewName);
        }

        [TestMethod]
        public void GetClientsViewResultIsClientList()
        {
           ViewResult result = contr.GetClients() as ViewResult;
            var clients = (IEnumerable<Client>)result.ViewData.Model;
            Assert.IsInstanceOfType(clients, typeof(IEnumerable<Client>), "Is not ClientList");
          
        }

        #endregion
        //---------------------------------------------------------------------------    

        #region ClientDetails(int id) test

        [TestMethod]
        public void ClientDetailsViewResultIsNotNull()
        {
            int valitId = 1;
          
           ViewResult result = contr.ClientDetails(valitId) as ViewResult;
        

          Assert.IsNotNull(result);
          Assert.AreEqual("ClientDetails", result.ViewName );
        }

        [TestMethod]
        public void ClientDetailsDataResultIsClientInstance()
        {
            int valitId = 1;
         
            var result = contr.ClientDetails(valitId) as ViewResult;
            Client client = (Client)result.ViewData.Model;  // contrCl.ClientDetails(valitId) ;

            Assert.IsInstanceOfType( result.ViewData.Model, typeof(Client));    //.Name.Contains("Pit"));
            Assert.AreEqual(valitId, client.Id);
        }
        #endregion
        //---------------------------------------------------------------------
        #region ClientCreate test
        [TestMethod]
        public void ClientCreateViewResult()
        {
           

            ViewResult result = contr.ClientCreate() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("ClientCreate", result.ViewName);  
        }

        [TestMethod]
        public void ClientCreateViewDataEquallient()
        {

            ViewResult result = contr.ClientCreate() as ViewResult;

            var client = (Client)result.ViewData.Model;

            Assert.IsNotNull(result);
            Assert.AreEqual("ClientCreate", result.ViewName);
        } 
        #endregion
    }
}
