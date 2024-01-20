using DVDCentral.BL.Models;

namespace DDB.DVDCentral.BL.Test
{
    [TestClass]
    public class utMovieGenre
    {

        [TestMethod]
        public void InsertTest()
        {
            
            int result = MovieGenreManager.Insert(1,2,true);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int result = MovieGenreManager.Delete(3, true);
            Assert.AreEqual(1, result);
            result = MovieGenreManager.Delete(3,2, true);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            int result = MovieGenreManager.Update(1,2,2, true);
            Assert.AreEqual(1, result);
        }
    }
}