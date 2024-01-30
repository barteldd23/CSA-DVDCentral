
namespace DDB.DVDCentral.PL.Test
{
    [TestClass]
    public class utMovie : utBase<tblMovie>
    {
       

        [TestMethod]
        public void LoadTest()
        {
            int results = dc.tblMovies.Count();
            Assert.AreEqual(7, results);
        }

        [TestMethod]
        public void LoadAllTest()
        {
            var movies = (from m in dc.tblMovies
                          join f in dc.tblFormats on m.FormatId equals f.Id
                          join r in dc.tblRatings on m.RatingId equals r.Id
                          join d in dc.tblDirectors on m.DirectorId equals d.Id
                          select new
                          {
                              m.Id,
                              RatingDescription = r.Description,
                              FormatDescription = f.Description,
                              DirectorFullName = d.FirstName + " " + d.LastName
                          }).ToList();

            Assert.AreEqual(7, movies.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            tblMovie entity = new tblMovie();
            entity.Title = "test Title";
            entity.Description = "test Desccription";
            entity.Cost = 9.99;
            entity.RatingId = base.LoadTest().FirstOrDefault().RatingId;
            entity.FormatId = base.LoadTest().FirstOrDefault().FormatId;
            entity.DirectorId = base.LoadTest().FirstOrDefault().DirectorId;
            entity.Quantity = 2;
            entity.ImagePath = "test path";

            entity.Id = Guid.NewGuid();

            int rowsAffected = base.InsertTest(entity);
            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblMovie entity = dc.tblMovies.FirstOrDefault();

            if (entity != null)
            {
                entity.Description = "test Update";
                int results = dc.SaveChanges();
                Assert.AreEqual(1, results);
            }
        }

        [TestMethod]
        public void DeleteTest() 
        {
            tblMovie entity = dc.tblMovies.FirstOrDefault();
            dc.tblMovies.Remove(entity);
            int results = dc.SaveChanges();
            Assert.AreEqual(1, results);
        }

        //[TestMethod]
        //public void LoadByIdTest()
        //{
        //    tblMovie entity = dc.tblMovies.Where(e => e.Id == 2).FirstOrDefault();

        //    Assert.AreEqual(2, entity.Id);
        //}
    }
}