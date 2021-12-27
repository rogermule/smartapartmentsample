using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleApartmentSample.Data.Abstractions;
using SampleApartmentSample.Data.Models;
using System.Threading.Tasks;


namespace SmartApartmentSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly ILogger<PropertiesController> _logger;
        private readonly IPropertiesElasticSearch _client;

        public PropertiesController(ILogger<PropertiesController> logger, IPropertiesElasticSearch client)
        {
            _logger = logger;
            _client = client;
        }

        // POST api/<PropertiesController>/search
        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> Post(SearchInputModel search)
        {
            var document = await _client.Search(search);
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
