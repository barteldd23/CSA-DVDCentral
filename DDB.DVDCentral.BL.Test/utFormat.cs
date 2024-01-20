using DVDCentral.BL.Models;

namespace DDB.DVDCentral.BL.Test
{
    [TestClass]
    public class utFormat
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Format> list = FormatManager.Load();
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Format format = new Format
            {
                Description = "Test",
                Id = -99
            };
            int result = FormatManager.Insert(format, true);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int result = FormatManager.Delete(1, true);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Format format = FormatManager.LoadById(1);
            format.Description = "Test";
            int result = FormatManager.Update(format, true);
            Assert.AreEqual(1, result);
        }
    }
}