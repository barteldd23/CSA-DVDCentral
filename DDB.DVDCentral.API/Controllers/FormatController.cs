using DDB.DVDCentral.BL;
using DDB.DVDCentral.BL.Models;
using DDB.DVDCentral.PL2.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DDB.DVDCentral.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormatController : ControllerBase
    {
        private readonly DbContextOptions<DVDCentralEntities> options;
        private readonly ILogger<FormatController> logger;

        public FormatController(ILogger<FormatController> logger,
                                DbContextOptions<DVDCentralEntities> options)
        {
            this.options = options;
            this.logger = logger;
        }

        // GET: api/<FormatController>
        [HttpGet]
        public IEnumerable<Format> Get()
        {
            return new FormatManager(options).Load();
        }

        // GET api/<FormatController>/5
        [HttpGet("{id}")]
        public Format Get(Guid id)
        {
            return new FormatManager(options).LoadById(id);
        }

        // POST api/<FormatController>
        [HttpPost("{rollback?}")]
        public int Post([FromBody] Format format, bool rollback = false)
        {
            return new FormatManager(options).Insert(format, rollback);
        }

        // PUT api/<FormatController>/5
        [HttpPut("{id}/{rollback?}")]
        public int Put(Guid id, [FromBody] Format format, bool rollback = false)
        {
            return new FormatManager(options).Insert(format, rollback);
        }

        // DELETE api/<FormatController>/5
        [HttpDelete("{id}/{rollback?}")]
        public int Delete(Guid id, bool rollback = false)
        {
            return new FormatManager(options).Delete(id, rollback);
        }
    }
}
