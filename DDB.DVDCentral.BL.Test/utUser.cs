using DVDCentral.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.DVDCentral.BL.Test
{
    [TestClass]
    public class utUser
    {
        public void Seed()
        {
            UserManager.Seed();

        }

        [TestMethod]
        public void InsertTest()
        {
            User user = new User
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Test",
                Password = "Test",
                UserName = "Test"
            };
            int result = UserManager.Insert(user, true);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void LoadTest()
        {
            Seed();
            List<User> user = UserManager.Load();
            Assert.AreEqual(26, user.Count);
        }

        [TestMethod]
        public void LoginSuccessTest()
        {
            Seed();

            User user1 = new User
            {
                UserName = "bfoote",
                Password = "maple"
            };

            User user2 = new User
            {
                UserName = "dbartel",
                Password = "password"
            };

            Assert.IsTrue(UserManager.Login(user1));
            Assert.IsTrue(UserManager.Login(user2));
        }

        [TestMethod]
        public void LoginFailureNoUserId()
        {
            Seed();
            try
            {
                User user1 = new User
                {
                    Password = "maple"
                };
                UserManager.Login(user1);
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void LoginFailureNoPassword()
        {
            Seed();
            try
            {
                User user1 = new User
                {
                    UserName = "bfoote"
                };
                UserManager.Login(user1);
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void LoginFailureBadPassword()
        {
            Seed();
            try
            {
                User user1 = new User
                {
                    UserName = "bfoote",
                    Password = "test"
                };
                UserManager.Login(user1);
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void LoginFailureBadUserId()
        {
            Seed();
            try
            {
                User user1 = new User
                {
                    UserName = "BFOOTE",
                    Password = "maple"
                };
                UserManager.Login(user1);
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


    }
}
