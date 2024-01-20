using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;

namespace DDB.DVDCentral.PL.Test
{
    [TestClass]
    public class utFormat
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
            int results = dc.tblFormats.Count();
            Assert.AreEqual(3, results);
        }

        [TestMethod]
        public void InsertTest()
        {
            tblFormat entity = new tblFormat();
            entity.Description = "test Update";
            entity.Id = -99;

            dc.tblFormats.Add(entity);
            int results = dc.SaveChanges();
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblFormat entity = dc.tblFormats.FirstOrDefault();

            if (entity != null)
            {
                entity.Description = "test";
                int results = dc.SaveChanges();
                Assert.AreEqual(1, results);
            }
        }

        [TestMethod]
        public void DeleteTest() 
        {
            tblFormat entity = dc.tblFormats.FirstOrDefault();
            dc.tblFormats.Remove(entity);
            int results = dc.SaveChanges();
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            tblFormat entity = dc.tblFormats.Where(e => e.Id == 2).FirstOrDefault();

            Assert.AreEqual(2, entity.Id);
        }
    }
}