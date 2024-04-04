
using DDB.Reporting;

namespace DDB.DVDCentral.BL.Test
{
    [TestClass]
    public class utFormat : utBase
    {
        [TestMethod]
        public void utReportTest()
        {
            var format = new FormatManager(options).Load();
            string[] columns = { "Description" };
            var data = MovieManager.ConvertData<Format>(format, columns);
            Excel.Export("movies.xlsx", data);
        }

        [TestMethod]
        public void LoadTest()
        {
            List<Format> formats = new FormatManager(options).Load();
            int expected = 4;
            Assert.AreEqual(expected, formats.Count);
        }

        [TestMethod]
        public async Task LoadTestAsync()
        {
            List<Format> formats = await new FormatManager(options).LoadAsync();
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
        public async Task InsertTestAsyncFail()
        {
            try
            {
                Format format = new Format
                {
                    Description = "Other"
                };

                int result = await new FormatManager(options).InsertAsync(format, true);
                Assert.IsTrue(result > 0);
            }
            catch (AlreadyExistsException ex)
            {
                Assert.IsTrue(true);
            }

            catch (Exception)
            {
                Assert.Fail();
            }
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