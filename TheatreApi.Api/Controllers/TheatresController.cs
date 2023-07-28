using Microsoft.AspNetCore.Mvc;
using TheatreApi.Api.Converter;
using TheatreApi.DataAccess.Models;
using TheatreApi.DataAccess.Repositories;

namespace TheatreApi.Api.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class TheatresController : Controller
    {
        private readonly ITheatreRepository _theatreRepository;
        private readonly ITheatreConverter _theatreConverter;

        public TheatresController(ITheatreRepository theatreRepository, ITheatreConverter theatreConverter)
        {
            _theatreRepository = theatreRepository;
            _theatreConverter = theatreConverter;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Theatre theatreModel)
        {
            var theatre = _theatreRepository.CreateTheatre(theatreModel);
            var theatreDto = _theatreConverter.ToDto(theatre);

            return Ok(theatreDto);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var listTheatres = _theatreRepository.GetTheatres().Select(t => _theatreConverter.ToDto(t));

            return Ok(listTheatres);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var theatre = _theatreRepository.GetTheatre(id);
            if(theatre == null) return NotFound();
            var theatreDto = _theatreConverter.ToDto(theatre);

            return Ok(theatreDto);
        }

        [HttpPut()]
        public IActionResult AddPlayToTheatre([FromQuery]int idTheatre, [FromQuery] int idPlay)
        {
            var listTheatres = _theatreRepository.GetTheatres();
            var listPlays = _theatreRepository.GetPlays();

            var modifiedTheatre = _theatreRepository.UpdatePlay(listTheatres, idTheatre, listPlays, idPlay);

            var theatreDto = _theatreConverter.ToDto(modifiedTheatre);

            return Ok(theatreDto);
        }

        [HttpGet("Average")]
        public IActionResult GetAverage()
        {
            var average = _theatreRepository.GetActorsAverage();

            return Ok(average);
        }
    }
}
