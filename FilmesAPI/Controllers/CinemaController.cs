using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaController : ControllerBase
    {
        private readonly FilmeContext _db;
        private readonly IMapper _mapper;

        public CinemaController(FilmeContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            var cinema = _mapper.Map<Cinema>(cinemaDto);
            _db.Cinemas.Add(cinema);
            _db.SaveChanges();
            return CreatedAtAction(nameof(RecuperaCinemaPorId), new { id = cinema.Id }, cinema);
        }

        [HttpGet]
        public IActionResult RecuperaCinemas([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var cinemas = _mapper.Map<ICollection<ReadCinemaDto>>(_db.Cinemas.Skip(skip).Take(take));
            return Ok(cinemas);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaCinemaPorId(int id)
        {
            var cinema = _db.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null) 
                return NotFound();
            var cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
            return Ok(cinemaDto);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            var cinema = _db.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null) 
                return NotFound();
            _mapper.Map(cinemaDto, cinema);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult AtualizaCinemaParcial(int id, JsonPatchDocument<UpdateCinemaDto> patch)
        {
            var cinema = _db.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null) 
                return NotFound();

            var cinemaParaAtualizar = _mapper.Map<UpdateCinemaDto>(cinema);

            patch.ApplyTo(cinemaParaAtualizar, ModelState);

            if (!TryValidateModel(cinemaParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(cinemaParaAtualizar, cinema);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaCinema(int id)
        {
            var cinema = _db.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null) 
                return NotFound();
            _db.Remove(cinema);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
