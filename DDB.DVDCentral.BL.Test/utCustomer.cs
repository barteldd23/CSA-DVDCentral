using DVDCentral.BL.Models;

namespace DDB.DVDCentral.BL.Test
{
    [TestClass]
    public class utCustomer : utBase
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Customer> customers = new CustomerManager(options).Load();
            int expected = 3;

            Assert.AreEqual(expected, customers.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Customer customer = new Customer
            {
                FirstName = "XXXXX",
                LastName = "XXXXX",
                Address = "XXXXX",
                City = "XXXXX",
                State = "XX",
                ZIP = "XXXXX",
                Phone = "XXX-XXX-XXXX",
                UserId = new UserManager(options).Load().FirstOrDefault().Id
            };

            int result = new CustomerManager(options).Insert(customer, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Customer customer = new CustomerManager(options).Load().FirstOrDefault();
            customer.FirstName = "Blah blah";

            Assert.IsTrue(new CustomerManager(options).Update(customer, true) > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Customer customer = new CustomerManager(options).Load().FirstOrDefault();

            Assert.IsTrue(new CustomerManager(options).Delete(customer.Id, true) > 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            Customer customer = new CustomerManager(options).Load().FirstOrDefault();
            Assert.AreEqual(new CustomerManager(options).LoadById(customer.Id).Id, customer.Id);
        }


    }
}
