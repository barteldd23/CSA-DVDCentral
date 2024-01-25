
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DDB.DVDCentral.PL.Test
{
    [TestClass]
    public class utBase
    {
        protected DVDCentralEntities dc; // Declare the DataContext
        protected IDbContextTransaction transaction;
        private IConfigurationRoot _configuration;
        private DbContextOptions<DVDCentralEntities> options;

        public utBase()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
            options = new DbContextOptionsBuilder<DVDCentralEntities>()
                .UseSqlServer(_configuration.GetConnectionString("DatabaseConnection"))
                .UseLazyLoadingProxies()
                .Options;

            dc = new DVDCentralEntities(options);
        }

        [TestInitialize]
        public void Initialize()
        {
            transaction = dc.Database.BeginTransaction();
        }

        [TestCleanup]
        public void Cleanup()
        {
            transaction.Rollback();
            transaction.Dispose();
            dc = null;
        }
    }
}
