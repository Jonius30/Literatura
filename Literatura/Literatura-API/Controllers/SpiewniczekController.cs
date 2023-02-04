using Literatura_API.Interfaces;
using Literatura_API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Literatura_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpiewniczekController : ControllerBase
    {
        private readonly ISpiewniczekRepository _spiewniczekRepository;

        public SpiewniczekController(ISpiewniczekRepository spiewniczekRepository)
        {
            _spiewniczekRepository = spiewniczekRepository;
        }

        [HttpGet("{id}")]
        public IActionResult GetSpiewniczek(int id)
        {
            return Ok(_spiewniczekRepository.GetSpiewniczek(id));
        }

        [HttpPost("Search")]
        public IActionResult SearchSpiewniczek(string searchString)
        {
            return Ok(_spiewniczekRepository.SearchResults(searchString));
        }


    }
}
