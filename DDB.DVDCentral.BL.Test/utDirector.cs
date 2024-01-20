using DVDCentral.BL.Models;

namespace DDB.DVDCentral.BL.Test
{
    [TestClass]
    public class utDirector
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Director> list = DirectorManager.Load();
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Director director = new Director
            {
                FirstName = "Test",
                LastName = "Test",
                Id = -99
            };
            int result = DirectorManager.Insert(director, true);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int result = DirectorManager.Delete(2, true);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Director director = DirectorManager.LoadById(2);
            director.FirstName = "Test";
            int result = DirectorManager.Update(director, true);
            Assert.AreEqual(1, result);
        }
    }
}