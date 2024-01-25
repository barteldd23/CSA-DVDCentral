

namespace DDB.DVDCentral.PL.Test
{
    [TestClass]
    public class utMovieGenre : utBase
    {
        

        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(4, dc.tblMovieGenres.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblMovieGenre entity = new tblMovieGenre();
            entity.MovieId = dc.tblMovies.FirstOrDefault().Id;
            entity.GenreId = dc.tblGenres.FirstOrDefault().Id;
            entity.Id = Guid.NewGuid();
            dc.tblMovieGenres.Add(entity);
            int results = dc.SaveChanges();
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblMovieGenre entity = dc.tblMovieGenres.FirstOrDefault();

            if(entity != null)
            {
                entity.MovieId = Guid.NewGuid();
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
