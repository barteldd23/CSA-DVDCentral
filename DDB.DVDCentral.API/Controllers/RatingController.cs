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
    public class RatingController : ControllerBase
    {

        private readonly DbContextOptions<DVDCentralEntities> options;
        private readonly ILogger<RatingController> logger;

        public RatingController(ILogger<RatingController> logger,
                                DbContextOptions<DVDCentralEntities> options)
        {
            this.options = options;
            this.logger = logger;
        }
        [HttpGet]
        public IEnumerable<Rating> Get()
        {
            return new RatingManager(options).Load();
        }

        [HttpGet("{id}")]
        public Rating Get(Guid id)
        {
            return new RatingManager(options).LoadById(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Rating rating)
        {
            try
            {
                int results = new RatingManager(options).Insert(rating);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest (ex.Message + ":" + ex.InnerException.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Rating rating) 
        {
            try
            {
                int results = new RatingManager(options).Update(rating);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                int results = new RatingManager(options).Delete(id);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
