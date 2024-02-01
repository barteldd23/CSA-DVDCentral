
namespace DDB.DVDCentral.PL.Test
{
    [TestClass]
    public class utGenre : utBase<tblGenre>
    {

            [TestMethod]
            public void LoadTest()
            {
                int expected = 10;
                var genres = base.LoadTest();
                Assert.AreEqual(expected, genres.Count());
            }

            [TestMethod]
            public void InsertTest()
            {
                int rowsAffected = InsertTest(new tblGenre
                {
                    Id = Guid.NewGuid(),
                    Description = "XXXXX"
                });
                Assert.AreEqual(1, rowsAffected);
            }

            [TestMethod]
            public void UpdateTest()
            {
                tblGenre row = base.LoadTest().FirstOrDefault();

                if (row != null)
                {
                    row.Description = "YYYYY";
                    int rowsAffected = UpdateTest(row);
                    Assert.AreEqual(1, rowsAffected);
                }
            }


            [TestMethod]
            public void DeleteTest()
            {
                tblGenre row = base.LoadTest().FirstOrDefault(x => x.Description == "Other");

                if (row != null)
                {
                    dc.tblGenres.Remove(row);
                    int rowsAffected = UpdateTest(row);
                    Assert.IsTrue(rowsAffected == 1);
                }

            }
        }
    }
