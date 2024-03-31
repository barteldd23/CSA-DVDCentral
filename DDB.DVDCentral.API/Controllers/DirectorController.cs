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
        private readonly ILogger<DirectorController> logger;
        private readonly DbContextOptions<DVDCentralEntities> options;
        

        public DirectorController(ILogger<DirectorController> logger,
                                DbContextOptions<DVDCentralEntities> options)
        {
            this.options = options;
            this.logger = logger;
            logger.LogWarning("I was here");
        }

        // GET: api/<DirectorController>
        [HttpGet]
        public  IEnumerable<Director> Get()
        {
            try
            {
                logger.LogWarning("Director-->");
                return new DirectorManager(logger, options).Load();
            }
            catch (Exception ex)
            {
                return null;
               // return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<DirectorController>/5
        [HttpGet("{id}")]
        public Director Get(Guid id)
        {
            return new DirectorManager(logger, options).LoadById(id);
        }

        // POST api/<DirectorController>
        [HttpPost("{rollback?}")]
        public int Post([FromBody] Director director, bool rollback = false)
        {
            try
            {
                return new DirectorManager(logger, options).Insert(director, rollback);
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
                return new DirectorManager(logger, options).Update(director, rollback);
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
                return new DirectorManager(logger, options).Delete(id, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
