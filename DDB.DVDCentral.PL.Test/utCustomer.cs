

namespace DDB.DVDCentral.PL.Test
{
    [TestClass]
    public class utCustomer : utBase<tblCustomer>
    {
        [TestMethod]
        public void LoadTest()
        {
            int expected = 3;
            var customers = base.LoadTest();
            Assert.AreEqual(expected, customers.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblCustomer newRow = new tblCustomer();

            newRow.Id = Guid.NewGuid();
            newRow.FirstName = "Joe";
            newRow.LastName = "Billings";
            newRow.Address = "XXXXXX";
            newRow.City = "Greenville";
            newRow.State = "WI";
            newRow.ZIP = "54942";
            newRow.Phone = "xxx-xxx-xxxx";
            newRow.UserId = dc.tblUsers.FirstOrDefault().Id;

            int rowsAffected = InsertTest(newRow);

            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblCustomer row = base.LoadTest().FirstOrDefault();

            if (row != null)
            {
                row.FirstName = "Sarah";
                row.LastName = "Vicchiollo";
                int rowsAffected = UpdateTest(row);
                Assert.AreEqual(1, rowsAffected);
            }
        }


        [TestMethod]
        public void DeleteTest()
        {
            tblCustomer row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                int rowsAffected = DeleteTest(row);
                Assert.IsTrue(rowsAffected == 1);
            }
        }
    }
}
