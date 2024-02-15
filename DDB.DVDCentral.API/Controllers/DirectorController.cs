using DDB.DVDCentral.BL;
using DDB.DVDCentral.BL.Models;
using DDB.DVDCentral.PL2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DDB.DVDCentral.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly DbContextOptions<DVDCentralEntities> options;
        private readonly ILogger<DirectorController> logger;

        public DirectorController(ILogger<DirectorController> logger,
                                DbContextOptions<DVDCentralEntities> options)
        {
            this.options = options;
            this.logger = logger;
            logger.LogWarning("I was here");
        }

        // GET: api/<DirectorController>
        [HttpGet]
        public IEnumerable<Director> Get()
        {
            return new DirectorManager(options).Load();
        }

        // GET api/<DirectorController>/5
        [HttpGet("{id}")]
        public Director Get(Guid id)
        {
            return new DirectorManager(options).LoadById(id);
        }

        // POST api/<DirectorController>
        [HttpPost("{rollback?}")]
        public int Post([FromBody] Director director, bool rollback = false)
        {
            try
            {
                return new DirectorManager(options).Insert(director, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT api/<DirectorController>/5
        [HttpPut("{id}/{rollback?}")]
        public int Put(Guid id, [FromBody] Director director, bool rollback = false)
        {
            try
            {
                return new DirectorManager(options).Update(director, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // DELETE api/<DirectorController>/5
        [HttpDelete("{id}/{rollback?}")]
        public int Delete(Guid id, bool rollback = false)
        {
            try
            {
                return new DirectorManager(options).Delete(id, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
