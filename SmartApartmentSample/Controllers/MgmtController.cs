using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;
using Newtonsoft.Json;
using SampleApartmentSample.Data.Abstractions;
using SampleApartmentSample.Data.Implementation;
using SampleApartmentSample.Data.Models;
using SmartApartmentSample.Abstractions;
using SmartApartmentSample.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace SmartApartmentSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MgmtController : ControllerBase
    {
        private const string INDEX = "mgmt";
        private readonly ILogger<MgmtController> _logger;
        private readonly IMgmtElasticSearch _client;

        public MgmtController(ILogger<MgmtController> logger, IMgmtElasticSearch client)
        {
            _logger = logger;
            _client = client;
        }

        // POST api/<MgmtController>/search
        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> Post(SearchInputModel search)
        {
            IEnumerable<Mgmt> document = await _client.Search(search);
            return Ok(document);
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload()
        {
            var message = await _client.Upload(HttpContext.Request.Body);
            return Ok(message);
        }
    }
}
