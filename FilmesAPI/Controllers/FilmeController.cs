using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private static List<Filme> _filmes = new List<Filme>();
    private static int _id = 0;

    [HttpPost]
    public void AdicionaFilme([FromBody] Filme filme)
    {
        filme.Id = ++_id;
        _filmes.Add(filme);
        HttpResponse response = HttpContext.Response;
        response.StatusCode = 201;
    }

    [HttpGet]
    public IEnumerable<Filme> RecuperaFilmes([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        return _filmes.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public Filme? RecuperaFilmePorId(int id)
    {
        return _filmes.FirstOrDefault(filme => filme.Id == id);
    }
}
