
namespace DDB.DVDCentral.PL.Test
{
    [TestClass]
    public class utGenre : utBase
    {
        
        [TestMethod]
        public void LoadTest()
        {
            int results = dc.tblGenres.Count();
            Assert.AreEqual(4, results);
        }

        [TestMethod]
        public void InsertTest()
        {
            tblGenre entity = new tblGenre();
            entity.Description = "test";
            entity.Id = Guid.NewGuid() ;

            dc.tblGenres.Add(entity);
            int results = dc.SaveChanges();
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblGenre entity = dc.tblGenres.FirstOrDefault();

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
            tblGenre entity = dc.tblGenres.FirstOrDefault();
            dc.tblGenres.Remove(entity);
            int results = dc.SaveChanges();
            Assert.AreEqual(1, results);
        }

        //[TestMethod]
        //public void LoadByIdTest()
        //{
        //    tblGenre entity = dc.tblGenres.Where(e => e.Id == 2).FirstOrDefault();

        //    Assert.AreEqual(2, entity.Id);
        //}
    }
}