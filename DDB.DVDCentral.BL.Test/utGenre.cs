namespace DDB.DVDCentral.BL.Test
{
    [TestClass]
    public class utGenre : utBase
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Genre> genres = new GenreManager(options).Load();
            int expected = 10;

            Assert.AreEqual(expected, genres.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Genre genre = new Genre
            {
                Description = "XXXXX"
            };

            int result = new GenreManager(options).Insert(genre, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Genre genre = new GenreManager(options).Load().FirstOrDefault();
            genre.Description = "Blah blah";

            Assert.IsTrue(new GenreManager(options).Update(genre, true) > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Genre genre = new GenreManager(options).Load().FirstOrDefault();

            Assert.IsTrue(new GenreManager(options).Delete(genre.Id, true) > 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            Genre genre = new GenreManager(options).Load().FirstOrDefault();
            Assert.AreEqual(new GenreManager(options).LoadById(genre.Id).Id, genre.Id);
        }


    }
}