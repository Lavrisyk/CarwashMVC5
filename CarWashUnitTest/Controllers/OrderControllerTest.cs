using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using WebCarWash.Controllers;
using WebCarWash.Models.Repository;
using Moq;
using WebCarWash.Models;
using WebCarWash.ViewModel;

namespace CarWashUnitTest.Controllers
{
    /// <summary>
    /// Сводное описание для OrderControllerTest
    /// </summary>
    [TestClass]
    public class OrderControllerTest
    {
        public OrderControllerTest()
        {
            //
            // TODO: добавьте здесь логику конструктора
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Получает или устанавливает контекст теста, в котором предоставляются
        ///сведения о текущем тестовом запуске и обеспечивается его функциональность.
        ///</summary>
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
        Mock<IServiceCarWash> mock;
        int validId = 1;
        int okSave = 1;

        [TestInitialize()]
       public void MyTestInitialize() {


            var orderFake = new Order() { OrderId =1, ClientId= validId };
            var orderLst = new List<Order>() { orderFake };

            var clientFake = new Client() { Name = "CLIENTTEST", Id = validId, Phone = "tttttt" };
            var clientLst = new List<Client>() { clientFake };


            mock = new Mock<IServiceCarWash>();
            mock.Setup(a => a.GetOrders()).Returns(orderLst);

            mock.Setup(a => a.GetClients()).Returns(clientLst);
            mock.Setup(m => m.GetClient(It.IsAny<int>())).Returns(clientFake);



            mock.Setup(m => m.GetOrder(It.IsAny<int>())).Returns(orderFake);

            mock.Setup(m => m.SaveOrder(It.IsAny<Order>())).Returns(okSave);

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

        #region OrderDetails(int? id) test

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
            var orderVM= new WebCarWash.ViewModel.OrderViewModel() 
            { OrderId = validId };
       

            ViewResult result = contr.OrderCreate(orderVM) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("OrderDetails", result.ViewName);    //.Name.Contains("Pit"));
        }

        [TestMethod]
        public void OrderCreateIdViewDataEquallient()
        {           

            ViewResult result = contr.OrderCreate(validId) as ViewResult;

            var ovm = (OrderViewModel)result.ViewData.Model;

            Assert.IsNotNull(result);
            Assert.AreEqual(validId, ovm.ClientId);
        }
        #endregion
    }
}

