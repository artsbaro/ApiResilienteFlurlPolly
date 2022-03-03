using ApiResilienteFlurlPolly.Core.Models;
using BffResilienteFlurlPolly.Services;
using Microsoft.AspNetCore.Mvc;

namespace BffResilienteFlurlPolly.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {

        private readonly ILogger<PessoaController> _logger;
        private readonly IPessoaService _service;


        public PessoaController(ILogger<PessoaController> logger,
            IPessoaService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet(Name = "Pessoas")]
        [ProducesResponseType(typeof(IEnumerable<Pessoa>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Pessoa>>> GetPessoas()
        {
            var pessoas = await _service.GetPessoas();
            return Ok(pessoas);
        }
    }
}