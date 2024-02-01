using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.DVDCentral.PL.Test
{
    [TestClass]
    public class utUser : utBase<tblUser>
    {

        [TestMethod]
        public void LoadTest()
        {
            Assert.IsTrue(base.LoadTest().Count() > 0);
        }

        [TestMethod]
        public void InsertTest()
        {
            tblUser newRow = new tblUser();

            newRow.Id = Guid.NewGuid();
            newRow.FirstName = "Joe";
            newRow.LastName = "Billings";
            newRow.UserName = "XXXXXX";
            newRow.Password = "YYYYY";

            int rowsAffected = InsertTest(newRow);

            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblUser row = base.LoadTest().FirstOrDefault();

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
            tblUser row = base.LoadTest().FirstOrDefault();

            if (row != null)
            {
                dc.tblUsers.Remove(row);
                int rowsAffected = DeleteTest(row);

                Assert.IsTrue(rowsAffected == 1);
            }

        }
    }
}
