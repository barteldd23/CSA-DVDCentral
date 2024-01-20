using DVDCentral.BL.Models;

namespace DDB.DVDCentral.BL.Test
{
    [TestClass]
    public class utOrder
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Order> list = OrderManager.Load();
            Assert.AreEqual(3,list.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Order order = new Order
            {
                Id = -99,
                CustomerId = 1,
                OrderDate = DateTime.Now,
                UserId = 4,
                ShipDate = DateTime.Now
            };
            int result = OrderManager.Insert(order, true);
            Assert.AreEqual(1,result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Order order = OrderManager.LoadById(1);
            order.UserId = -99;
            int result = OrderManager.Update(order, true);
            Assert.AreEqual(1,result);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int result = OrderManager.Delete(2, true);
            Assert.AreEqual(1,result);
        }

        [TestMethod]
        public void InsertOrderItemsTest()
        {
            Order order = new Order
            {
                // removed required from Id on Orders class
                CustomerId = 99,
                OrderDate = DateTime.Now,
                UserId = 99,
                ShipDate = DateTime.Now,
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem
                    {
                        Id = 88,
                        MovieId = 1,
                        Cost = 9.99,
                        Quantity = 9,
                        // removed required from OrderId on OrderItem class
                        
                    },
                    new OrderItem
                    {
                        Id = 99,
                        MovieId = 2,
                        Cost = 8.88,
                        Quantity = 2,
                        
                    }
                }
            };
            int result = OrderManager.Insert(order, true);
            Assert.AreEqual(order.OrderItems[1].OrderId,order.Id);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            int id = OrderManager.Load().LastOrDefault().Id;
            Order order = OrderManager.LoadById(id);
            Assert.AreEqual(id,order.Id);
            Assert.IsTrue(order.OrderItems.Count > 0);
        }

        [TestMethod]
        public void LoadByCustomerIdTest()
        {
            int customerId = OrderManager.Load().FirstOrDefault().CustomerId;

            // Assert.AreEqual(OrderManager.LoadById(customerId).CustomerId, customerId);
            Assert.AreEqual(OrderManager.Load(customerId)[0].CustomerId, customerId);
        }


    }
}
