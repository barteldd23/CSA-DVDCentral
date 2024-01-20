using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;

namespace DDB.DVDCentral.PL.Test
{
    [TestClass]
    public class utDirector
    {
        protected DVDCentralEntities dc;
        protected IDbContextTransaction transaction;

        [TestInitialize]
        public void Initialize()
        {
            dc = new DVDCentralEntities();
            transaction = dc.Database.BeginTransaction();
        }

        [TestCleanup]
        public void Cleanup()
        {
            transaction.Rollback();
            transaction.Dispose();
            dc = null;
        }


        [TestMethod]
        public void LoadTest()
        {
            int results = dc.tblDirectors.Count();
            Assert.AreEqual(3, results);
        }

        [TestMethod]
        public void InsertTest()
        {
            tblDirector entity = new tblDirector();
            entity.FirstName = "test FN";
            entity.LastName = "test LN";
            entity.Id = -99;

            dc.tblDirectors.Add(entity);
            int results = dc.SaveChanges();
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblDirector entity = dc.tblDirectors.FirstOrDefault();

            if (entity != null)
            {
                entity.FirstName = "test Update";
                int results = dc.SaveChanges();
                Assert.AreEqual(1, results);
            }
        }

        [TestMethod]
        public void DeleteTest() 
        {
            tblDirector entity = dc.tblDirectors.FirstOrDefault();
            dc.tblDirectors.Remove(entity);
            int results = dc.SaveChanges();
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            tblDirector entity = dc.tblDirectors.Where(e => e.Id == 2).FirstOrDefault();

            Assert.AreEqual(2, entity.Id);
        }
    }
}