
using DDB.Reporting;

namespace DDB.DVDCentral.BL.Test
{
    [TestClass]
    public class utMovieGenre : utBase
    {

        [TestMethod]
        public void InsertTest()
        {
            Guid movieId = new MovieManager(options).Load().FirstOrDefault().Id;
            Guid genreId = new GenreManager(options).Load().FirstOrDefault().Id;
            int result = new MovieGenreManager(options).Insert(movieId, genreId, true);
            Assert.IsTrue(result > 0);
        }

        //[TestMethod]
        //public void DeleteTest()
        //{
        //    Guid movieGenreId = new MovieGenreManager(options).Load().FirstOrDefault().Id;
        //    Assert.IsTrue(new MovieGenreManager(options).Delete(movieGenreId, true) > 0);
        //}

        [TestMethod]
        public void DeleteTest2()
        {
            Movie movie = new MovieManager(options).Load().FirstOrDefault();
            Guid genreId = movie.Genres[0].Id;
            
            //Guid movieId = new MovieManager(options).Load().FirstOrDefault().Id;
            //Guid genreId = new GenreManager(options).Load().FirstOrDefault().Id;
            Assert.IsTrue(new MovieGenreManager(options).Delete(movie.Id, genreId, true) > 0);
        }
        [TestMethod]
        public void utReportTest()
        {
            var movies = new MovieManager(options).Load();
            Excel.Export("movies.xlsx", MovieManager.ConvertData(movies));
        }

    }
}