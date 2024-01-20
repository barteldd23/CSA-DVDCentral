using DVDCentral.BL.Models;

namespace DDB.DVDCentral.BL.Test
{
    [TestClass]
    public class utOrderItem
    {
        [TestMethod]
        public void LoadTest()
        {
            List<OrderItem> list = OrderItemManager.Load();
            Assert.AreEqual(3,list.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            OrderItem orderItem = new OrderItem
            {
                Id = -99,
                OrderId = 1,
                MovieId = 1,
                Quantity = 4,
                Cost = 999.99
            };
            int result = OrderItemManager.Insert(orderItem, true);
            Assert.AreEqual(1,result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            OrderItem orderItem = OrderItemManager.LoadById(1);
            orderItem.Cost = -99;
            int result = OrderItemManager.Update(orderItem, true);
            Assert.AreEqual(1,result);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int result = OrderItemManager.Delete(2, true);
            Assert.AreEqual(1,result);
        }

        [TestMethod]
        public void LoadByOrderIdTest()
        {
            int orderId = OrderItemManager.Load().FirstOrDefault().OrderId;
            Assert.IsTrue(OrderItemManager.LoadByOrderId(orderId).Count > 0);
        }
    }
}
