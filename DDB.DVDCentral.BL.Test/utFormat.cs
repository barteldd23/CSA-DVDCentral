using DVDCentral.BL.Models;

namespace DDB.DVDCentral.BL.Test
{
    [TestClass]
    public class utFormat : utBase
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Format> formats = new FormatManager(options).Load();
            int expected = 4;
            Assert.AreEqual(expected, formats.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Format format = new Format
            {
                Description = "XXXXX"
            };

            int result = new FormatManager(options).Insert(format, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Format format = new FormatManager(options).Load().FirstOrDefault();
            format.Description = "Blah blah";
            Assert.IsTrue(new FormatManager(options).Update(format, true) > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Format format = new FormatManager(options).Load().FirstOrDefault(x => x.Description == "Other");
            Assert.IsTrue(new FormatManager(options).Delete(format.Id, true) > 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            Format format = new FormatManager(options).Load().FirstOrDefault();
            Assert.AreEqual(new FormatManager(options).LoadById(format.Id).Id, format.Id);
        }


    }
}