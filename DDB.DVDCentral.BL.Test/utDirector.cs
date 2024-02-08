
namespace DDB.DVDCentral.BL.Test
{
    [TestClass]
    public class utDirector : utBase
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Director> directors = new DirectorManager(options).Load();
            int expected = 5;

            Assert.AreEqual(expected, directors.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Director director = new Director
            {
                FirstName = "Sarah",
                LastName = "Vicchiollo"
            };

            int result = new DirectorManager(options).Insert(director, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Director director = new DirectorManager(options).Load().FirstOrDefault();
            director.FirstName = "Blah blah blah";

            Assert.IsTrue(new DirectorManager(options).Update(director, true) > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Director director = new DirectorManager(options).Load().FirstOrDefault();

            Assert.IsTrue(new DirectorManager(options).Delete(director.Id, true) > 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {

            Director director = new DirectorManager(options).Load().FirstOrDefault();
            Assert.AreEqual(new DirectorManager(options).LoadById(director.Id).Id, director.Id);
        }


    }
}