using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Enderecos;
using FilmesAPI.Data.Dtos.Filme;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public EnderecoController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            var endereco = _mapper.Map<Endereco>(enderecoDto);
            _db.Enderecos.Add(endereco);
            _db.SaveChanges();
            return CreatedAtAction(nameof(RecuperaEnderecoPorId), new { id = endereco.Id }, endereco);
        }

        [HttpGet]
        public IActionResult RecuperaEnderecos([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var enderecos = _mapper.Map<ICollection<ReadEnderecoDto>>(_db.Enderecos.Skip(skip).Take(take));
            return Ok(enderecos);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaEnderecoPorId(int id)
        {
            var endereco = _db.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null)
                return NotFound();
            var enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);
            return Ok(enderecoDto);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto)
        {
            var endereco = _db.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null)
                return NotFound();
            _mapper.Map(enderecoDto, endereco);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult AtualizaEnderecoParcial(int id, [FromBody] UpdateEnderecoDto enderecoDto)
        {
            var endereco = _db.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null)
                return NotFound();
            _mapper.Map(enderecoDto, endereco);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaEndereco(int id)
        {
            var endereco = _db.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null)
                return NotFound();
            _db.Remove(endereco);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
