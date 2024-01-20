using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;

namespace DDB.DVDCentral.PL.Test
{
    [TestClass]
    public class utMovie
    {
        protected DVDCentralEntities dc;
        protected IDbContextTransaction transaction;

        [TestInitialize]
        public void Initialize()
        {
            dc = new DVDCentralEntities();
            transaction = dc.Database.BeginTransaction();
        }

        [TestCleanup]
        public void Cleanup()
        {
            transaction.Rollback();
            transaction.Dispose();
            dc = null;
        }


        [TestMethod]
        public void LoadTest()
        {
            int results = dc.tblMovies.Count();
            Assert.AreEqual(3, results);
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

            Assert.AreEqual(3, movies.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            tblMovie entity = new tblMovie();
            entity.Title = "test Title";
            entity.Description = "test Desccription";
            entity.Cost = 9.99;
            entity.RatingId = 1;
            entity.FormatId = 1;
            entity.DirectorId = 1;
            entity.InStkQty = 2;
            entity.ImagePath = "test path";

            entity.Id = -99;

            dc.tblMovies.Add(entity);
            int results = dc.SaveChanges();
            Assert.AreEqual(1, results);
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

        [TestMethod]
        public void LoadByIdTest()
        {
            tblMovie entity = dc.tblMovies.Where(e => e.Id == 2).FirstOrDefault();

            Assert.AreEqual(2, entity.Id);
        }
    }
}