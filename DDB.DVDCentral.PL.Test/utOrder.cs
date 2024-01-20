using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.DVDCentral.PL.Test
{
    [TestClass]
    public class utOrder
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
            Assert.AreEqual(3, dc.tblOrders.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblOrder entity = new tblOrder();
            entity.CustomerId = 2;
            entity.OrderDate = DateTime.Now;
            entity.UserId = 1;
            entity.ShipDate = DateTime.Now;
            entity.Id = -99;
            dc.tblOrders.Add(entity);
            int results = dc.SaveChanges();
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblOrder entity = dc.tblOrders.FirstOrDefault();

            if(entity != null)
            {
                entity.CustomerId = -99;
                int results = dc.SaveChanges();
                Assert.AreEqual(1, results);
            }
            
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblOrder entity = dc.tblOrders.FirstOrDefault();
            if(entity != null)
            {
                dc.tblOrders.Remove(entity);
            }
            int results = dc.SaveChanges();
            Assert.AreEqual(1, results);
        }
    }
}
