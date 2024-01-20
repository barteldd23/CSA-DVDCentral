using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.DVDCentral.PL.Test
{
    [TestClass]
    public class utCustomer
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
            Assert.AreEqual(3, dc.tblCustomers.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblCustomer entity = new tblCustomer();
            entity.FirstName = "test";
            entity.LastName = "test";
            entity.Address = "test";
            entity.City = "test";
            entity.State = "wi";
            entity.ZIP = "12345";
            entity.Phone = "1234567890";
            entity.UserId = -99;
            entity.Id = -99;
            dc.tblCustomers.Add(entity);
            int results = dc.SaveChanges();
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblCustomer entity = dc.tblCustomers.FirstOrDefault();

            if(entity != null)
            {
                entity.UserId = -99;
                int results = dc.SaveChanges();
                Assert.AreEqual(1, results);
            }
            
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblCustomer entity = dc.tblCustomers.FirstOrDefault();
            if(entity != null)
            {
                dc.tblCustomers.Remove(entity);
            }
            int results = dc.SaveChanges();
            Assert.AreEqual(1, results);
        }
    }
}
