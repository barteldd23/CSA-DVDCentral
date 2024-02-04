using DVDCentral.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.DVDCentral.BL.Test
{
    [TestClass]
    public class utUser : utBase
    {

        [TestInitialize]
        public void Initialize()
        {
            new UserManager(options).Seed();
        }


        [TestMethod]
        public void LoadTest()
        {
            List<User> users = new UserManager(options).Load();
            Assert.IsTrue(users.Count > 0);
        }

        [TestMethod]
        public void InsertTest()
        {
            User user = new User { FirstName = "Bill", LastName = "Smith", UserName = "bsmith", Password = "1234" };
            int result = new UserManager(options).Insert(user, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void LoginSuccess()
        {
            User user = new User { FirstName = "Brian", LastName = "Foote", UserName = "bfoote", Password = "maple" };
            bool result = new UserManager(options).Login(user);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LoginFail()
        {
            try
            {
                User user = new User { FirstName = "Brian", LastName = "Foote", UserName = "bfoote", Password = "xxxxx" };
                new UserManager(options).Login(user);
                Assert.Fail();
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }



    }
}
