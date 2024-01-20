using DVDCentral.BL.Models;

namespace DDB.DVDCentral.BL.Test
{
    [TestClass]
    public class utCustomer
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Customer> list = CustomerManager.Load();
            Assert.AreEqual(3,list.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Customer customer = new Customer
            {
                Id = -99,
                FirstName = "test",
                LastName = "test",
                Address = "test",
                City = "test",
                State = "test",
                ZIP = "test",
                Phone = "test",
                UserId = -99
            };
            int result = CustomerManager.Insert(customer, true);
            Assert.AreEqual(1,result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Customer customer = CustomerManager.LoadById(1);
            customer.UserId = -99;
            int result = CustomerManager.Update(customer, true);
            Assert.AreEqual(1,result);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int result = CustomerManager.Delete(2, true);
            Assert.AreEqual(1,result);
        }

        [TestMethod]
        public void LoadCustomersOfUserId()
        {
            Assert.AreEqual(1, CustomerManager.Load(25).Count);
        }
    }
}
