using DVDCentral.BL.Models;

namespace DDB.DVDCentral.BL.Test
{
    [TestClass]
    public class utMovie
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Movie> list = MovieManager.Load();
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Movie movie = new Movie
            {
                Title = "Test",
                Description = "Test",
                Cost = 9.99,
                RatingId = 1,
                FormatId = 1,
                DirectorId = 1,
                InStkQty = 1,
                ImagePath = "",
                Id = -99
            };
            int result = MovieManager.Insert(movie, true);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int result = MovieManager.Delete(1, true);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Movie movie = MovieManager.LoadById(2);
            movie.Description = "test";
            int result = MovieManager.Update(movie, true);
            Assert.AreEqual(1, result);
        }
    }
}