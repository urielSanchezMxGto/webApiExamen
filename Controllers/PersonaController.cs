using Microsoft.AspNetCore.Mvc;
using webApiExamen.Data;
using webApiExamen.Models;

namespace webApiExamen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : ControllerBase
    {

        private readonly PersonaRepository _repo;

        public PersonaController(PersonaRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("insertar")]
        public async Task<ActionResult> InsertarPersona([FromBody] Persona persona)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var mensaje = await _repo.InsertarPersona(persona);
            return Ok(new { Mensaje = mensaje });
        }

        [HttpPost("buscar")]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersonas([FromBody] Persona persona)
        {
            var personas = await _repo.GetPersonas(persona);
            return Ok(personas);
        }

    }
}
