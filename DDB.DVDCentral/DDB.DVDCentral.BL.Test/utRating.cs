using DVDCentral.BL.Models;

namespace DDB.DVDCentral.BL.Test
{
    [TestClass]
    public class utRating
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Rating> list = RatingManager.Load();
            Assert.AreEqual(5, list.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Rating rating = new Rating
            {
                Description = "Test",
                Id = -99
            };
            int result = RatingManager.Insert(rating, true);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int result = RatingManager.Delete(3, true);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Rating rating = RatingManager.LoadById(2);
            rating.Description = "Test";
            int result = RatingManager.Update(rating, true);
            Assert.AreEqual(1, result);
        }
    }
}