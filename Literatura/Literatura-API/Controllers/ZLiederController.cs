using Literatura_API.Interfaces;
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

        [HttpPost("Search")]
        public IActionResult SearchZLieder(string searchString)
        {
            return Ok(_zLiederRepository.SearchResults(searchString));
        }
    }
}
