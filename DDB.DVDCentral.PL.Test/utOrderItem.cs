

namespace DDB.DVDCentral.PL.Test
{
    [TestClass]
    public class utOrderItem : utBase<tblOrderItem>
    {
       
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, dc.tblOrderItems.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblOrderItem entity = new tblOrderItem();
            entity.OrderId = dc.tblOrders.FirstOrDefault().Id;
            entity.MovieId = dc.tblMovies.FirstOrDefault().Id;
            entity.Quantity = 4;
            entity.Cost = 9999.99;
            entity.Id = Guid.NewGuid();

            int rowsAffected = base.InsertTest(entity);
            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblOrderItem entity = dc.tblOrderItems.FirstOrDefault();

            if(entity != null)
            {
                entity.Quantity = 99;
                int results = dc.SaveChanges();
                Assert.AreEqual(1, results);
            }
            
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblOrderItem entity = dc.tblOrderItems.FirstOrDefault();
            if(entity != null)
            {
                dc.tblOrderItems.Remove(entity);
            }
            int results = dc.SaveChanges();
            Assert.AreEqual(1, results);
        }
    }
}
