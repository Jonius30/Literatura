using Literatura_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Literatura_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PBTController : ControllerBase
    {
        private readonly IPbtRepository _pbtRepository;

        public PBTController(IPbtRepository pbtRepository)
        {
            _pbtRepository = pbtRepository;
        }

        [HttpGet("{id}")]
        public IActionResult GetPBT(int id)
        {
            return Ok(_pbtRepository.GetPbt(id));
        }

        [HttpPost("Search")]
        public IActionResult SearchPbt(string searchString)
        {
            return Ok(_pbtRepository.SearchResults(searchString));  
        }
    }
}
