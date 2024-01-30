
namespace DDB.DVDCentral.PL.Test
{
    [TestClass]
    public class utRating : utBase<tblRating>
    {
        [TestMethod]
        public void LoadTest()
        {
            var results = base.LoadTest();
            Assert.AreEqual(5, results.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblRating entity = new tblRating();
            entity.Description = "testUpdate";
            entity.Id = Guid.NewGuid();

            int rowsAffected = base.InsertTest(entity);
            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblRating entity = base.LoadTest().FirstOrDefault();

            if (entity != null)
            {
                entity.Description = "test";
                int results = base.UpdateTest(entity);
                Assert.AreEqual(1, results);
            }
        }

        [TestMethod]
        public void DeleteTest() 
        {
            tblRating row = base.LoadTest().FirstOrDefault(x => x.Description == "Other");
            int results = base.DeleteTest(row);
            Assert.IsTrue(results == 1);
        }

        //[TestMethod]
        //public void LoadByIdTest()
        //{
        //    tblRating entity = dc.tblRatings.Where(e => e.Id == 2).FirstOrDefault();

        //    Assert.AreEqual(2, entity.Id);
        //}
    }
}