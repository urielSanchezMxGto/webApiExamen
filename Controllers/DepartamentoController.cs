using Microsoft.AspNetCore.Mvc;
using webApiExamen.Data;
using webApiExamen.Models;

namespace webApiExamen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentoController : ControllerBase
    {

        private readonly DepartamentoRepository _repo;

        public DepartamentoController(DepartamentoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Departamento>>> GetDepartamento()
        {
            var departamentos = await _repo.GetDepartamentos();
            return Ok(departamentos);
        }


    }
}
