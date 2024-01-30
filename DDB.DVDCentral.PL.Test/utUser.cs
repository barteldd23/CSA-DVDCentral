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
            var user = base.LoadTest();
            Assert.IsTrue(user.Count > 0);
        }

        [TestMethod]
        public void InsertTest()
        {
            tblUser entity = new tblUser();
            entity.FirstName = "testInsert";
            entity.LastName = "testLast";
            entity.Password = "password";
            entity.UserName = "username";
            entity.Id = Guid.NewGuid();

            int rowsAffected = base.InsertTest(entity);
            Assert.AreEqual(1, rowsAffected);
        }
    }
}
