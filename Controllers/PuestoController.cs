using Microsoft.AspNetCore.Mvc;
using webApiExamen.Data;
using webApiExamen.Models;

namespace webApiExamen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PuestoController : ControllerBase
    {

        private readonly PuestoRepository _repo;

        public PuestoController(PuestoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Puesto>>> GetPuestos()
        {
            var puestos = await _repo.GetPuestos();
            return Ok(puestos);
        }

    }
}
