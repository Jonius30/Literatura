using Literatura_API.Interfaces;
using Literatura_API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Literatura_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MannaController : ControllerBase
    {
        private readonly IMannaRepository _mannaRepository;

        public MannaController(IMannaRepository mannaRepository)
        {
            _mannaRepository = mannaRepository;
        }

        [HttpGet("{day}/{month}")]
        public IActionResult GetManna(int day, int month)
        {
            return Ok(_mannaRepository.GetManna(day,month));
        }

        [HttpGet("Next/{day}/{month}")]
        public IActionResult GetNextManna(int day, int month)
        {
            return Ok(_mannaRepository.GetNextManna(day, month));
        }

        [HttpGet("Previous/{day}/{month}")]
        public IActionResult GetPreviousManna(int day, int month)
        {
            return Ok(_mannaRepository.GetPreviousManna(day, month));
        }



        [HttpPost("Search")]
        public IActionResult SearchPbt(string searchString)
        {
            return Ok(_mannaRepository.SearchResults(searchString));
        }
    }
}
