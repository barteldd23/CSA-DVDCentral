

namespace DDB.DVDCentral.PL.Test
{
    [TestClass]
    public class utMovieGenre : utBase<tblMovieGenre>
    {
        

        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(13, dc.tblMovieGenres.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblMovieGenre entity = new tblMovieGenre();
            entity.MovieId = base.LoadTest().FirstOrDefault().MovieId;
            entity.GenreId = base.LoadTest().FirstOrDefault().GenreId;
            entity.Id = Guid.NewGuid();

            int rowsAffected = base.InsertTest(entity);
            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblMovieGenre entity = dc.tblMovieGenres.FirstOrDefault();

            if(entity != null)
            {
                entity.Id = Guid.NewGuid();
                int results = dc.SaveChanges();
                Assert.AreEqual(1, results);
            }
            
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblMovieGenre entity = dc.tblMovieGenres.FirstOrDefault();
            if(entity != null)
            {
                dc.tblMovieGenres.Remove(entity);
            }
            int results = dc.SaveChanges();
            Assert.AreEqual(1, results);
        }
    }
}
