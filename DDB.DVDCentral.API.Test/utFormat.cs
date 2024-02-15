using DDB.DVDCentral.BL.Models;
using System.Runtime.InteropServices;

namespace DDB.DVDCentral.API.Test
{
    [TestClass]
    public class utFormat : utBase
    {
        [TestMethod]
        public async Task LoadTestAsync()
        {
            await base.LoadTestAsync<Format>();
        }

        [TestMethod]
        public async Task InsertTestAsync()
        {
            Format format = new Format { Description = "Test" };
            await base.InsertTestAsync<Format>(format);

        }

        [TestMethod]
        public async Task DeleteTestAsync()
        {
            await base.DeleteTestAsync<Format>(new KeyValuePair<string, string>("Description", "DVD"));
        }

        [TestMethod]
        public async Task LoadByIdTestAsync()
        {
            await base.LoadByIdTestAsync<Format>(new KeyValuePair<string, string>("Description", "DVD"));
        }

        [TestMethod]
        public async Task UpdateTestAsync()
        {
            Format format = new Format { Description = "Test" };
            await base.UpdateTestAsync<Format>(new KeyValuePair<string, string>("Description", "DVD"), format);

        }

    }
}
