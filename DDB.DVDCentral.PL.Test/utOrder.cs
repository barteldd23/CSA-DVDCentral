
namespace DDB.DVDCentral.PL.Test
{
    [TestClass]
    public class utOrder : utBase<tblOrder>
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, dc.tblOrders.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblOrder entity = new tblOrder();
            entity.CustomerId = dc.tblCustomers.FirstOrDefault().Id;
            entity.OrderDate = DateTime.Now;
            entity.UserId = dc.tblCustomers.FirstOrDefault().UserId;
            entity.ShipDate = DateTime.Now;
            entity.Id = Guid.NewGuid();

            int rowsAffected = base.InsertTest(entity);
            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblOrder entity = dc.tblOrders.FirstOrDefault();

            if(entity != null)
            {
                entity.ShipDate = DateTime.Now;
                int results = dc.SaveChanges();
                Assert.AreEqual(1, results);
            }
            
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblOrder entity = dc.tblOrders.FirstOrDefault();
            if(entity != null)
            {
                dc.tblOrders.Remove(entity);
            }
            int results = dc.SaveChanges();
            Assert.AreEqual(1, results);
        }
    }
}
