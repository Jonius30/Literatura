using Literatura_API.Interfaces;
using Literatura_API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Literatura_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZLiederController : ControllerBase
    {
        private readonly IZLiederRepository _zLiederRepository;

        public ZLiederController(IZLiederRepository zLiederRepository)
        {
            _zLiederRepository = zLiederRepository;
        }

        [HttpGet("{id}")]
        public IActionResult GetZLieder(int id)
        {
            return Ok(_zLiederRepository.GetZLieder(id));
        }

        [HttpGet("Next/{id}")]
        public IActionResult GetNextZLieder(int id)
        {
            return Ok(_zLiederRepository.GetNextZLieder(id));
        }

        [HttpGet("Previous/{id}")]
        public IActionResult GetPreviousZLieder(int id)
        {
            return Ok(_zLiederRepository.GetPreviousZLieder(id));
        }

        [HttpPost("Search")]
        public IActionResult SearchZLieder(string searchString)
        {
            return Ok(_zLiederRepository.SearchResults(searchString));
        }
    }
}
