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
    public class MovieController : ControllerBase
    {
        private readonly DbContextOptions<DVDCentralEntities> options;
        private readonly ILogger<MovieController> logger;

        public MovieController(ILogger<MovieController> logger,
                                DbContextOptions<DVDCentralEntities> options)
        {
            this.options = options;
            this.logger = logger;
            logger.LogWarning("I was here");
        }

        // GET: api/<MovieController>
        /// <summary>
        /// Returns a list of movies.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return new MovieManager(options).Load();
        }

        // GET api/<MovieController>/5
        /// <summary>
        /// Get a particular movie by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Movie Get(Guid id)
        {
            return new MovieManager(options).LoadById(id);
        }

        // POST api/<MovieController>
        /// <summary>
        /// Insert a movie
        /// </summary>
        /// <param name="movie"></param>
        /// <param name="rollback"></param>
        /// <returns></returns>
        [HttpPost("{rollback?}")]
        public int Post([FromBody] Movie movie, bool rollback = false)
        {
            try
            {
                return new MovieManager(options).Insert(movie, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT api/<MovieController>/5
        /// <summary>
        /// Update a movie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movie"></param>
        /// <param name="rollback"></param>
        /// <returns></returns>
        [HttpPut("{id}/{rollback?}")]
        public int Put(Guid id, [FromBody] Movie movie, bool rollback = false)
        {
            try
            {
                return new MovieManager(options).Update(movie, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // DELETE api/<MovieController>/5
        /// <summary>
        /// Delete a movie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rollback"></param>
        /// <returns></returns>
        [HttpDelete("{id}/{rollback?}")]
        public int Delete(Guid id, bool rollback = false)
        {
            try
            {
                return new MovieManager(options).Delete(id, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
