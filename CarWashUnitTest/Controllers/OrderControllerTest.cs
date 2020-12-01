using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using WebCarWash.Controllers;
using Moq;
using WebCarWash.Domain.Abstract;
using WebCarWash.Domain.Entities;

namespace CarWashUnitTest.Controllers
{

    [TestClass]
    public class OrderControllerTest
    {
        public OrderControllerTest()
        {
           
        }

        private TestContext testContextInstance;

        
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        OrderController contr;
        Mock<IUnitOfWork> mock;

        int validId = 1;
     
        [TestInitialize()]
       public void MyTestInitialize() 
        {
            var orderFake = new Order() { OrderId = validId, ClientId= validId };
            var orderLst = new List<Order>() { orderFake };

            var clientFake = new Client() { Name = "CLIENTTEST", Id = validId, Phone = "tttttt" };
            var clientLst = new List<Client>() { clientFake };

            var serviceFake = new Service() {ServiceId= validId, Title="TestService" };
            var serviseLst = new List<Service>() { serviceFake };


            mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.Orders.GetAll()).Returns(orderLst);

            mock.Setup(m => m.Clients.GetAll()).Returns(clientLst);
            mock.Setup(m => m.Clients.Get(It.IsAny<int>())).Returns(clientFake);

            mock.Setup(m => m.Services.GetAll()).Returns(serviseLst);
            mock.Setup(m => m.Orders.Get(It.IsAny<int>())).Returns(orderFake);

            mock.Setup(m => m.Orders.Create(It.IsAny<Order>()));


           contr = new OrderController(mock.Object);

        }

        #region GetOrders test

        [TestMethod]
        public void GetOrdersViewResultNotNull()
        {
            ViewResult result = contr.GetOrders() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("GetOrders", result.ViewName);
        }

        [TestMethod]
        public void GetOrdersViewResultIsOrderList()
        {
            ViewResult result = contr.GetOrders() as ViewResult;
            var orders = (IEnumerable<Order>)result.ViewData.Model;
            Assert.IsInstanceOfType(orders, typeof(IEnumerable<Order>), "Is not OrderList");
           
        }
        #endregion

        #region OrderDetails(int id) test

        [TestMethod]
        public void OrderDetailsViewResultIsNotNull()
        {                      
            ViewResult result = contr.OrderDetails(validId) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("OrderDetails", result.ViewName);
        }

        [TestMethod]
        public void OrderDetailsDataResultIsClientInstance()
        {
                     
            var result = contr.OrderDetails(validId) as ViewResult;
            Order order = (Order)result.ViewData.Model;  

            Assert.IsInstanceOfType(result.ViewData.Model, typeof(Order));    //.Name.Contains("Pit"));
            Assert.AreEqual(validId, order.OrderId);
        }

        #endregion

        #region OrderCreate test
        [TestMethod]
        public void OrderCreateVMViewResult()
        {
            var orderVM= new WebCarWash.Model.OrderViewModel() 
            { OrderId = validId };
       

            ViewResult result = contr.OrderCreate(orderVM) as ViewResult;

           // Assert.IsNotNull(result);
            Assert.AreEqual("OrderDetails", result.ViewName);   
        }

        [TestMethod]
        public void OrderCreateIdViewDataEquallient()
        {           

            ViewResult result = contr.OrderCreate(validId) as ViewResult;

            var ovm = (WebCarWash.Model.OrderViewModel)result.ViewData.Model;

            Assert.IsNotNull(result);
            Assert.AreEqual(validId, ovm.ClientId);
        }
        #endregion
    }
}

