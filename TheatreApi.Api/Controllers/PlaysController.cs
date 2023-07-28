using Microsoft.AspNetCore.Mvc;
using TheatreApi.Api.Converter;
using TheatreApi.DataAccess.Models;
using TheatreApi.DataAccess.Repositories;

namespace TheatreApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaysController : Controller
    {
        private readonly ITheatreRepository _theatreRepository;
        private readonly IPlayConverter _playConverter;
        public PlaysController(ITheatreRepository theatreRepository, IPlayConverter playConverter)
        {
            _theatreRepository = theatreRepository;
            _playConverter = playConverter;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Play playModel)
        {
            _playConverter.ToDto(_theatreRepository.CreatePlay(playModel));

            return Ok(playModel);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_theatreRepository.GetPlays());
        }

        [HttpGet("filter")]
        public IActionResult Get([FromQuery] DateTimeOffset? startDate, [FromQuery] DateTimeOffset? endDate, [FromQuery] string name)
        {
            var play = _theatreRepository.GetPlay(startDate, endDate, name).Select(p => _playConverter.ToDto(p));

            return Ok(play);
        }

        [HttpGet("{id}/plays")]
        public IActionResult GetPlays(int id)
        {
            var plays = _theatreRepository.GetPlays(id).Select(p => _playConverter.ToDto(p));

            return Ok(plays);
        }
    }
}
