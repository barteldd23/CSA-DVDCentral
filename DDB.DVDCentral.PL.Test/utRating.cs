
namespace DDB.DVDCentral.PL.Test
{
    [TestClass]
    public class utRating : utBase
    {
        [TestMethod]
        public void LoadTest()
        {
            int results = dc.tblRatings.Count();
            Assert.AreEqual(5, results);
        }

        [TestMethod]
        public void InsertTest()
        {
            tblRating entity = new tblRating();
            entity.Description = "testUpdate";
            entity.Id = Guid.NewGuid();

            dc.tblRatings.Add(entity);
            int results = dc.SaveChanges();
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblRating entity = dc.tblRatings.FirstOrDefault();

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
            tblRating entity = dc.tblRatings.FirstOrDefault();
            dc.tblRatings.Remove(entity);
            int results = dc.SaveChanges();
            Assert.AreEqual(1, results);
        }

        //[TestMethod]
        //public void LoadByIdTest()
        //{
        //    tblRating entity = dc.tblRatings.Where(e => e.Id == 2).FirstOrDefault();

        //    Assert.AreEqual(2, entity.Id);
        //}
    }
}