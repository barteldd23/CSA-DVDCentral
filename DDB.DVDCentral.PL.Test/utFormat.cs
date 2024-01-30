

namespace DDB.DVDCentral.PL.Test
{
    [TestClass]
    public class utFormat : utBase<tblFormat>
    {

        [TestMethod]
        public void LoadTest()
        {
            int results = dc.tblFormats.Count();
            Assert.AreEqual(4, results);
        }

        [TestMethod]
        public void InsertTest()
        {
            tblFormat entity = new tblFormat();
            entity.Description = "testInsert";
            entity.Id = Guid.NewGuid();

            int rowsAffected = base.InsertTest(entity);
            Assert.AreEqual(1, rowsAffected);
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
            tblFormat entity = dc.tblFormats.FirstOrDefault(x => x.Description == "Other");
            dc.tblFormats.Remove(entity);
            int results = dc.SaveChanges();
            Assert.AreEqual(1, results);
        }

        //[TestMethod]
        //public void LoadByIdTest()
        //{
        //    tblFormat entity = dc.tblFormats.Where(e => e.Id == 2).FirstOrDefault();

        //    Assert.AreEqual(2, entity.Id);
        //}
    }
}