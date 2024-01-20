using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.DVDCentral.PL.Test
{
    [TestClass]
    public class utOrderItem
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
            Assert.AreEqual(3, dc.tblOrderItems.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblOrderItem entity = new tblOrderItem();
            entity.OrderId = 2;
            entity.MovieId = 3;
            entity.Quantity = 4;
            entity.Cost = 9999.99;
            entity.Id = -99;
            dc.tblOrderItems.Add(entity);
            int results = dc.SaveChanges();
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblOrderItem entity = dc.tblOrderItems.FirstOrDefault();

            if(entity != null)
            {
                entity.OrderId = -99;
                int results = dc.SaveChanges();
                Assert.AreEqual(1, results);
            }
            
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblOrderItem entity = dc.tblOrderItems.FirstOrDefault();
            if(entity != null)
            {
                dc.tblOrderItems.Remove(entity);
            }
            int results = dc.SaveChanges();
            Assert.AreEqual(1, results);
        }
    }
}
