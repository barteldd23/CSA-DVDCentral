

namespace DDB.DVDCentral.PL.Test
{
    [TestClass]
    public class utCustomer : utBase
    {
        

        [TestMethod]
        public void InsertTest()
        {
            tblCustomer entity = new tblCustomer();
            entity.FirstName = "test";
            entity.LastName = "test";
            entity.Address = "test";
            entity.City = "test";
            entity.State = "wi";
            entity.ZIP = "12345";
            entity.Phone = "1234567890";
            entity.UserId = dc.tblUsers.FirstOrDefault().Id;
            entity.Id = Guid.NewGuid();
            dc.tblCustomers.Add(entity);
            int results = dc.SaveChanges();
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblCustomer entity = dc.tblCustomers.FirstOrDefault();

            if(entity != null)
            {
                entity.FirstName = "test";
                int results = dc.SaveChanges();
                Assert.AreEqual(1, results);
            }
            
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblCustomer entity = dc.tblCustomers.FirstOrDefault();
            if(entity != null)
            {
                dc.tblCustomers.Remove(entity);
            }
            int results = dc.SaveChanges();
            Assert.AreEqual(1, results);
        }
    }
}
