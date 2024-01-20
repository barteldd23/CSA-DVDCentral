using DDB.DVDCentral.BL;
using DVDCentral.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDB.DVDCentral.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Rating> Get()
        {
            return RatingManager.Load();
        }

        [HttpGet("{id}")]
        public Rating Get(int id)
        {
            return RatingManager.LoadById(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Rating rating)
        {
            try
            {
                int results = RatingManager.Insert(rating);
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
                int results = RatingManager.Update(rating);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                int results = RatingManager.Delete(id);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
